using Employees.App.Interfaces;
using Employees.Services;
using System;
using System.Globalization;
 
namespace Employees.App.Commands
{
    internal class SetBirthdayCommand : ICommand
    {
        private readonly EmployeeService service;

        public SetBirthdayCommand(EmployeeService service)
        {
            this.service = service;
        }

        // SetBirthday <employeeId> <date: 'dd-mm-yyyy'>
        public string Execute(params string[] args)
        {
            int id = int.Parse(args[0]);
            DateTime birthday = DateTime.ParseExact(args[1], "dd-MM-yyyy", CultureInfo.InvariantCulture);

            var employeeName = this.service.SetBirthday(id, birthday);

            return $"{employeeName}'s birthday was set to {args[1]} !";
        }
    }
}
