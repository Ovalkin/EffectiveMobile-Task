using EffectiveMobile.Common.EntityModel.Sqlite;
using EffectiveMobile.Repositories;
using EffectiveMobile.Repositories.Interfaces;
using EffectiveMobile.Services;
using EffectiveMobile.Services.Interfaces;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var logPath = builder.Configuration["AppSettings:DeliveryLogPath"]!;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .Enrich.FromLogContext()
    .WriteTo.File( logPath+"/deliveryLog-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddDbContext<EffectiveMobileContext>();

builder.Services.AddScoped<IOrdersRepository, OrdersRepository>();

builder.Services.AddScoped<IOrdersService, OrdersService>();

builder.Host.UseSerilog();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();