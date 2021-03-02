using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LookupSystem.DataAccess
{
    public static class EnumerableExtensions
    {
        public static Random randomizer = new Random();

        public static IEnumerable<T> GetRandom<T>(this List<T> list, int numItems)
        {
            var items = new HashSet<T>(); // don't want to add the same item twice; otherwise use a list
            while (numItems > 0)
                // if we successfully added it, move on
                if (items.Add(list[randomizer.Next(list.Count)])) numItems--;

            return items;
        }
    }
}
