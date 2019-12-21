using System;
using System.IO;
using System.Linq;
using Castle.DynamicProxy;

namespace AspNetCoreDynamicProxyExample.Models.Interceptors
{
    public class CustomLogger : IInterceptor
    {
        readonly TextWriter _writer;

        public CustomLogger(TextWriter writer)
        {
            _writer = writer ?? throw new ArgumentNullException(nameof(writer));
        }

        public void Intercept(IInvocation invocation)
        {
            var name = $"{invocation.Method.DeclaringType}.{invocation.Method.Name}";
            var args = string.Join(", ", invocation.Arguments.Select(a => (a ?? "").ToString()));

            _writer.WriteLine($"Calling: {name}");
            _writer.WriteLine($"Args: {args}");

            var watch = System.Diagnostics.Stopwatch.StartNew();

            try
            {
                invocation.Proceed(); //Intercepted method is executed here.
                _writer.WriteLine($"Done: result was {invocation.ReturnValue}");
            }
            catch (Exception exc)
            {
                _writer.WriteLine($"Exception occured: {exc}");
                throw;
            }

            watch.Stop();
            var executionTime = watch.ElapsedMilliseconds;

            _writer.WriteLine($"Execution Time: {executionTime} ms.");
            _writer.WriteLine();
        }
    }
}
