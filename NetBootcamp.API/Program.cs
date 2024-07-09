using Bootcamp.Repository;
using Bootcamp.Service;
using Bootcamp.Service.Products.Configurations;
using Bootcamp.Service.Products.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetBootcamp.API.Filters;
using NetBootcamp.API.Roles;
using NetBootcamp.API.Users;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(x => {
    x.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer") ,x =>
    {
        x.MigrationsAssembly(typeof(RepositoryAssembly).Assembly.GetName().Name);
    });
});


builder.Services.Configure<ApiBehaviorOptions>(x =>
{
    x.SuppressModelStateInvalidFilter = true;
});


builder.Services.AddScoped<NotFoundFilter>();

builder.Services.AddAutoMapper(typeof(ServiceAssembly).Assembly);

builder.Services.AddControllers(x=> x.Filters.Add<ValidationFilter>());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add services to the container.


// DI(Dependency Injection) Container framework
// IoC ( Inversion Of Container)  framework
//  - Dependency Inversion / Inversion Of Control Principles
//  - Dependency Injection Design Pattern


// 1. AddSingleton
// 2. AddScoped (*)
// 3. AddTransient

builder.Services.AddProductService();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddSingleton<PriceCalculator>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();


var app = builder.Build();

app.SeedDatabase();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();