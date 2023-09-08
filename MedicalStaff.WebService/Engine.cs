using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using MedicalRecordsSystem.WebService.Core.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MedicalRecordsSystem.WebService.Core.Infrastructure.MedicalRecords;

namespace MedicalRecordsSystem.WebService
{
    /// <summary>
    /// Application entry gate.
    /// </summary>
    public sealed class Engine
    {
        /// <summary>
        /// Application startup entry point.
        /// </summary>
        /// <param name="args"></param>
        public static void Main(String[] args)
        {
            WebApplicationBuilder Builder = WebApplication.CreateBuilder(args);

            Builder.Services.AddDbContext<SystemDbContext>(options =>
            {
                options.UseSqlServer(Builder.Configuration.GetConnectionString("DatabaseConnectionString")).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                //options.UseInMemoryDatabase("development").UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });

            Builder.Services.AddControllers();
            Builder.Services.AddScoped<MedicalRecordsDbContext>();
            Builder.Services.AddEndpointsApiExplorer();
            Builder.Services.AddSwaggerGen();
            Builder.Services.AddCors();

            WebApplication Application = Builder.Build();

            if (Application.Environment.IsDevelopment())
            {
                Application.UseSwagger();
                Application.UseSwaggerUI();
            }

            Application.UseHttpsRedirection();

            Application.UseAuthorization();

            Application.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            Application.MapControllers();

            Application.Run();
        }
    }
}