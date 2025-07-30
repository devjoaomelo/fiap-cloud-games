using System.Text;
using FCG.API.Middleware;
using FCG.Application.UseCases.Games.CreateGame;
using FCG.Application.UseCases.Games.DeleteGame;
using FCG.Application.UseCases.Games.GetAllGames;
using FCG.Application.UseCases.Games.GetGameById;
using FCG.Application.UseCases.Games.UpdateGame;
using FCG.Application.UseCases.UserGames.BuyGame;
using FCG.Application.UseCases.UserGames.GetGamesByUser;
using FCG.Application.UseCases.UserGames.RemoveGameFromUser;
using FCG.Application.UseCases.Users.CreateUser;
using FCG.Application.UseCases.Users.DeleteUser;
using FCG.Application.UseCases.Users.GetAllUsers;
using FCG.Application.UseCases.Users.GetUserByEmail;
using FCG.Application.UseCases.Users.GetUserById;
using FCG.Application.UseCases.Users.LoginUser;
using FCG.Application.UseCases.Users.UpdateUser;
using FCG.Domain.Interfaces;
using FCG.Domain.Services;
using FCG.Infra.Context;
using FCG.Infra.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using FCG.Application.Interfaces;
using FCG.Application.Services;

var builder = WebApplication.CreateBuilder(args);

#region Services
builder.Services.AddScoped<IGameRepository, GameRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserGameRepository, UserGameRepository>();
builder.Services.AddScoped<CreateUserHandler>();
builder.Services.AddScoped<CreateGameHandler>();
builder.Services.AddScoped<GetAllUsersHandler>();
builder.Services.AddScoped<GetAllGamesHandler>();
builder.Services.AddScoped<GetUserByIdHandler>();
builder.Services.AddScoped<GetGameByIdHandler>();
builder.Services.AddScoped<GetUserByEmailHandler>();
builder.Services.AddScoped<UpdateUserHandler>();
builder.Services.AddScoped<UpdateGameHandler>();
builder.Services.AddScoped<DeleteUserHandler>();
builder.Services.AddScoped<DeleteGameHandler>();
builder.Services.AddScoped<LoginUserHandler>();
builder.Services.AddScoped<BuyGameHandler>();
builder.Services.AddScoped <GetGamesByUserHandler>();
builder.Services.AddScoped<RemoveGameFromUserHandler>();
builder.Services.AddScoped<IUserCreationService, UserCreationService>();
builder.Services.AddScoped<IGameCreationService, GameCreationService>();
builder.Services.AddScoped<IUserValidationService, UserValidationService>();
builder.Services.AddScoped<IGameValidationService, GameValidationService>();
builder.Services.AddScoped<IUserGamePurchaseService, UserGamePurchaseService>();
builder.Services.AddScoped<IUserAuthenticationService, UserAuthenticationService>();
builder.Services.AddSingleton<ITokenService, TokenService>();
builder.Services.AddScoped<IUserGameRemovalService, UserGameRemovalService>();

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"] 
                ?? throw new InvalidOperationException("JWT:SecretKey is missing.")))
        };
    });

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "FCG API", Version = "v1" });
    
    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        Scheme = "bearer",
        BearerFormat = "JWT",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Description = "Token JWT",

        Reference = new OpenApiReference
        {
            Id = "Bearer",
            Type = ReferenceType.SecurityScheme
        }
    };

    c.AddSecurityDefinition("Bearer", jwtSecurityScheme);

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            jwtSecurityScheme, Array.Empty<string>()
        }
    });
});
#endregion

#region DbContext
builder.Services.AddDbContext<FCGDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection")),
        b => b.MigrationsAssembly("FCG.Infra")
    ));
#endregion

#region  Logger
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

builder.Host.UseSerilog(dispose: true);
#endregion
var app = builder.Build();
app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "FCG API v1");
    });
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();  


app.MapControllers();

app.Run();
