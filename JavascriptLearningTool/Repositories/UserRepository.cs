using JavascriptLearningTool.Models;

namespace JavascriptLearningTool.Repositories
{
    public class UserRepository
    {

        public async Task<User?> GetUser(string username, string password)
        {
            if (username == "admin" && password == "password")
            {
                return new User
                {
                    Username = username,
                    Password = password
                };
            }
            return null;
        }
    }
}
