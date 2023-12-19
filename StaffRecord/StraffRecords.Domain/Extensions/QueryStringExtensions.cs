using System.Collections;

namespace StaffRecords.WEB.Extensions
{
    public static class QueryStringExtensions
    {
        public static string ToQueryString<T>(this T obj)
        {
            return string.Join("&", typeof(T).GetProperties().Where(property => property.GetMethod is not null)
                .Select(property => $"{property.Name}={property.GetMethod!.Invoke(obj, null)}"));
        }

        public static string ToQueryStringWithDate<T>(this T obj)
        {
            var queryStringParts = typeof(T).GetProperties()
                .Where(property => property.GetMethod is not null)
                .Select(property =>
                {
                    var value = property.GetMethod!.Invoke(obj, null);

                    if (value is DateTime dateTime)
                    {
                        value = dateTime.ToString("yyyy-MM-ddTHH:mm:ss");
                    }

                    if (value is ICollection collection)
                    {
                        if (!collection.Cast<object>().Any())
                        {
                            return string.Empty;
                        }

                        var param = string.Empty;

                        foreach (var item in collection)
                        {
                            param = string.Concat(param, $"{property.Name}={item}&");
                        }

                        return param.Remove(param.Length - 1);
                    }

                    return $"{property.Name}={value}";
                });

            return string.Join("&", queryStringParts);
        }
    }
}
