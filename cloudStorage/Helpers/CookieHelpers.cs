using CloudStorage.Constants;

namespace CloudStorage.Helpers {
    public static class CookieHelpers {
        public static string GetJwtTokenFromCookies(IRequestCookieCollection Cookies)
        {
            string jwtTokenCookie = Cookies[CookieKeyNames.access_token];

            if (jwtTokenCookie == null)
            {
                throw new ApplicationException(CookieHelpersErrorMessages.canNotGetAccesTokenFromCookies);
            }

            Console.WriteLine("jwtTokenCookie: ");
            Console.WriteLine(jwtTokenCookie);


            return jwtTokenCookie;
        }

        public static CookieOptions GetAuthCookieOptions() 
        {
            return new CookieOptions() 
            {
                HttpOnly = true,
                SameSite = SameSiteMode.Strict
            };
        }
    }


}