using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using urlshortener.Data;
using urlshortener.Interfaces;
using urlshortener.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection")!, new MySqlServerVersion(new Version(9, 3, 0))));

builder.Services.AddScoped<IUrlRepository, UrlRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
