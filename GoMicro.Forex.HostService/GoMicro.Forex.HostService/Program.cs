using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoMicro.Forex.HostService
{
    class Program
    {
        static void Main(string[] args)
        {
            // config values

            var serviceName = "GoMicroForex";
            var serviceDescription = "Exchange rate micro-service";
            var serviceDisplayName = "GoMicro Forex Service";
            var serviceInstanceName = "GMFX01";


            // run topshelf service

            return (int)HostFactory.Run(serviceConfig =>
            {
                serviceConfig.UseAssemblyInfoForServiceInfo();
                serviceConfig.RunAsPrompt();
                serviceConfig.StartAutomatically();
                serviceConfig.UseNLog();

                serviceConfig.SetServiceName(serviceName);
                serviceConfig.SetDisplayName(serviceDisplayName);
                serviceConfig.SetInstanceName(serviceInstanceName);
                serviceConfig.SetDescription(serviceDescription);

                serviceConfig.Service<WebApiHostService>(serviceInstance =>
                {
                    serviceInstance.ConstructUsing(() => new WebApiHostService());
                    serviceInstance.WhenStarted((s, hostControl) => s.Start(hostControl));
                    serviceInstance.WhenStopped((s, hostControl) => s.Stop(hostControl));
                    //
                    serviceInstance.WhenCustomCommandReceived((s, hostControl, cmd) => s.ExecuteCustomCommand(cmd));
                });

                serviceConfig.SetStartTimeout(TimeSpan.FromSeconds(10));
                serviceConfig.SetStopTimeout(TimeSpan.FromSeconds(10));

                serviceConfig.EnableServiceRecovery(recover => {
                    recover.SetResetPeriod(2);
                    recover.RestartService(2);
                    recover.OnCrashOnly();
                });

            });
        }
    }
}
