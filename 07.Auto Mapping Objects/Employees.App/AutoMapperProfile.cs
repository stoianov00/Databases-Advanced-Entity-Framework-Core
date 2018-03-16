using AutoMapper;
using Employees.DtoModels;
using Employees.Models;

namespace Employees.App
{
    internal class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Employee, EmployeeDto>();
            CreateMap<EmployeeDto, Employee>();

            CreateMap<Employee, ManagerDto>();
            CreateMap<ManagerDto, Employee>();
        }

    }
}
