using System;
using System.Collections.Generic;
using AspNetCoreDynamicProxyExample.Models;
using Microsoft.AspNetCore.Connections;

namespace AspNetCoreDynamicProxyExample.DAL
{
    public class EmployeeContext : IDisposable
    {
        private List<Employee> _employees;

        public EmployeeContext()
        {
            _employees = new List<Employee>
            {
                new Employee
                {
                    Id = 1,
                    Name = "Ali",
                    Surname = "Talibov",
                    Age = 23
                },
                new Employee
                {
                    Id = 2,
                    Name = "Babaxan",
                    Surname = "Valiyev",
                    Age = 22
                },
                new Employee
                {
                    Id = 3,
                    Name = "Qara",
                    Surname = "Qarayev",
                    Age = 27
                }
            };
        }

        public List<Employee> Employees
        {
            get
            {
                if (new Random().Next(0, 50) % 2 == 0)
                {
                    return _employees;
                }

                throw new ConnectionAbortedException("Connection aborted");
            }
        }

        public void Dispose()
        {
            _employees = null;
        }
    }
}