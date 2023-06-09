using System.Collections.Generic;
using System.Linq;

namespace Yogurt.Arena
{
    public static class IEnumerableEx
    {
        public static bool IsEmpty<T>(this IEnumerable<T> enumerable)
        {
            return !enumerable.Any();
        }

        public static T GetRandom<T>(this IEnumerable<T> enumerable)
        {
            if (enumerable.IsEmpty()) 
                return default;
            
            int index = enumerable.Count().RandomTo();
            return enumerable.ElementAt(index);
        }
    }
}