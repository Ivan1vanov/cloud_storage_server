namespace CloudStorage.Utils
{
    public static class CookieUtils
    {
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
