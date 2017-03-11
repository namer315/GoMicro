using System;
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
                _log.Info("Topshelf Service > Being Started...");

                //DI.Container.Resolve<IActorSystemShell>().Start();
                //DI.Container.Resolve<IApiShell>().Start();
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
            _log.Info("Topshelf Service > Being Stopped...");            
            return true;
        }
        public void ExecuteCustomCommand(int command)
        {
            _log.Info($"Topshelf Service > system command:{command}");
        }
    }
}
