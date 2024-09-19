using Core.Core;
using Core.DatabaseHandler;
using Core.Interfaces;
using Core.Repositories;
using Core.Repositories.Interfaces;
using Core.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<CoreDbContext>(x =>
    x.UseNpgsql("Host=localhost;Port=5432;Database=rpg;Username=postgres;Password=qRV5jSQXsn2&$6")
    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

builder.Services.AddScoped<IEnemyRepository, EnemyRepository>();
builder.Services.AddScoped<IInventoryRepository, InventoryRepository>();
builder.Services.AddScoped<IPlayerRepository, PlayerRepository>();
builder.Services.AddScoped<IStoreRepository, StoreRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IStoreService, StoreService>();
builder.Services.AddScoped<IPlayerInfoService, PlayerService>();
builder.Services.AddScoped<IBattleService, BattleService>();
builder.Services.AddScoped<IInventoryService, InventoryService>();
builder.Services.AddScoped<IAuthenticationUserService, AuthenticationUserService>();

var app = builder.Build();

app.MapControllerRoute(
    name: "authentication",
    pattern: "AuthenticationUser/{action=Index}/{id?}",
    defaults: new { controller = "AuthenticationUser" });

app.MapControllerRoute(
    name: "battle",
    pattern: "Battle/{action=Index}/{id?}",
    defaults: new { controller = "Battle" });

app.MapControllerRoute(
    name: "inventory",
    pattern: "Inventory/{action=Index}/{id?}",
    defaults: new { controller = "Inventory" });

app.MapControllerRoute(
    name: "player",
    pattern: "Player/{action=Index}/{id?}",
    defaults: new { controller = "Player" });

app.MapControllerRoute(
    name: "store",
    pattern: "Store/{action=Index}/{id?}",
    defaults: new { controller = "Store" });

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
