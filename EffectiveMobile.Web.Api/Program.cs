using EffectiveMobile.Common.EntityModel.Sqlite;
using EffectiveMobile.Repositories;
using EffectiveMobile.Repositories.Interfaces;
using EffectiveMobile.Services;
using EffectiveMobile.Services.Interfaces;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddDbContext<EffectiveMobileContext>();

builder.Services.AddScoped<IOrdersRepository, OrdersRepository>();

builder.Services.AddScoped<IOrdersService, OrdersService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();