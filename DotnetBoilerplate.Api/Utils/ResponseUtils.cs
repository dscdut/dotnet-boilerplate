namespace DotnetBoilerplate.Api.Utils
{
    public class ResponseUtils
    {
        public static object CreateResponse(string message, object data = null)
        {
            return new { message, data };
        }
    }
}
