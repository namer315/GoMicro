using Owin;
using Microsoft.Owin.Hosting;
using System;
using System.Web.Http;
using Topshelf;
using Topshelf.Logging;

namespace GoMicro.Forex.HostService
{
    class WebApiHostService : ServiceControl
    {
        private readonly LogWriter _log = HostLogger.Get<WebApiHostService>();

        public WebApiHostService() { }
        public bool Start(HostControl hostControl)
        {
            try
            {
                _log.Info(">>> Topshelf Service >>> HostControl being Started...");

                string baseAddress = "http://localhost:9000/";
                WebApp.Start<Startup>(url: baseAddress);

                return true;
            }
            catch (Exception e)
            {
                _log.Error(e);
                return false;
            }
        }
        public bool Stop(HostControl hostControl)
        {
            _log.Info(">>> Topshelf Service >>> HostControl being Stopped...");            
            return true;
        }
        public void ExecuteCustomCommand(int command)
        {
            _log.Info($">>> Topshelf Service >>> system command:{command}");
        }
        internal class Startup
        {
            public void Configuration(IAppBuilder appBuilder)
            {
                HttpConfiguration config = new HttpConfiguration();
                config.Routes.MapHttpRoute(
                    name: "DefaultApi",
                    routeTemplate: "api/{controller}/{id}",
                    defaults: new { id = RouteParameter.Optional }
                );

                appBuilder.UseWebApi(config);
            }
        }
    }
}
