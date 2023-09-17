using CloudStorage.Interfaces;
using CloudStorage.Repositories;
using CloudStorage.Contexts;
using Microsoft.EntityFrameworkCore;
using CloudStorage.Config;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using CloudStorage.Services;
using CloudStorage.Middlewares;
using CloudStorage.Helpers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ICloudinaryService, CloudinaryService>();
builder.Services.AddScoped<IDokumentService, DokumentService>();
builder.Services.AddScoped<IDokumentRepository, DokumentRepository>();
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
builder.Services.AddScoped<IBCryptHelpers, BCryptHelpers>();



builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DbConnection")
));

var jwtOptions = builder.Configuration
    .GetSection("JwtConfig")
    .Get<JwtConfig>();

builder.Services.Configure<CloudinaryConfig>(builder.Configuration.GetSection("CloudinaryConfig"));
builder.Services.Configure<JwtConfig>(builder.Configuration.GetSection("JwtConfig"));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(options =>
{
    byte[] accessTokenKeyBytes = Encoding.UTF8.GetBytes(jwtOptions.AccessTokenKey);

    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(accessTokenKeyBytes),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = false,
    };
});

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<SetRequestIdMiddleware>();
app.UseMiddleware<ErrorHandlerMiddleware>();

app.MapControllers();

app.Run();