using System.Linq;
using AspNetCoreDynamicProxyExample.DAL;
using AspNetCoreDynamicProxyExample.Models.GenericHandlers;
using Microsoft.Extensions.Logging;

namespace AspNetCoreDynamicProxyExample.Models
{
    public class EmployeeDataSourceQueryHandler : IQueryHandler<string, GetEmployeesResult>
    {
        private readonly ILogger<EmployeeDataSourceQueryHandler> _logger;

        public EmployeeDataSourceQueryHandler(ILogger<EmployeeDataSourceQueryHandler> logger)
        {
            _logger = logger;
        }

        public GetEmployeesResult Handle(string query)
        {
            using var context = new EmployeeContext();

            var employees = context.Employees.Where(e => e.Name.Contains(query)).ToList();

            return new GetEmployeesResult(employees);
        }
    }
}