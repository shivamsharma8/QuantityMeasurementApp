using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using QuantityMeasurementApp.ExceptionHandling;
using QuantityMeasurementBusinessLayer;
using QuantityMeasurementBusinessLayer.Interfaces;
using QuantityMeasurementRepositoryLayer;

namespace QuantityMeasurementApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();

            // Configure Database
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<QuantityMeasurementDbContext>(options =>
                options.UseSqlServer(connectionString));

            // Register Repositories and Services
            builder.Services.AddScoped<IQuantityMeasurementRepository, QuantityMeasurementEfRepository>();
            builder.Services.AddScoped<IQuantityMeasurementService, QuantityMeasurementService>();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Quantity Measurement API",
                    Version = "v1",
                    Description = "API for comparing, converting, and calculating quantities."
                });

                var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = System.IO.Path.Combine(System.AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

                var modelXmlFile = "QuantityMeasurementModelLayer.xml";
                var modelXmlPath = System.IO.Path.Combine(System.AppContext.BaseDirectory, modelXmlFile);
                if (System.IO.File.Exists(modelXmlPath))
                {
                    c.IncludeXmlComments(modelXmlPath);
                }
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.UseMiddleware<GlobalExceptionMiddleware>();

            // Enable Swagger globally for this project
            app.UseSwagger();
            app.UseSwaggerUI();

            // Redirect root to swagger
            app.MapGet("/", context =>
            {
                context.Response.Redirect("/swagger");
                return System.Threading.Tasks.Task.CompletedTask;
            });

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}