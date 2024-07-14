
using Microsoft.AspNetCore.Mvc;

namespace DotnetBoilerplate.Api.Params
{
    public class PaginationParams
    {
        [FromQuery(Name = "page_size")]
        public string PageSize { get; set; } = "10";
        [FromQuery(Name = "page")]
        public string Page { get; set; } = "1";
    }
}
