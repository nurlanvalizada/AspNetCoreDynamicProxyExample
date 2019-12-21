using System.Collections.Generic;

namespace AspNetCoreDynamicProxyExample.Models
{
    public interface IEmployeeDataSource
    {
        //IEnumerable<Employee> GetEmployees(string filterText);

        IEnumerable<Employee> GetEmployeesSimple(string filterText);
    }
}