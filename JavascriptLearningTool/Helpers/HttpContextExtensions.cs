namespace JavascriptLearningTool.Helpers
{
    public static class HttpContextExtensions
    {
        public static string? GetAuthToken(this IHttpContextAccessor httpContextAccessor)
        {
            return httpContextAccessor.HttpContext?.Session.GetString(Constants.TokenName);
        }

        public static void SetAuthToken(this IHttpContextAccessor httpContextAccessor, string? token)
        {
            if (string.IsNullOrEmpty(token))
            {
                httpContextAccessor.HttpContext?.Session.Remove(Constants.TokenName);
                return;
            }
            httpContextAccessor.HttpContext?.Session.SetString(Constants.TokenName, token);
        }
    }
}
