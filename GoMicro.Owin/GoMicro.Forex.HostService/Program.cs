﻿using System;
using Topshelf;

namespace GoMicro.Forex.HostService
{
    class Program
    {
        static int Main(string[] args)
        {
            // config values

            var serviceName = "GoMicroForex";
            var serviceDescription = "Exchange rate micro-service";
            var serviceDisplayName = "GoMicro Forex Service";
            var serviceInstanceName = "GMFX01";


            // run topshelf service

            return (int)HostFactory.Run(serviceConfig =>
            {
                serviceConfig.SetServiceName(serviceName);
                serviceConfig.SetDescription(serviceDescription);
                serviceConfig.SetDisplayName(serviceDisplayName);
                serviceConfig.SetInstanceName(serviceInstanceName);

                serviceConfig.UseAssemblyInfoForServiceInfo();
                serviceConfig.RunAsPrompt();
                serviceConfig.StartAutomatically();
                serviceConfig.UseNLog();


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
