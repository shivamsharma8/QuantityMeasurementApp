using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using QuantityMeasurementApp.ExceptionHandling;
using QuantityMeasurementBusinessLayer;
using QuantityMeasurementBusinessLayer.Interfaces;
using QuantityMeasurementBusinessLayer.Services;
using QuantityMeasurementRepositoryLayer;
using QuantityMeasurementRepositoryLayer.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace QuantityMeasurementApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            // ✅ CORS FIX (Netlify + Localhost)
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend",
                    policy =>
                    {
                        policy.WithOrigins(
                                "https://stunning-unicorn-7e64ba.netlify.app",
                                "http://localhost:4200",
                                "http://localhost:5173"
                            )
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });

            // Database
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<QuantityMeasurementDbContext>(options =>
                options.UseSqlServer(connectionString));

            // Services
            builder.Services.AddScoped<IQuantityMeasurementRepository, QuantityMeasurementEfRepository>();
            builder.Services.AddScoped<IQuantityMeasurementService, QuantityMeasurementService>();

            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IHashingService, HashingService>();
            builder.Services.AddScoped<ISecurityService, SecurityService>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IUserService, UserService>();

            // JWT
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = builder.Configuration["Jwt:Issuer"],
                        ValidAudience = builder.Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? "")
                        )
                    };
                });

            builder.Services.AddAuthorization();

            // Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Quantity Measurement API",
                    Version = "v1"
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });

            var app = builder.Build();

            // Middleware
            app.UseMiddleware<GlobalExceptionMiddleware>();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.MapGet("/", context =>
            {
                context.Response.Redirect("/swagger");
                return Task.CompletedTask;
            });

            app.UseHttpsRedirection();

            // ✅ CORS MUST BE BEFORE AUTH
            app.UseCors("AllowFrontend");

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
