using Microsoft.AspNetCore.Mvc;

namespace DotnetBoilerplate.Api.Params
{
    public class UserIdParam
    {
        [FromRoute(Name = "id")]
        public string Id { get; set; }
    }
}
