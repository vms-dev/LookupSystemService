using System;
using System.Collections.Generic;

namespace LookupSystem.DataAccess.Extensions
{
    public static class EnumerableExtensions
    {
        public static Random randomizer = new Random();

        public static IEnumerable<T> GetRandom<T>(this List<T> list, int numItems)
        {
            var items = new HashSet<T>();
            while (numItems > 0)
                if (items.Add(list[randomizer.Next(list.Count)])) numItems--;

            return items;
        }
    }
}
