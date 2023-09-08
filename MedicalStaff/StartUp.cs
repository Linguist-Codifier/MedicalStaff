using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace MedicalStaff
{
    public class StartUp
    {
        public static void Main(String[] args)
        {
            WebApplicationBuilder Builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            Builder.Services.AddControllersWithViews();

            WebApplication application = Builder.Build();

            // Configure the HTTP request pipeline.
            if (!application.Environment.IsDevelopment())
            {
                application.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                application.UseHsts();
            }

            application.UseHttpsRedirection();
            application.UseStaticFiles();

            application.UseRouting();

            application.UseAuthorization();

            application.MapControllerRoute(name: "default", pattern: "{controller=StartUp}/{action=SignIn}");

            application.Run();
        }
    }
}