using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreDynamicProxyExample.Models.Attributes
{
    public class LogCountAttribute : Attribute
    {
        public string Name { get; private set; }

        public LogCountAttribute(string name)
        {
            Name = name;
        }
    }
}
