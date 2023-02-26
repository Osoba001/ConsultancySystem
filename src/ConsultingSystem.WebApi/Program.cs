using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using ConsultancySystem.WebApi.Files.Manager;
using Law.Persistence.Data;
using Law.Persistence.Dependency;
using LCS.WebApi.CustomMiddlewares;
using Microsoft.OpenApi.Models;
using Serilog;
using ShareServices.Dependency;
using ShareServices.Models;
using User.Application.DTO;
using User.Persistence.Data;
using User.Persistence.Dependency;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Description = "Bearer Authentication with JWT Token",
        Type = SecuritySchemeType.Http
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            new List<string>()
        }
    });
});

var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

builder.Services.AddCors(opt =>
{
   
    opt.AddDefaultPolicy(x =>
    {
        x.AllowAnyOrigin()
        //.WithOrigins(builder.Configuration.GetSection("AllowedOrigins").Get<string[]>())
        //.WithHeaders("Authorization")
        .AllowAnyHeader()
        .WithMethods("GET", "POST", "PUT", "PATCH", "DELETE");
    });
});

var config = builder.Configuration;
string lawDbConStr;
string userDbConStr;
string authSecretKey;
string vaultUri= config.GetSection("VaultUri").Value;
var client = new SecretClient(vaultUri: new Uri(vaultUri), credential: new DefaultAzureCredential());
KeyVaultSecret lawConStrSecret = client.GetSecret("ConnectionStrings--LawConString");
KeyVaultSecret userConStrSecret = client.GetSecret("ConnectionStrings--AuthConString");
////lawDbConStr = config.GetConnectionString("LawConString");
//userDbConStr = config.GetConnectionString("AuthConString");
authSecretKey = config.GetSection("AuthConfigModel:SecretKey").Value;
lawDbConStr = lawConStrSecret.Value;
userDbConStr = userConStrSecret.Value;
//authSecretKey=JwtSecret.Value;
builder.Services.AddSqlServer<LawDbContext>(lawDbConStr, opt =>
{
    opt.EnableRetryOnFailure(2);
});
builder.Services.AddSqlServer<UserDbContext>(userDbConStr, op =>
{
    op.EnableRetryOnFailure(2);
});
builder.Services.Configure<EmailConfigData>(config.GetSection(nameof(EmailConfigData)));
builder.Services.Configure<AuthConfigModel>(config.GetSection(nameof(AuthConfigModel)));
builder.Services.AddTransient<ExceptionHandlerMiddleware>();
builder.Services.AuthenticationSetup(authSecretKey);
builder.Services.UserServiceCollection();
builder.Services.ShareServiceCollection();
builder.Services.LawDependencyCollection();
builder.Services.AddAuthorization();
builder.Services.AddControllers();
builder.Services.AddScoped<IFileManager,FileManager>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();



var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI(c =>
//    {
//        c.SwaggerEndpoint("/swagger/v1/swagger.json", "ConsultancySystem API V1");
//        c.RoutePrefix = string.Empty;
//    });
//}
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ConsultancySystem API V1");
    if(!app.Environment.IsDevelopment())
        c.RoutePrefix = string.Empty;
});

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthentication();

app.UseAuthorization();

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.MapControllers();

app.Run();
