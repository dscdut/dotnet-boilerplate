namespace DotnetBoilerplate.Domain.Payloads
{
    public class PaginatedMeta
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
    }
}
