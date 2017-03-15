using Autofac;
using Autofac.Features.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GoMicro.Forex.DI
{
    public static class IoC
    {
        private static readonly object Lock = new object();
        public static IContainer Container { get; set; }
        public static void BootstrapContainer()
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.ScanAssemblyModules();
            builder.Build();
        }
        public static void BootstrapContainer(Action<ContainerBuilder> configure)
        {
            ContainerBuilder builder = new ContainerBuilder();
            configure(builder);
            Container = builder.Build();
            builder = new ContainerBuilder();
            builder.ScanAssemblyModules();
            builder.Update(Container);
        }
        public static void UpdateContainer(Action<ContainerBuilder> configure)
        {
            ContainerBuilder builder = new ContainerBuilder();
            configure(builder);
            builder.Update(Container);
        }
        public static T Resolve<T>()
        {
            return Container.Resolve<T>();
        }
        public static TInterface Resolve<TInterface, TDirector>(string key)
        {
            lock (Lock)
            {
                if (Container.IsRegisteredWithKey<TInterface>(key)) return Container.ResolveKeyed<TInterface>(key);
                ContainerBuilder builder = new ContainerBuilder();
                builder.RegisterType<TDirector>().Keyed<TInterface>(key).SingleInstance();
                builder.Update(Container);
                return Container.ResolveKeyed<TInterface>(key);
            }
        }
        public static TInterface Resolve<TInterface, TLookupType>(this IEnumerable<Meta<TInterface>> enumerable,
            TLookupType lookupKey, TLookupType defaultKey)
        {
            var collection = enumerable.ToList();
            var item = collection.FirstOrDefault(a => a.Metadata[typeof(TLookupType).Name].Equals(lookupKey));
            if (item != null) return item.Value;
            item = collection.FirstOrDefault(a => a.Metadata[typeof(TLookupType).Name].Equals(defaultKey));
            if (item != null) return item.Value;
            throw new NotImplementedException($"IoC Metadata key {typeof(TLookupType).Name} does not contain values {lookupKey} or {defaultKey}");

        }
    }
}
