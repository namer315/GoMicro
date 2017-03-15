using System;
using Topshelf;
using Topshelf.Logging;
using Autofac;
using GoMicro.Forex.DI;
using GoMicro.Forex.WebApi;

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
                
                IoC.Container.Resolve<IApiShell>().Start();

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
    }
}
