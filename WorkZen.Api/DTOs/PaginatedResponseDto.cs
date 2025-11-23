namespace WorkZen.Api.DTOs;

public class PaginatedResponseDto<T>
{
    public IEnumerable<T> Items { get; }
    public int PageNumber { get; }
    public int PageSize { get; }
    public int TotalItems { get; }
    public int TotalPages { get; }
    public IEnumerable<LinkDto> Links { get; }

    public PaginatedResponseDto(
        IEnumerable<T> items,
        int pageNumber,
        int pageSize,
        int totalItems,
        int totalPages,
        IEnumerable<LinkDto> links)
    {
        Items = items;
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalItems = totalItems;
        TotalPages = totalPages;
        Links = links;
    }
}