using Autofac;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace GoMicro.Forex.DI
{
    public static class ContainerBuilderExtensions
    {
        public static void ScanAssemblyModules(this ContainerBuilder builder, string searchPattern = "GoMicro*.dll")
        {
            var path = AppDomain.CurrentDomain.BaseDirectory;
            foreach (var assembly in Directory.GetFiles(path, searchPattern).Select(Assembly.LoadFile))
                builder.RegisterAssemblyModules(assembly);
        }
        public static void AddRegistry<TRegistry>(this ContainerBuilder builder)
            where TRegistry : IRegistry, new()
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));
            new TRegistry().Configure(builder);
        }
        public static void AddRegistry(this ContainerBuilder builder, IRegistry registry)
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));
            if (registry == null) throw new ArgumentNullException(nameof(registry));
            registry.Configure(builder);
        }

        private static void AddRegistriesFromAssembly(ContainerBuilder builder, Assembly assembly)
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));
            if (assembly == null) throw new ArgumentNullException(nameof(assembly));
            LoadRegistries(builder, new[] { assembly });
        }

        public static void AddRegistriesFromAssemblies(this ContainerBuilder builder, params Assembly[] assemblies)
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));
            if (assemblies == null) throw new ArgumentNullException(nameof(assemblies));
            LoadRegistries(builder, assemblies);
        }
        public static void AddRegistriesFromAssembliesOf<TType>(this ContainerBuilder builder, params Assembly[] assemblies)
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));
            if (assemblies == null) throw new ArgumentNullException(nameof(assemblies));
            LoadRegistries(builder, assemblies);
        }
        public static void AddregistriesFromAssemblyOf<TType>(this ContainerBuilder builder)
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));
            LoadRegistries(builder, new[] { typeof(TType).Assembly });
        }
        public static void LoadRegistries(this ContainerBuilder builder, params Assembly[] assemblies)
        {
            foreach (var type in TypeFinder.FindConcreteTypesOf<IRegistry>(false, assemblies))
                ((IRegistry)Activator.CreateInstance(type)).Configure(builder);
        }
    }
}
