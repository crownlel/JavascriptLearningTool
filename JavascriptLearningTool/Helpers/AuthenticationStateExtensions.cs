using Microsoft.AspNetCore.Components.Authorization;

namespace JavascriptLearningTool.Helpers
{
    public static class AuthenticationStateExtensions
    {
        public static async Task<bool> IsAuthenticatedAsync(this Task<AuthenticationState> authenticationStateTask)
        {
            var authenticationState = await authenticationStateTask;
            return authenticationState.User?.Identity?.IsAuthenticated is true;
        }
    }
}
