using System.Collections.Specialized;

namespace DotnetBoilerplate.Api.Utils
{
    public class RequestUtils
    {
        public static NameValueCollection GetAllQueryParams(IHttpContextAccessor httpContextAccessor)
        {
            var queryParams = httpContextAccessor.HttpContext.Request.Query;

            // Convert query parameters to a NameValueCollection
            var queryParamsCollection = new NameValueCollection();

            foreach (var queryParam in queryParams)
            {
                foreach (var value in queryParam.Value)
                {
                    queryParamsCollection.Add(queryParam.Key, value);
                }
            }
            return queryParamsCollection;
        }
    }
}
