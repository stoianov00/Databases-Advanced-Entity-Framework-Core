using Employees.App.Interfaces;
using Employees.Services;

namespace Employees.App.Commands
{
    internal class SetManagerCommand : ICommand
    {
        private readonly EmployeeService service;

        public SetManagerCommand(EmployeeService service)
        {
            this.service = service;
        }

        // SetManager <employeeId> <managerId>
        public string Execute(params string[] args)
        {
            int employeeId = int.Parse(args[0]);
            int managerId = int.Parse(args[1]);

            var employee = this.service.SetManager(employeeId, managerId);

            return $"{employee[1].FirstName} {employee[1].LastName} was set as manager to {employee[0].FirstName} {employee[0].LastName}";
        }
    }
}
