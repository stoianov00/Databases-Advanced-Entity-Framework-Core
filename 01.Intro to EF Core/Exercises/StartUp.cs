using System;
using System.Globalization;
using System.Linq;
using Exercises.Data;
using Exercises.Data.Models;

namespace Exercises
{
    public class StartUp
    {
        public static void Main()
        {
            var context = new SoftUniDbContext();

            // P03_EmployeesFullInformation(context);
            // P04_EmployeesWithSalaryOver50000(context);
            // P05_EmployeesFromResearchAndDevelopment(context);
            // P06_AddingNewAddressAndUpdateEmployee(context);
            // P07_EmployeesAndProjects(context);
            // P08_AddressesByTown(context);
            // P08_AddressesByTown(context);
            // P09_Employee147(context);
            // P10_DepartmentsWithMoreThan5Employees(context);
            // P11_FindLatest10Projects(context);
            // P12_IncreaseSalaries(context);
            // P13_FindEmployeesByFirstNameStartingWithSa(context);
            // P14_DeleteProjectById(context);
            // P15_RemoveTowns(context);
        }

        private static void P02_DatabaseFirst()
        {
            // First go to <Package Manager Console> and install packages below

            /*
                Install - Package Microsoft.EntityFrameworkCore.Tools
                Install - Package Microsoft.EntityFrameworkCore.SqlServer
                Install - Package Microsoft.EntityFrameworkCore.SqlServer.Design
            */

            // Also set the scaffold
            /* Scaffold - DbContext - Connection "Server=<ServerName>;Database=<DbName>;Integrated Security=True" - Provider Microsoft.EntityFrameworkCore.SqlServer - OutputDir Data / Models - Context <ContextName> */
        }

        private static void P03_EmployeesFullInformation(SoftUniDbContext context)
        {
            var employeesInfo = context.Employees
                .OrderBy(e => e.EmployeeId)
                .ToList();

            foreach (var employee in employeesInfo)
            {
                Console.WriteLine($"{employee.FirstName} {employee.LastName} {employee.MiddleName} {employee.JobTitle} {employee.Salary:F2}");
            }
        }

        private static void P04_EmployeesWithSalaryOver50000(SoftUniDbContext context)
        {
            var employeesInfo = context.Employees
                .Where(s => s.Salary > 50_000)
                .Select(e => e.FirstName)
                .OrderBy(e => e)
                .ToList();

            employeesInfo.ForEach(Console.WriteLine);
        }

        private static void P05_EmployeesFromResearchAndDevelopment(SoftUniDbContext context)
        {
            var employeesInfo = context.Employees
                .Where(e => e.Department.Name == "Research and Development")
                .OrderBy(e => e.Salary)
                .ThenByDescending(e => e.FirstName)
                .Select(e => new
                {
                    Name = $"{e.FirstName} {e.LastName}",
                    Department = e.Department.Name,
                    e.Salary
                })
                .ToList();

            foreach (var employee in employeesInfo)
            {
                Console.WriteLine($"{employee.Name} from {employee.Department} - ${employee.Salary:F2}");
            }
        }

        private static void P06_AddingNewAddressAndUpdateEmployee(SoftUniDbContext context)
        {
            var address = new Address
            {
                AddressText = "Vitoshka 15",
                TownId = 4
            };
            context.Addresses.Add(address);

            var employee = context.Employees
                .FirstOrDefault(e => e.LastName == "Nakov");

            employee.Address = address;
            context.SaveChanges();

            var result = context.Employees
                .OrderByDescending(e => e.AddressId)
                .Take(10)
                .Select(e => e.Address.AddressText)
                .ToList();

            result.ForEach(Console.WriteLine);
        }

        private static void P07_EmployeesAndProjects(SoftUniDbContext context)
        {
            var employeesInfo = context.Employees
                .Where(e => e.EmployeesProjects.Any(ep =>
                    ep.Project.StartDate.Year >= 2001 && ep.Project.StartDate.Year <= 2003))
                .Take(30)
                .Select(ep => new
                {
                    Name = $"{ep.FirstName} {ep.LastName}",
                    ManagerName = $"{ep.Manager.FirstName} {ep.Manager.LastName}",
                    Project = ep.EmployeesProjects.Select(e => new
                    {
                        e.Project.Name,
                        e.Project.StartDate,
                        e.Project.EndDate
                    })
                })
                .ToList();

            foreach (var e in employeesInfo)
            {
                Console.WriteLine($"{e.Name} - Manager: {e.ManagerName}");
                foreach (var p in e.Project)
                {
                    Console.Write($"--{p.Name} - {p.StartDate.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture)} - ");
                    if (p.EndDate == null)
                    {
                        Console.WriteLine("not finished");
                    }
                    else
                    {
                        Console.WriteLine(p.EndDate.Value.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture));
                    }
                }
            }
        }

        private static void P08_AddressesByTown(SoftUniDbContext context)
        {
            var addressesInfo = context.Addresses
                .OrderByDescending(e => e.Employees.Count)
                .ThenBy(t => t.Town.Name)
                .ThenBy(a => a.AddressText)
                .Take(10)
                .Select(e => new
                {
                    Address = e.AddressText,
                    Town = e.Town.Name,
                    Employees = $"{e.Employees.Count} employees"
                })
                .ToList();

            foreach (var address in addressesInfo)
            {
                Console.WriteLine($"{address.Address}, {address.Town} - {address.Employees}");
            }

        }

