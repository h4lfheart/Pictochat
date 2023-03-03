using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Media.Imaging;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;

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
    
    public static BitmapImage ToBitmapImage(this Image bitmap)
    {
        var source = new BitmapImage { CacheOption = BitmapCacheOption.OnDemand };
        source.BeginInit();
        source.StreamSource = new MemoryStream();
        bitmap.Save(source.StreamSource, PngFormat.Instance);
        source.EndInit();
        return source;
    }
}