namespace DotnetBoilerplate.Domain.Payloads
{
    public class PaginatedResult<T>
    {
        public List<T> Data { get; set; }
        public PaginatedMeta Meta { get; set; } 
    }
}