        private static void P09_Employee147(SoftUniDbContext context)
        {
            var employeesInfo = context.Employees
                .Where(e => e.EmployeeId == 147)
                .Select(e => new
                {
                    Name = $"{e.FirstName} {e.LastName}",
                    e.JobTitle,
                    Project = e.EmployeesProjects
                    .Select(p => new
                    {
                        p.Project.Name
                    })
                })
                .OrderBy(p => p.Project)
                .ToList();

            foreach (var employee in employeesInfo)
            {
                Console.WriteLine($"{employee.Name} - {employee.JobTitle}");
                foreach (var project in employee.Project.Skip(1))
                {
                    Console.WriteLine(project.Name);
                }
            }
        }

        private static void P10_DepartmentsWithMoreThan5Employees(SoftUniDbContext context)
        {
            var departmentInfo = context.Departments
                .Where(e => e.Employees.Count > 5)
                .OrderBy(e => e.Employees.Count)
                .ThenBy(e => e.Name)
                .Select(e => new
                {
                    DepartmentName = e.Name,
                    ManagerName = $"{e.Manager.FirstName} {e.Manager.LastName}",
                    EmployeeInfo = e.Employees.Select(ep => new
                    {
                        ep.FirstName,
                        ep.LastName,
                        ep.JobTitle
                    })
                })
                .ToList();

            foreach (var department in departmentInfo)
            {
                Console.WriteLine($"{department.DepartmentName} - {department.ManagerName}");
                foreach (var employees in department.EmployeeInfo
                    .OrderBy(ep => ep.FirstName).ThenBy(ep => ep.LastName))
                {
                    Console.WriteLine($"{employees.FirstName} {employees.LastName} - {employees.JobTitle}");
                }

                Console.WriteLine("----------");
            }
        }

        private static void P11_FindLatest10Projects(SoftUniDbContext context)
        {
            var projectsInfo = context.Projects
                .OrderByDescending(p => p.StartDate)
                .Take(10)
                .OrderBy(p => p.Name)
                .Select(p => new
                {
                    p.Name,
                    p.Description,
                    p.StartDate
                })
                .ToList();

            foreach (var project in projectsInfo)
            {
                Console.WriteLine(project.Name + Environment.NewLine);
                Console.WriteLine(project.Description + Environment.NewLine);
                Console.WriteLine(project.StartDate.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture) + Environment.NewLine);
            }
        }

        private static void P12_IncreaseSalaries(SoftUniDbContext context)
        {
            var employeesInfo = context.Employees
                .Where(d => d.Department.Name == "Engineering" || d.Department.Name == "Tool Design" ||
                            d.Department.Name == "Marketing" ||
                            d.Department.Name == "Information Services")
                .ToList();

            foreach (var employee in employeesInfo)
            {
                employee.Salary *= 1.12m;
            }

            foreach (var employee in employeesInfo
                .OrderBy(e => e.FirstName)
                .ThenBy(e => e.LastName))
            {
                Console.WriteLine($"{employee.FirstName} {employee.LastName} ({employee.Salary:F2})");
            }

            context.SaveChanges();
        }

        private static void P13_FindEmployeesByFirstNameStartingWithSa(SoftUniDbContext context)
        {
            var employeesInfo = context.Employees
                .Where(e => e.FirstName.StartsWith("Sa"))
                .OrderBy(e => e.FirstName)
                .ThenBy(e => e.LastName)
                .Select(e => new
                {
                    Name = $"{e.FirstName} {e.LastName}",
                    e.JobTitle,
                    e.Salary
                })
                .ToList();

            foreach (var employee in employeesInfo)
            {
                Console.WriteLine($"{employee.Name} - {employee.JobTitle} - (${employee.Salary:F2})");
            }
        }

        private static void P14_DeleteProjectById(SoftUniDbContext context)
        {
            int projectId = 2;

            var employeesProjects = context.EmployeesProjects
                .Where(ep => ep.ProjectId == projectId)
                .ToList();

            foreach (var employeesProject in employeesProjects)
            {
                context.EmployeesProjects.Remove(employeesProject);
            }

            var deleteProject = context.Projects.Find(2);
            context.Projects.Remove(deleteProject);

            context.SaveChanges();

            var projectNames = context.Projects
                .Take(10)
                .Select(p => p.Name);

            foreach (var projectName in projectNames)
            {
                Console.WriteLine(projectName);
            }
        }

        private static void P15_RemoveTowns(SoftUniDbContext context)
        {
            string townName = Console.ReadLine();

            context.Employees
                .Where(e => e.Address.Town.Name == townName)
                .ToList()
                .ForEach(e => e.AddressId = null);

            var addressesCount = context.Addresses
                .Count(a => a.Town.Name == townName);

            context.Addresses
                .Where(a => a.Town.Name == townName)
                .ToList()
                .ForEach(a => context.Addresses.Remove(a));

            context.Towns
                .Remove(context.Towns
                .SingleOrDefault(t => t.Name == townName));

            context.SaveChanges();

            Console.WriteLine($"{addressesCount} {(addressesCount == 1 ? "address" : "addresses")} in {townName} {(addressesCount == 1 ? "was" : "were")} deleted");

        }

    }
}