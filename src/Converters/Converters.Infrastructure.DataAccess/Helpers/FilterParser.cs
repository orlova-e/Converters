namespace Converters.Infrastructure.DataAccess.Helpers;

public static class FilterParser
{
    public static bool TryParseFilter(string filter, out Dictionary<string, string> filterModels)
    {
        filterModels = new Dictionary<string,string>();

        if (string.IsNullOrWhiteSpace(filter))
            return false;
        
        var filters = filter.Split(new[] {';'}, StringSplitOptions.RemoveEmptyEntries);

        foreach (var entry in filters)
        {
            var splitIndex = entry.IndexOf('=');
            var property = entry.Substring(0, splitIndex);
            var value = entry.Substring(splitIndex + 1);

            if (string.IsNullOrWhiteSpace(property) ||
                string.IsNullOrEmpty(value))
            {
                continue;
            }

            filterModels.Add(property, value);
        }

        return true;
    }
    
    public static Dictionary<string, string> ParseFilter(string filter)
    {
        TryParseFilter(filter, out Dictionary<string, string> filters);
        return filters;
    }

    public static string InsertIntoFilter(string filter, string property, string value)
    {
        if (!TryParseFilter(filter, out Dictionary<string, string> filters))
            return GetFilter(property, value);

        if (filters.ContainsKey(property))
        {
            filters.Remove(property);
        }
        
        filters.TryAdd(property, value);
        return filters.AsString();
    }

    public static string RemoveFromFilter(string filter, string property)
    {
        if (!TryParseFilter(filter, out Dictionary<string, string> filters))
            return filter;

        if (filters.ContainsKey(property))
        {
            filters.Remove(property);
        }

        return filters.AsString();
    }

    public static string GetFilter(string property, string value)
    {
        return string.Join("=", property, value);
    }
    
    public static string AsString(this Dictionary<string, string> filters)
    {
        if (filters is null || !filters.Any())
            return string.Empty;
        
        return string.Join(";", filters.Select(x => x.Key + "=" + x.Value));
    }
}