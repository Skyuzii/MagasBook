using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MagasBook.Domain.Entities.Account;
using MagasBook.Infrastructure.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MagasBook.WebApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using var score = host.Services.CreateScope();
            var services = score.ServiceProvider;

            await SetDefaultDataAsync(services);
            
            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });

        private static async Task SetDefaultDataAsync(IServiceProvider serviceProvider)
        {
            try
            {
                var context = serviceProvider.GetService<ApplicationDbContext>();
                await context.Database.MigrateAsync();

                var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();
                var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();

                await ApplicationDbContextSeed.SetDefaultDataAsync(roleManager, userManager);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
    }
}