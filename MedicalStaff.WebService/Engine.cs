using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using MedicalStaff.WebService.Core.Data;
using Microsoft.Extensions.DependencyInjection;
using MedicalStaff.WebService.Core.Interfaces;
using MedicalStaff.WebService.Core.Models.Db.Physician;
using MedicalStaff.WebService.Core.Services.Accounts;

namespace MedicalStaff.WebService
{
    /// <summary>
    /// Application entry gate.
    /// </summary>
    public sealed class Engine
    {
        /// <summary>
        /// Application startup entry point.
        /// </summary>
        /// <param name="args">Start up arguments.</param>
        public static void Main(String[] args)
        {
            WebApplicationBuilder Builder = WebApplication.CreateBuilder(args);

            Builder.Services.AddDbContext<SystemDbContext>(options =>
            {
                //options.UseSqlServer(Builder.Configuration.GetConnectionString("DatabaseConnectionString")).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                options.UseInMemoryDatabase("development").UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });

            Builder.Services.AddControllers();
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