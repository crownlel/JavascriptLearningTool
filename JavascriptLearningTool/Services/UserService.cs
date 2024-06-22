using JavascriptLearningTool.Repositories;
using System.Security.Claims;

namespace JavascriptLearningTool.Services
{
    public class UserService
    {
        private ClaimsPrincipal _currentUser = new(new ClaimsIdentity());

        public bool IsLoggedIn => _currentUser.Identity?.IsAuthenticated ?? false;

        public event Action<bool>? OnChange;

        public ClaimsPrincipal User
        {
            get => _currentUser;
            set
            {
                if (_currentUser != value)
                {
                    var oldLoginState = IsLoggedIn;
                    _currentUser = value;
                    if (oldLoginState != IsLoggedIn)
                    {
                        OnChange?.Invoke(IsLoggedIn); 
                    }
                }
            }
        }
    }
}
