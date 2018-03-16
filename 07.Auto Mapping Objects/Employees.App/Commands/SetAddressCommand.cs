using Employees.App.Interfaces;
using Employees.Services;

namespace Employees.App.Commands
{
    internal class SetAddressCommand : ICommand
    {
        private readonly EmployeeService service;

        public SetAddressCommand(EmployeeService service)
        {
            this.service = service;
        }

        // SetAddress <id> <address>
        public string Execute(params string[] args)
        {
            int id = int.Parse(args[0]);
            string address = args[1];

            var employeeName = this.service.SetAddress(id, address);

            return $"{employeeName}'s address was set to {address} !";
        }
    }
}
