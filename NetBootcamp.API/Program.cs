using Bootcamp.Repository;
using Bootcamp.Service;
using Bootcamp.Service.Products.Configurations;
using Bootcamp.Service.Products.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetBootcamp.API.Extensions;
using NetBootcamp.API.Filters;
using NetBootcamp.API.Roles;
using NetBootcamp.API.Users;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);





builder.Services.AddScoped<NotFoundFilter>();



builder.Services.AddControllers(x=> x.Filters.Add<ValidationFilter>());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRepository(builder.Configuration);
builder.Services.AddService();

// Add services to the container.


// DI(Dependency Injection) Container framework
// IoC ( Inversion Of Container)  framework
//  - Dependency Inversion / Inversion Of Control Principles
//  - Dependency Injection Design Pattern


// 1. AddSingleton
// 2. AddScoped (*)
// 3. AddTransient




builder.Services.AddSingleton<PriceCalculator>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();


var app = builder.Build();

app.SeedDatabase();

// Configure the HTTP request pipeline.
app.AddMiddlewares();

app.Run();