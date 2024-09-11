using InventoryOrderManagement.AggregateRoot;
using InventoryOrderManagement.Handler.Interfaces;
using InventoryOrderManagement.Handler.Services;
using InventoryOrderManagement.Repository.Data;
using InventoryOrderManagement.Repository.GenericRepositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configure DbContext with SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("InventoryOrderManagementSystem"))); // Update connection string name appropriately


//Exportservies
builder.Services.AddScoped<OrderExportService>();
builder.Services.AddScoped<OrderDetailExportService>();
// Register handlers
builder.Services.AddScoped<IOrderHandler, OrderHandler>();
builder.Services.AddScoped<IOrderDetailHandler, OrderDetailHandler>();






//builder.Services.AddScoped<OrderDetailExportService>();






// Register the generic repository
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

// Note: Since you mentioned not using AutoMapper anymore, ensure all mapping configurations are removed.

// Build the app
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
