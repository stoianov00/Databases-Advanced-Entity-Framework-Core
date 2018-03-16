using Employees.App.Interfaces;
using Employees.Services;

namespace Employees.App.Commands
{
    internal class EmployeeInfoCommand : ICommand
    {
        private readonly EmployeeService service;

        public EmployeeInfoCommand(EmployeeService service)
        {
            this.service = service;
        }

        // EmployeeInfo <id>
        public string Execute(params string[] args)
        {
            int id = int.Parse(args[0]);
            var employee = this.service.ById(id);

            return $"ID: {employee.Id} - {employee.FirstName} {employee.LastName} - {employee.Salary:F2}";
        }

    }
}
