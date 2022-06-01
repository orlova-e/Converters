namespace Converters.Web.Models.Common;

public class ListDto<T>
{
    public int CurrentPage { get; set; }
    public int ItemsPerPage { get; set; }
    public int TotalItems { get; set; }
    public int TotalPages { get; set; }
    public IEnumerable<T> Entities { get; set; }
}