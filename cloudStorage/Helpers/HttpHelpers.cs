namespace CloudStorage.Helpers
{
    public class HttpHelpers
    {
        public static string GetJwtTokenFromHeaders(IHeaderDictionary headers)
        {
            string authorizationHeader = headers["Authorization"];
            string jwtToken = authorizationHeader.Substring("Bearer ".Length);

            return jwtToken;
        }
    }
}