
using Microsoft.AspNetCore.Mvc;

namespace DotnetBoilerplate.Api.Params
{
    public class PaginationParams
    {
        [FromQuery(Name = "page_size")]
        public int PageSize { get; set; } = 10;
        [FromQuery(Name = "page")]
        public int Page { get; set; } = 1;
    }
}
