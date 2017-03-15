using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GoMicro.Forex.DI
{
    public static class TypeFinder
    {
        public static IEnumerable<Type> FindConcreteTypesOf<TBasetype>()
            where TBasetype : class
        {
            return FindConcreteTypesOf<TBasetype>(true);
        }
        public static IEnumerable<Type> FindConcreteTypesOf<TBasetype>(bool publicTypesOnly)
            where TBasetype : class
        {
            return FindConcreteTypesOf<TBasetype>(publicTypesOnly, typeof(TBasetype).Assembly);
        }
        public static IEnumerable<Type> FindConcreteTypesOf<TBasetype>(bool publicTypesOnly, params Assembly[] assemblies)
            where TBasetype : class
        {
            IList<Type> foundTypes = new List<Type>();
            foreach (var assembly in assemblies)
                FindConcreteTypesOf<TBasetype>(publicTypesOnly, assembly).ForEach(foundTypes.Add); // foreach (Type T in FindConcreteTypesOf<TBasetype>(publicTypesOnly, assembly)) foundTypes.Add(T);         
            return foundTypes;

        }
        public static IEnumerable<Type> FindConcreteTypesOf<TBasetype>(bool publicTypesOnly, Assembly assembly)
            where TBasetype : class
        {
            var baseType = typeof(TBasetype);
            var foundTypes = assembly.GetTypes().Where(t => t.IsClass && !t.IsAbstract && baseType.IsAssignableFrom(t)); // var foundTypes = from type in assembly.GetTypes() where type.IsClass && !type.IsAbstract && baseType.IsAssignableFrom(type) select type;
            if (publicTypesOnly)
                foundTypes = foundTypes.Where(t => t.IsPublic); // foundTypes = from type in foundTypes where type.IsPublic select type;
            return foundTypes;
        }
    }
}
