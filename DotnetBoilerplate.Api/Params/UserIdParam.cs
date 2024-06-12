using Microsoft.AspNetCore.Mvc;

namespace DotnetBoilerplate.Api.Params
{
    public class UserIdParam
    {
        [FromRoute(Name = "id")]
        public int Id { get; set; }
    }
}
