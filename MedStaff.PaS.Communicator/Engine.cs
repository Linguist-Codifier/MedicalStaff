using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;

/* References: https://aka.ms/aspnetcore/swashbuckle */

namespace MedStaff.DaS.Communicator
{
    /// <summary>
    /// 
    /// </summary>
    [DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
    public class Engine
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
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

        private System.String GetDebuggerDisplay() => this.ToString() ?? typeof(Engine).Assembly.GetName().FullName;
    }
}