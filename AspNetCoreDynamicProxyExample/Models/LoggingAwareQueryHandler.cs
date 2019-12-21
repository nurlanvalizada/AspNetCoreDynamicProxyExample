using System;
using AspNetCoreDynamicProxyExample.Controllers;
using AspNetCoreDynamicProxyExample.Models.GenericHandlers;
using Microsoft.Extensions.Logging;

namespace AspNetCoreDynamicProxyExample.Models
{
    public class LoggingAwareQueryHandler<TQuery, TResult> : IQueryHandler<TQuery, TResult>
    {
        private readonly IQueryHandler<TQuery, TResult> _decoratedHandler;
        private readonly ILogger<TestController> _logger;

        public LoggingAwareQueryHandler(IQueryHandler<TQuery, TResult> decoratedHandler, ILogger<TestController> logger)
        {
            _decoratedHandler = decoratedHandler;
            _logger = logger;
        }

        public TResult Handle(TQuery query)
        {
            //logging
            try
            {
                var result = _decoratedHandler.Handle(query);

                _logger.LogInformation("");

                return result;
            }
            catch (Exception exc)
            {
                _logger.LogError($"Exception occured: {exc}");
                throw;
            }
        }
    }
}
