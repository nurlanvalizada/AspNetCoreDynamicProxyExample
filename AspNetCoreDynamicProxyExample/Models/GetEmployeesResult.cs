using System.Collections.Generic;
using AspNetCoreDynamicProxyExample.Models.Attributes;

namespace AspNetCoreDynamicProxyExample.Models
{
    public class GetEmployeesResult
    {
        [LogCount("Number of employees")]
        public IEnumerable<Employee> Employees { get; }

        public GetEmployeesResult(IEnumerable<Employee> employees)
        {
            Employees = employees;
        }
    }
}
