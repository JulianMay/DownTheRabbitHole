using System;
using System.Collections.Generic;
using System.Linq;

namespace DownTheRabbitHole
{
    public static class CollectionExtensions
    {
        public static bool TryGet<TObj>(this IEnumerable<TObj> collection, Func<TObj, bool> predicate, out TObj found)
        {
            found = collection.FirstOrDefault(predicate);
            return !default(TObj).Equals(found);
        }

    }

}
