using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace AspNetCoreDynamicProxyExample.Models.Decorators
{
    public class EmployeeDataSourceDecorator : IEmployeeDataSource
    {
        private readonly IEmployeeDataSource _employeeDataSource;
        private readonly ILogger<EmployeeDataSourceDecorator> _logger;

        public EmployeeDataSourceDecorator(IEmployeeDataSource employeeDataSource, ILogger<EmployeeDataSourceDecorator> logger)
        {
            _employeeDataSource = employeeDataSource;
            _logger = logger;
        }

        public IEnumerable<Employee> GetEmployeesSimple(string filterText)
        {
            _logger.LogInformation("Some details");

            try
            {
                var result = _employeeDataSource.GetEmployeesSimple(filterText);

                var employeesSimple = result.ToList();

                _logger.LogInformation($"Number of documents returned is {employeesSimple.Count()}");

                return employeesSimple;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc, "Error obtaining employees");
                throw;
            }
        }
    }
}
