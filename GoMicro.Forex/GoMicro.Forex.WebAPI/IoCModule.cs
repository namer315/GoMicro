using System.Reflection;
using Autofac;
using Autofac.Integration.WebApi;
using Module = Autofac.Module;

namespace GoMicro.Forex.WebApi
{
    class IoCModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ApiShell>().As<IApiShell>().SingleInstance();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
        }
    }
}
