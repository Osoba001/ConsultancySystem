using Auth.UserServices;
using Microsoft.AspNetCore.Mvc;

namespace ConsultancySystem.WebApi.Controllers.UserModule
{
    public class UserControllerBase:ControllerBase
    {
        private readonly IUserService _userService;

        public UserControllerBase(IUserService userService)
        {
            _userService = userService;
        }
    }
}
