using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using QuantityMeasurementBusinessLayer.Interfaces;
using QuantityMeasurementBusinessLayer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddScoped<IQuantityMeasurementService, QuantityMeasurementService>();
builder.Services.AddHttpClient(); 

var app = builder.Build();

app.UseAuthorization();
app.MapControllers();

app.Run();
