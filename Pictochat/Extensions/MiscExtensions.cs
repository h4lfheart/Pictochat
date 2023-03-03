using System.Collections.Generic;
using System.Linq;

namespace Pictochat.Extensions;

public static class MiscExtensions
{
    public static string CommaJoin<T>(this IEnumerable<T> enumerable, bool includeAnd = true)
    {
        var list = enumerable.ToList();
        if (list.Count == 0) return string.Empty;
        
        var joiner = includeAnd ? (list.Count == 2 ? " and " : ", and ") : ", ";
        return list.Count > 1 ? string.Join(", ", list.Take(list.Count - 1)) + joiner + list.Last() : list.First().ToString();
    }
}