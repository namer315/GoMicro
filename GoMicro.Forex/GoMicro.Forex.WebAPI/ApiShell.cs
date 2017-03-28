using System;
using Owin;
using System.Web.Http;
using Microsoft.Owin.Hosting;
using GoMicro.Forex.DI;
using Autofac.Integration.WebApi;

namespace GoMicro.Forex.WebApi
{
    public class ApiShell : IApiShell
    {
        private IDisposable _server;
        private IApiSettings _ApiStettings { get; }

        public ApiShell(IApiSettings settings)
        {
            _ApiStettings = settings;
        }

        public void Start()
        {
            _server = WebApp.Start<Startup>(_ApiStettings.Url);
            Console.WriteLine("...>>>...");
            Console.WriteLine($"Api listening on >> {_ApiStettings.Url}");
            Console.WriteLine($"Api Diagnostics >> {_ApiStettings.Url}/Ping");
            Console.WriteLine("...>>>...");
        }
        public void Stop() { _server.Dispose(); }

        internal class Startup {
            public void Configuration(IAppBuilder appBuilder)
            {
                var config = new HttpConfiguration();
                var resolver = new AutofacWebApiDependencyResolver(IoC.Container);
                config.DependencyResolver = resolver;
                config.Routes.MapHttpRoute("Default", "api/{controller}/{id}", new { id = RouteParameter.Optional });
                config.MapHttpAttributeRoutes();
                config.Formatters.Remove(config.Formatters.XmlFormatter);


                appBuilder.UseAutofacMiddleware(IoC.Container);
                appBuilder.UseAutofacWebApi(config);
                appBuilder.UseWebApi(config);



                /*
                var builder = new Autofac.ContainerBuilder();
                var config = new HttpConfiguration();
                config.Routes.MapHttpRoute("Default", "api/{controller}/{id}", new { id = RouteParameter.Optional });

                builder.RegisterApiControllers(System.Reflection.Assembly.GetExecutingAssembly());
                var container = builder.Build();
                config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

                appBuilder.UseAutofacMiddleware(container);
                appBuilder.UseAutofacWebApi(config);
                app.UseWebApi(config);
                */
            }
        }

    }
}
