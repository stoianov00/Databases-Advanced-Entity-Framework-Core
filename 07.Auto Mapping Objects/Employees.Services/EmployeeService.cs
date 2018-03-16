using AutoMapper;
using Employees.Data;
using Employees.DtoModels;
using Employees.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Employees.Services
{
    public class EmployeeService
    {
        private readonly EmployeesDbContext context;

        public EmployeeService(EmployeesDbContext context)
        {
            this.context = context;
        }

        public EmployeeDto ById(int employeeId)
        {
            var employee = this.context.Employees
                .Find(employeeId);

            var employeeDto = Mapper.Map<EmployeeDto>(employee);

            return employeeDto;
        }

        public void AddEmployee(EmployeeDto dto)
        {
            var employee = Mapper.Map<Employee>(dto);

            this.context.Employees.Add(employee);
            this.context.SaveChanges();
        }

        public string SetBirthday(int employeeId, DateTime date)
        {
            var employee = this.context.Employees.Find(employeeId);
            employee.Birthday = date;

            this.context.SaveChanges();

            return $"{employee.FirstName} {employee.LastName}";
        }

        public string SetAddress(int employeeId, string address)
        {
            var employee = this.context.Employees.Find(employeeId);
            employee.Address = address;

            this.context.SaveChanges();

            return $"{employee.FirstName} {employee.LastName}";
        }

        public ManagerDto ManagerInfo(int id)
        {
            var manager = this.context.Employees
                .Include(e => e.ManagedEmployees)
                .SingleOrDefault(e => e.Id == id);

            var managerDto = Mapper.Map<ManagerDto>(manager);

            return managerDto;
        }

        public EmployeeDto[] SetManager(int employeeId, int managerId)
        {
            var employee = this.context.Employees.Find(employeeId);
            var manager = this.context.Employees.Find(managerId);

            employee.Manager = manager;

            this.context.SaveChanges();

            var employeeDto = Mapper.Map<EmployeeDto>(employee);
            var employeeDtoManager = Mapper.Map<EmployeeDto>(manager);

            var managerDto = new ManagerDto();

            return new[] { employeeDto, employeeDtoManager };
        }
    }
}