using System;
using System.Collections;
using System.Collections.Generic;

namespace GoMicro.Forex.DI
{
    internal static class EnumerationSupport
    {
        public static void ForEach<TItem>(this IEnumerable<TItem> list, Action<TItem> action)
        {
            foreach (var item in list) action(item);
        }
        public static void ForEach<TItem>(this IEnumerable list, Action<TItem> action)
        {
            foreach (TItem item in list) action(item);
        }
    }
}
