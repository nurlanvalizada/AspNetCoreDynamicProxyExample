using System;
using AspNetCoreDynamicProxyExample.Models;
using AspNetCoreDynamicProxyExample.Models.Decorators;
using AspNetCoreDynamicProxyExample.Models.GenericHandlers;
using AspNetCoreDynamicProxyExample.Models.Interceptors;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AspNetCoreDynamicProxyExample
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            this.Configuration = builder.Build();
        }


        public IConfiguration Configuration { get; }

        public ILifetimeScope AutofacContainer { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddTransient<IEmployeeDataSource, EmployeeDataSource>();

            services.AddControllers();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.Register(i => new CustomLogger(Console.Out));
            builder.RegisterType<EmployeeDataSource>().As<IEmployeeDataSource>();  //.EnableInterfaceInterceptors().InterceptedBy(typeof(CustomLogger));
            builder.RegisterType<EmployeeDataSourceDecorator>().AsSelf();
            builder.RegisterType<EmployeeDataSourceQueryHandler>().As<IQueryHandler<string, GetEmployeesResult>>();
            builder.RegisterType<LoggingAwareQueryHandler<string, GetEmployeesResult>>().AsSelf();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            AutofacContainer = app.ApplicationServices.GetAutofacRoot();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
