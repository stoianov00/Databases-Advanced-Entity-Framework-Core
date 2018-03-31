using System;
using System.Linq;
using System.Reflection;
using Workshop.App.Interfaces;

namespace Workshop.App.Core
{
    internal class CommandParser
    {
        public static ICommand Parse(IServiceProvider serviceProvider, string commandName)
        {
            var assembly = Assembly.GetExecutingAssembly();

            var commandTypes = assembly.GetTypes()
                .Where(c => c.GetInterfaces().Contains(typeof(ICommand)));

            var commandType = commandTypes
                .SingleOrDefault(t => t.Name == $"{commandName}Command");

            if (commandType == null)
            {
                throw new InvalidOperationException("Invalid command !");
            }

            var constructor = commandType.GetConstructors().FirstOrDefault();
            var constructorParams = constructor.GetParameters()
                .Select(p => p.ParameterType)
                .ToArray();

            var constructorArgs = constructorParams
                .Select(serviceProvider.GetService)
                .ToArray();

            var command = (ICommand)constructor.Invoke(constructorArgs);

            return command;
        }

    }
}
