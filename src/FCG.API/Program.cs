using FCG.API.Middleware;
using FCG.Application.UseCases.Users.CreateUser;
using FCG.Application.UseCases.Users.DeleteUser;
using FCG.Application.UseCases.Users.GetAllUsers;
using FCG.Application.UseCases.Users.GetUserByEmail;
using FCG.Application.UseCases.Users.GetUserById;
using FCG.Application.UseCases.Users.UpdateUser;
using FCG.Domain.Interfaces;
using FCG.Infra.Context;
using FCG.Infra.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

#region Services
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<CreateUserHandler>();
builder.Services.AddScoped<GetAllUsersHandler>();
builder.Services.AddScoped<GetUserByIdHandler>();
builder.Services.AddScoped<GetUserByEmailHandler>();
builder.Services.AddScoped<UpdateUserHandler>();
builder.Services.AddScoped<DeleteUserHandler>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
#endregion

#region DbContext
builder.Services.AddDbContext<FCGDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection")),
        b => b.MigrationsAssembly("FCG.Infra")
    ));
#endregion

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionMiddleware();

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();