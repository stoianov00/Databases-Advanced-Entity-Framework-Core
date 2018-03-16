using Employees.App.Interfaces;
using Employees.Services;
using System.Text;

namespace Employees.App.Commands
{
    internal class EmployeePersonalInfoCommand : ICommand
    {
        private readonly EmployeeService service;

        public EmployeePersonalInfoCommand(EmployeeService service)
        {
            this.service = service;
        }

        // EmployeePersonalInfo <id>
        public string Execute(params string[] args)
        {
            int id = int.Parse(args[0]);

            var employee = this.service.ById(id);

            var sb = new StringBuilder();

            sb.AppendLine($"ID: {employee.Id} - {employee.FirstName} {employee.LastName} - ${employee.Salary}")
                .AppendLine($"Birthday: {employee.Birthday.ToString("dd-MM-yyyy")}")
                .AppendLine($"Address: {employee.Address}");

            return sb.ToString();
        }
    }
}
