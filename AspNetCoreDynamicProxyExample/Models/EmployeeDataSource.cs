using System;
using System.Collections.Generic;
using System.Linq;
using AspNetCoreDynamicProxyExample.DAL;
using Microsoft.Extensions.Logging;

namespace AspNetCoreDynamicProxyExample.Models
{
    public class EmployeeDataSource : IEmployeeDataSource
    {
        private readonly ILogger<EmployeeDataSource> _logger;

        public EmployeeDataSource(ILogger<EmployeeDataSource> logger)
        {
            _logger = logger;
        }

        public IEnumerable<Employee> GetEmployees(string filterText)
        {
            try
            {
                using var context = new EmployeeContext();

                var employees = context.Employees.Where(e => e.Name.Contains(filterText)).ToList();

                _logger.LogInformation($"Obtained {employees.Count} employees based on {filterText}");

                return employees;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc, "Error obtaining employees");

                throw;
            }
        }

        public IEnumerable<Employee> GetEmployeesSimple(string filterText)
        {
            using var context = new EmployeeContext();

            var employees = context.Employees.Where(e => e.Name.Contains(filterText)).ToList();

            return employees;
        }
    }
}