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
                config.Routes.MapHttpRoute("DefaultApi", "{controller}/{id}", new { id = RouteParameter.Optional });
                config.MapHttpAttributeRoutes();
                config.Formatters.Remove(config.Formatters.XmlFormatter);

                foreach (var r in config.Routes) Console.WriteLine(r.RouteTemplate);

                appBuilder.UseWebApi(config);
            }
        }

    }
}
