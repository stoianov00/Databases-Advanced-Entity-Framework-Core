using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Linq;
using System.Text;
using Workshop.App.Core;
using Workshop.App.Core.Utilities;
using Workshop.App.Interfaces;
using Workshop.App.IO;
using Workshop.Data;
using Workshop.Services;

namespace Workshop.App
{
    public class StartUp
    {
        public static void Main()
        {
            DbTools.ResetDb();

            IReader reader = new ConsoleReader();
            IWriter writer = new ConsoleWriter();

            var serviceProvider = ConfigureServices();
            var engine = new Engine(serviceProvider, reader, writer);
            engine.Run();
        }

        private static IServiceProvider ConfigureServices()
        {
            string path = @"C:\Users\Ss\Documents\Visual Studio 2017\Projects\C# DATABASES ADVANCED - ENTITY FRAMEWORK\WorkShop\connection.txt";
            var connection = File.ReadAllLines(path, Encoding.UTF8).FirstOrDefault();

            var serviceCollection = new ServiceCollection();
            serviceCollection.AddDbContext<TeamBuilderDbContext>(options => options.UseSqlServer(connection));
            serviceCollection.AddTransient<UserService>();
            serviceCollection.AddTransient<EventService>();
            serviceCollection.AddTransient<TeamService>();
            serviceCollection.AddTransient<InvitationService>();
            serviceCollection.AddAutoMapper(cfg => cfg.AddProfile<AutoMapperProfile>());

            var serviceProvider = serviceCollection.BuildServiceProvider();

            return serviceProvider;
        }
    }
}
