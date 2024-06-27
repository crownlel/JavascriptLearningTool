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
            _httpClient!.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Constants.JWTToken);
            var response = await _httpClient.GetAsync("api/courses");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var courses = JsonConvert.DeserializeObject<Course[]>(result);
                return courses;
            }
            return null;
        }

        public async Task<Course?> GetCourseByIdAsync(int id)
        {
            _httpClient!.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Constants.JWTToken);
            var response = await _httpClient.GetAsync($"api/courses/{id}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var course = JsonConvert.DeserializeObject<Course>(result);
                return course;
            }
            return null;
        }

        public async Task<CoursePage?> GetCoursePageAsync(int courseId, int pageId)
        {
            _httpClient!.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Constants.JWTToken);
            var response = await _httpClient.GetAsync($"api/courses/{courseId}/page/{pageId}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var coursePage = JsonConvert.DeserializeObject<CoursePage>(result);
                return coursePage; 
            }
            return null;
        }
    }
}
