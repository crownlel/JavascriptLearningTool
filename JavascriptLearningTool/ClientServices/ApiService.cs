using JavascriptLearningTool.Helpers;
using JavascriptLearningTool.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace JavascriptLearningTool.ClientServices
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        private async Task<T> GetAsync<T>(string url, bool isAuthorizationNeeded = false)
        {
            if (isAuthorizationNeeded)
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Constants.JWTToken);
            }
            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var t = JsonConvert.DeserializeObject<T>(result);
                return t;
            }
            return default;
        }

        private async Task<T> PostAsJsonAsync<T, T2>(string url, T2 obj, bool isAuthorizationNeeded = false)
        {
            if (isAuthorizationNeeded)
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Constants.JWTToken);
            }
            var response = await _httpClient.PostAsJsonAsync(url, obj);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var t = JsonConvert.DeserializeObject<T>(result);
                return t;
            }
            return default;
        }

        public async Task<string?> GetAuthTokenAsync(User loginModel)
        {

            var response = await _httpClient.PostAsJsonAsync("api/authentication/token", loginModel);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            return null;
        }

        public async Task<Course[]?> GetUserCourses() => await GetAsync<Course[]>("api/courses", true);

        public async Task<Course?> GetCourseByIdAsync(int id) => await GetAsync<Course>($"api/courses/{id}", true);

        public async Task<CoursePage?> GetCoursePageAsync(int courseId, int pageId, int secondsSpentOnPage) 
            => await PostAsJsonAsync<CoursePage, int>($"api/courses/{courseId}/page/{pageId}", secondsSpentOnPage, true);
    }
}
