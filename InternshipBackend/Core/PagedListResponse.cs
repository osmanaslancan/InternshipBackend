namespace InternshipBackend.Core;

public class PagedListDto<T>
{
    public required List<T> Items { get; set; }
    public int From { get; set; }
    public int Count { get; set; }
    public int Total { get; set; }
}