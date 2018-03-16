using System;

namespace Employees.DtoModels
{
    public class EmployeeDto
    {
        public EmployeeDto()
        {

        }

        public EmployeeDto(int id, DateTime birthday)
        {

        }

        public EmployeeDto(int id, string address)
        {

        }

        public EmployeeDto(string firstName, string lastName, decimal salary)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Salary = salary;
        }

        public EmployeeDto(string firstName, string lastName, decimal salary, DateTime birthday, string address)
            : this(firstName, lastName, salary)
        {
            this.Birthday = birthday;
            this.Address = address;
        }

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public decimal Salary { get; set; }

        public DateTime Birthday { get; set; }

        public string Address { get; set; }
    }
}
