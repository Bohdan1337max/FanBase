using Microsoft.EntityFrameworkCore;
using WarehouseManagementSystem.Controllers;
using WarehouseManagementSystem.DataBase;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<WmsDbContext>(
    o => o.UseNpgsql(builder.Configuration.GetConnectionString("wms_project_db")));
builder.Services.AddScoped<ItemController>();

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

