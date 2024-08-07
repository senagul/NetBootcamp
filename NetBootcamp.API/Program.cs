using Bootcamp.Repository;
using Bootcamp.Repository.Identities;
using Bootcamp.Service;
using Bootcamp.Service.Products.Configurations;
using Bootcamp.Service.Products.Helpers;
using Bootcamp.Service.Token;
using Bootcamp.Service.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NetBootcamp.API.Extensions;
using NetBootcamp.API.Filters;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);




builder.Services.AddScoped<NotFoundFilter>();



builder.Services.AddControllers(x=> x.Filters.Add<ValidationFilter>());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRepository(builder.Configuration);
builder.Services.AddService(builder.Configuration);
builder.Services.AddScoped<IAuthorizationHandler,OverAgeRequirementHandler>();

builder.Services.AddAuthorization(x => 
{
    x.AddPolicy("Over18AgePolicy", y => 
    { 
        y.AddRequirements(new OverAgeRequirement() { Age=18 }); 
    });   

    x.AddPolicy("UpdatePolicy", y => 
    { 
        y.RequireClaim("update", "true"); 
    });
});


builder.Services.AddSingleton<PriceCalculator>();


var app = builder.Build();

app.SeedDatabase();
await app.SeedIdentityData(); 

// Configure the HTTP request pipeline.
app.AddMiddlewares();

app.Run();