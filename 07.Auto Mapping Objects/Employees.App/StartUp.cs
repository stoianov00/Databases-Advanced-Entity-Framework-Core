using System;
using Microsoft.Extensions.DependencyInjection;
using Employees.Data;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.IO;
using System.Linq;
using Employees.Services;
using AutoMapper;
using Employees.App.Interfaces;
using Employees.App.IO;

namespace Employees.App
{
    public class StartUp
    {
        public static void Main()
        {
            IReader reader = new ConsoleReader();
            IWriter writer = new ConsoleWriter();

            var serviceProvider = ConfigureServices();
            var engine = new Engine(serviceProvider, reader, writer);
            engine.Run();
        }

        private static IServiceProvider ConfigureServices()
        {
            string path = @"C:\Users\Ss\Documents\Visual Studio 2017\Projects\C# DATABASES ADVANCED - ENTITY FRAMEWORK\07.Auto Mapping Objects\connection.txt";
            var connection = File.ReadAllLines(path, Encoding.UTF8).FirstOrDefault();

            var serviceCollection = new ServiceCollection();
            serviceCollection.AddDbContext<EmployeesDbContext>(options => options.UseSqlServer(connection));
            serviceCollection.AddTransient<EmployeeService>();
            serviceCollection.AddAutoMapper(cfg => cfg.AddProfile<AutoMapperProfile>());

            var serviceProvider = serviceCollection.BuildServiceProvider();

            return serviceProvider;
        }
    }
}