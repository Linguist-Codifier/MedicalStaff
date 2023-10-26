using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

/* References: https://aka.ms/aspnetcore/swashbuckle */

namespace MedStaff.DaS.Communicator
{
    public class Engine
    {
        public static void Main(System.String[] args)
        {
            WebApplicationBuilder system = WebApplication.CreateBuilder(args);

            system.Services.AddControllers();
            system.Services.AddEndpointsApiExplorer();
            system.Services.AddSwaggerGen();

            WebApplication application = system.Build();

            if (application.Environment.IsDevelopment())
            {
                application.UseSwagger();
                application.UseSwaggerUI();
            }

            application.UseHttpsRedirection();

            application.UseAuthorization();

            application.MapControllers();

            application.Run();
        }
    }
}