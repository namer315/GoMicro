using Autofac;
namespace GoMicro.Forex.DI
{
    public interface IRegistry
    {
        void Configure(ContainerBuilder builder);
    }
}