using Employees.App.Interfaces;
using Employees.Services;
using System.Text;

namespace Employees.App.Commands
{
    internal class ManagerInfoCommand : ICommand
    {
        private readonly EmployeeService service;

        public ManagerInfoCommand(EmployeeService service)
        {
            this.service = service;
        }

        public string Execute(params string[] args)
        {
            int id = int.Parse(args[0]);

            var managerInfo = this.service.ManagerInfo(id);

            var sb = new StringBuilder();
            sb.AppendLine($"{managerInfo.FirstName} {managerInfo.LastName}");

            foreach (var employee in managerInfo.ManagedEmployees)
            {
                sb.AppendLine($"- {employee.FirstName} {employee.LastName} - ${employee.Salary}");
            }

            return sb.ToString();
        }

    }
}
