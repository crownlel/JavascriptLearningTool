using JavascriptLearningTool.Helpers;
using JavascriptLearningTool.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace JavascriptLearningTool.ClientServices
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ApiService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
        }

        private async Task<T?> GetAsync<T>(string url, bool isAuthorizationNeeded = false)
        {
            if (isAuthorizationNeeded)
            {

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _httpContextAccessor.GetAuthToken());
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

        private async Task PostAsJsonAsync<T>(string url, T obj, bool isAuthorizationNeeded = false)
        {
            if (isAuthorizationNeeded)
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _httpContextAccessor.GetAuthToken());
            }
            var response = await _httpClient.PostAsJsonAsync(url, obj);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
            }
        }

        private async Task<T?> PostAsJsonAsync<T, T2>(string url, T2 obj, bool isAuthorizationNeeded = false)
        {
            if (isAuthorizationNeeded)
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _httpContextAccessor.GetAuthToken());
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

        #region Courses
        public async Task<Course[]?> GetUserCourses() => await GetAsync<Course[]>("api/courses", true);

        public async Task<Course?> GetCourseByIdAsync(int id) => await GetAsync<Course>($"api/courses/{id}", true);

        public async Task<CoursePage?> GetCoursePageAsync(int courseId, int pageId, int secondsSpentOnPage)
            => await PostAsJsonAsync<CoursePage, int>($"api/courses/{courseId}/page/{pageId}", secondsSpentOnPage, true);

        public async Task<IEnumerable<PageActivity>?> GetAllUserPageStatsGroupedAsync() => await GetAsync<IEnumerable<PageActivity>>("api/courses/stats", true);

        public async Task<Dictionary<DateOnly, int>?> GetDailyActivitiesAsync() => await GetAsync<Dictionary<DateOnly, int>>("api/courses/stats/daily", true);
        #endregion

        #region Tests

        public async Task<IEnumerable<Test>?> GetTestsAsync() => await GetAsync<IEnumerable<Test>>("api/tests");

        public async Task<IEnumerable<Question>?> GetTestQuestionsAsync(int courseId) => await GetAsync<IEnumerable<Question>>($"api/tests/{courseId}");
        public async Task<IEnumerable<Question>?> GetTestQuestionsWeightedAsync(int courseId) => await GetAsync<IEnumerable<Question>>($"api/tests/{courseId}/weighted", true);

        public async Task<IEnumerable<Question>?> GetComprehensiveTestQuestionsAsync() => await GetAsync<IEnumerable<Question>>($"api/tests/{Constants.ComprehensiveTestRoute}");
        public async Task<IEnumerable<Question>?> GetComprehensiveTestQuestionsWeightedAsync() => await GetAsync<IEnumerable<Question>>($"api/tests/{Constants.ComprehensiveTestRoute}/weighted", true);

        public async Task SubmitAnswersAsync(IEnumerable<Answer> answers) => await PostAsJsonAsync("api/tests/submit", answers, true);

        public async Task<IEnumerable<QuestionProgress>?> GetQuestionProgressAsync() => await GetAsync<IEnumerable<QuestionProgress>>("api/tests/progress", true);
        #endregion
    }
}