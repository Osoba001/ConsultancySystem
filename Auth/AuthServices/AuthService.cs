using Auth.Entities;
using Auth.Models;
using Auth.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Auth.AuthServices
{
    public class AuthService : IAuthService
    {
        readonly string _secretKey;
        private readonly IUserRepo _userRepo;
        private readonly int _refreshTokenExpireTime;
        private readonly int _accessTokenExpireTime;
        public AuthService(IUserRepo userRepo, IOptionsSnapshot<AuthConfigModel> authConfigOpton)
        {
            _secretKey = authConfigOpton.Value.SecretKey;
            _userRepo = userRepo;
            _refreshTokenExpireTime = authConfigOpton.Value.RefreshTokenLifespanInMins;
            _accessTokenExpireTime=authConfigOpton.Value.AccessTokenLifespanInMins;
        }
        public void PasswordManager(string password, UserTb user)
        {
            using (var hmc = new HMACSHA512())
            {
                byte[] passwordSalt = hmc.Key;
                byte[] passwordHash = hmc.ComputeHash(Encoding.ASCII.GetBytes(password));
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
            }
        }

        public async Task<TokenModel> TokenManager(UserTb user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var encodedKey = Encoding.ASCII.GetBytes(_secretKey);

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,user.Name),
                            new Claim(ClaimTypes.Email,user.Email),
                            new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
            };
            foreach (var role in user.AssignedUserRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.Role.Name));
            }
            
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                
                Subject = new ClaimsIdentity(claims),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(encodedKey), SecurityAlgorithms.HmacSha256),
                Expires = DateTime.UtcNow.AddMinutes(_accessTokenExpireTime),
                
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            user.RefreshToken = $"{RandomToken()}{user.Email}";
            user.RefreshTokenExpireTime = DateTime.UtcNow.AddMinutes(_refreshTokenExpireTime);

            await _userRepo.Update(user);
            return new TokenModel()
            {
                AccessToken = tokenHandler.WriteToken(token),
                RefreshToken = user.RefreshToken,
            };
        }

        public bool VerifyPassword(string password, UserTb user)
        {
            using (var hmc = new HMACSHA512(user.PasswordSalt))
            {
                byte[] computed = hmc.ComputeHash(Encoding.ASCII.GetBytes(password));
                return computed.SequenceEqual(user.PasswordHash);
            }
        }
        private static string RandomToken()
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        }
    }
}
