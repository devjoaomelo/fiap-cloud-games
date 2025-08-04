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
using Serilog.Events;
using Serilog.Sinks.ApplicationInsights.TelemetryConverters;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables();

#region Services
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
builder.Services.AddScoped<GetGamesByUserHandler>();
builder.Services.AddScoped<RemoveGameFromUserHandler>();

builder.Services.AddScoped<IUserCreationService, UserCreationService>();
builder.Services.AddScoped<IGameCreationService, GameCreationService>();
builder.Services.AddScoped<IUserValidationService, UserValidationService>();
builder.Services.AddScoped<IGameValidationService, GameValidationService>();
builder.Services.AddScoped<IUserGamePurchaseService, UserGamePurchaseService>();
builder.Services.AddScoped<IUserAuthenticationService, UserAuthenticationService>();
builder.Services.AddSingleton<ITokenService, TokenService>();
builder.Services.AddScoped<IUserGameRemovalService, UserGameRemovalService>();
builder.Services.AddScoped<IUserQueryService, UserQueryService>();
builder.Services.AddScoped<IUserCommandService, UserCommandService>();
builder.Services.AddScoped<IGameQueryService, GameQueryService>();
builder.Services.AddScoped<IGameCommandService, GameCommandService>();
builder.Services.AddScoped<IGameRepository, GameRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserGameRepository, UserGameRepository>();
builder.Services.AddScoped<IUserGameQueryService, UserGameQueryService>();
builder.Services.AddScoped<IUserGameCommandService, UserGameCommandService>();
builder.Services.AddScoped<IUserSelfService, UserSelfService>();

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
        builder.Configuration.GetConnectionString("Default"),
        new MySqlServerVersion(new Version(8, 0, 36)),
        mysqlOpts =>
        {
            mysqlOpts.MigrationsAssembly("FCG.Infra");

            mysqlOpts.EnableRetryOnFailure(
                maxRetryCount: 5,
                maxRetryDelay: TimeSpan.FromSeconds(10),
                errorNumbersToAdd: null);    
        }));
#endregion

#region Logger
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .MinimumLevel.Override("System", LogEventLevel.Warning)
    .WriteTo.Console()
    .WriteTo.ApplicationInsights(
        builder.Configuration["APPLICATIONINSIGHTS_CONNECTION_STRING"],
        new TraceTelemetryConverter())
    .CreateLogger();

builder.Host.UseSerilog(dispose: true);
#endregion

var app = builder.Build();

if (app.Environment.IsDevelopment() || app.Environment.EnvironmentName == "docker")
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "FCG API v1");
    });
}

app.UseExceptionMiddleware();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

var runMigrations = Environment.GetEnvironmentVariable("RUN_MIGRATION");

if (!string.IsNullOrWhiteSpace(runMigrations) && runMigrations.Equals("true", StringComparison.OrdinalIgnoreCase))
{
    using var scope = app.Services.CreateScope();
    try
    {
        var db = scope.ServiceProvider.GetRequiredService<FCGDbContext>();
        db.Database.Migrate();
    }
    catch (Exception ex)
    {
        Log.Fatal(ex, "Erro ao aplicar migrations no banco de dados.");
        throw;
    }
}
Console.WriteLine("Conectando ao banco: " + builder.Configuration.GetConnectionString("Default"));
Console.WriteLine(">>>>> APP CONFIG LOADED <<<<<");
Console.WriteLine("Ambiente: " + builder.Environment.EnvironmentName);
Console.WriteLine("ConnString: " + builder.Configuration.GetConnectionString("Default"));

app.Run();
