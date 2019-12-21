using System.Collections.Generic;
using AspNetCoreDynamicProxyExample.Models;
using AspNetCoreDynamicProxyExample.Models.GenericHandlers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AspNetCoreDynamicProxyExample.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class TestController : ControllerBase
    {
        private readonly LoggingAwareQueryHandler<string, GetEmployeesResult> _loggingAwareQueryHandler;

        public TestController(LoggingAwareQueryHandler<string, GetEmployeesResult> loggingAwareQueryHandler)
        {
            _loggingAwareQueryHandler = loggingAwareQueryHandler;
        }


        [HttpGet]
        public IEnumerable<object> Employees()
        {
            var handlerResult = _loggingAwareQueryHandler.Handle("aba");

            return handlerResult.Employees;
        }
    }
}
