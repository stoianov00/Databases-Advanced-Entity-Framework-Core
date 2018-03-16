using System.Collections.Generic;

namespace Employees.DtoModels
{
    public class ManagerDto
    {
        public ManagerDto()
        {
            this.ManagedEmployees = new HashSet<EmployeeDto>();
        }

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public ICollection<EmployeeDto> ManagedEmployees { get; set; }
    }
}
