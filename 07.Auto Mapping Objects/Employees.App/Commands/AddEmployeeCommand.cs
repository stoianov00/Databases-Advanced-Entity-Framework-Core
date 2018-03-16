using Employees.App.Interfaces;
using Employees.DtoModels;
using Employees.Services;

namespace Employees.App.Commands
{
    internal class AddEmployeeCommand : ICommand
    {
        private readonly EmployeeService service;

        public AddEmployeeCommand(EmployeeService service)
        {
            this.service = service;
        }

        // AddEmployee <firstName> <lastName> <salary>
        public string Execute(params string[] args)
        {
            string firstName = args[0];
            string lastName = args[1];
            decimal salary = decimal.Parse(args[2]);

            var employeeDto = new EmployeeDto(firstName, lastName, salary);

            this.service.AddEmployee(employeeDto);

            return $"Employee {firstName} {lastName} successfully added !";
        }
    }
}
