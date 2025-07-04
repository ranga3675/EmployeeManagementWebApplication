﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
//builder.Services.AddDbContext<MongoDbWebApplicationContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("MongoDbWebApplicationContext") ?? throw new InvalidOperationException("Connection string 'MongoDbWebApplicationContext' not found.")));

builder.Services.AddHttpClient("MongoDbService", client =>
{
    //client.BaseAddress = new Uri("https://localhost:7021/");
});
//builder.Services.AddScoped<MongoDbWebApplicationContext>();
builder.Services.AddScoped<MongoDbWebApplication.Interfaces.IService, MongoDbWebApplication.Services.Service>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
