namespace PaginatedFilteredProducts.Services.QueryParameterParser;

public interface IQueryParameterParser
{
    (string Column, string SortDirection)? ParseSortInstruction(string sort);
    Dictionary<string, List<(string Operation, object Value)>> ParseFilterCriteria(string filter);
}