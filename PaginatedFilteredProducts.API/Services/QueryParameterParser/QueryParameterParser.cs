using Ardalis.GuardClauses;

namespace PaginatedFilteredProducts.Services.QueryParameterParser;

public class QueryParameterParser : IQueryParameterParser
{
    public (string Column, string SortDirection)? ParseSortInstruction(string sort)
    {
        Guard.Against.NullOrEmpty(sort, nameof(sort));

        var parts = sort.Split(',', StringSplitOptions.RemoveEmptyEntries);

        Guard.Against.NullOrEmpty(parts[0], nameof(parts));
        Guard.Against.NullOrEmpty(parts[1], nameof(parts));

        var column = parts[0];
        var sortDirection = parts.Length == 2 ? parts[1] : "asc";

        return (column, sortDirection);
    }

    public Dictionary<string, List<(string Operation, object Value)>> ParseFilterCriteria(string filter)
    {
        var filterCriteria = new Dictionary<string, List<(string Operation, object Value)>>();
        foreach (var f in filter.Split(';', StringSplitOptions.RemoveEmptyEntries))
        {
            var parts = f.Split(',', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length < 3) continue; // Skip invalid filters

            var columnName = parts[0];
            var operation = parts[1];
            var value = parts[2];

            if (!filterCriteria.ContainsKey(columnName))
            {
                filterCriteria[columnName] = new List<(string Operation, object Value)>();
            }
            filterCriteria[columnName].Add((Operation: operation, Value: value));
        }

        return filterCriteria;
    }
}