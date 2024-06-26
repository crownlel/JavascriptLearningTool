using JavascriptLearningTool.Helpers;
using JavascriptLearningTool.Models;
using Newtonsoft.Json;

namespace JavascriptLearningTool.ClientServices
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string?> GetAuthTokenAsync(User loginModel)
        {

            var response = await _httpClient.PostAsJsonAsync("api/authentication/token", loginModel);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                return null;
            }
        }

        public async Task<Course[]?> GetUserCourses()
        {
            _httpClient!.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Constants.JWTToken);
            var response = await _httpClient.GetAsync("api/courses");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var courses = JsonConvert.DeserializeObject<Course[]>(result);
                return courses;
            }
            return null;
        }
    }
}
