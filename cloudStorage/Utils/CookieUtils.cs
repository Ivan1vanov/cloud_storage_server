using CloudStorage.Constants;

namespace CloudStorage.Utils
{
    public static class CookieUtils
    {
        public static string GetJwtTokenFromCookies(IRequestCookieCollection Cookies)
        {
            string jwtTokenCookie = Cookies[CookieKeyNames.access_token];

            if (jwtTokenCookie == null)
            {
                throw new ApplicationException(CookieUtilsErrorMessages.canNotGetAccesTokenFromCookies);
            }

            return jwtTokenCookie;
        }

        public static CookieOptions GetAuthCookieOptions()
        {
            return new CookieOptions()
            {
                HttpOnly = false,
                SameSite = SameSiteMode.Strict,
            };
        }
    }
}
