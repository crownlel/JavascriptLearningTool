using JavascriptLearningTool.Helpers;
using JavascriptLearningTool.Models;
using JavascriptLearningTool.Repositories;
using JavascriptLearningTool.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JavascriptLearningTool.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoursesController : Controller
    {
        private readonly CourseRepository _courseRepository;
        private readonly UserRepository _userRepository;
        private readonly UserProgressRepository _userProgressRepository;
        private readonly PageRepository _pageRepository;

        public CoursesController(CourseRepository courseRepository, UserRepository userRepository, UserProgressRepository userProgressRepository, PageRepository pageRepository)
        {
            _courseRepository = courseRepository;
            _userRepository = userRepository;
            _userProgressRepository = userProgressRepository;
            _pageRepository = pageRepository;
        }

        [HttpGet]
        [Authorize]
        //[Route("courses")]
        public async Task<IActionResult> GetUserCourses()
        {
            var username = User!.Identity!.Name!;
            var user = await _userRepository.GetUserByUsernameAsync(username);
            if (user == null)
                return BadRequest("User not found");
            var userCourses = await _courseRepository.GetAllUserCoursesAsync(user.Id);

            return Ok(userCourses.ToArray());
        }

        [HttpGet]
        [Authorize]
        [Route("{courseId:int}")]
        public async Task<IActionResult> GetUserCourse(int courseId)
        {
            var username = User!.Identity!.Name!;
            var user = await _userRepository.GetUserByUsernameAsync(username);
            if (user == null)
                return BadRequest("User not found");
            var userCourse = await _courseRepository.GetUserCourseAsync(courseId, user.Id);
            if (userCourse?.CurrentPage is 0)
            {
                await _userProgressRepository.AddUserProgressAsync(new UserProgress
                {
                    UserId = user.Id,
                    CourseId = courseId,
                    LastPage = 1,
                    LastUpdated = DateTime.Now
                });
                userCourse.CurrentPage = 1;
            }
            return Ok(userCourse);
        }

        [HttpPost]
        [Authorize]
        [Route("{courseId:int}/page/{pageId:int}")]
        public async Task<IActionResult> GetUserCoursePage(int courseId, int pageId, [FromBody] int secondsSpentOnPage)
        {
            var username = User!.Identity!.Name!;
            var user = await _userRepository.GetUserByUsernameAsync(username);
            if (user == null)
                return BadRequest("User not found");

            var page = await _pageRepository.GetCoursePageAsync(courseId, pageId);
            if (page == null)
                return NotFound("Page not found");

            // Save user progress
            await _userProgressRepository.SaveUserProgressAsync(user.Id, courseId, pageId, secondsSpentOnPage);
            return Ok(page);
        }

        [HttpGet]
        [Authorize]
        [Route("stats")]
        public async Task<IActionResult> GetAllUserPageStatsGroupedAsync()
        {
            var username = User!.Identity!.Name!;
            var user = await _userRepository.GetUserByUsernameAsync(username);
            if (user == null)
                return BadRequest("User not found");
            var activities = await _userProgressRepository.GetAllUserPageStatsAsync(user.Id);
            var groupedActivities = activities.GroupBy(a => new { a.CourseId, a.PageId })
                .Select(g => new PageActivity
                {
                    UserId = user.Id,
                    CourseId = g.Key.CourseId,
                    PageId = g.Key.PageId,
                    SecondsSpent = g.Sum(a => a.SecondsSpent),
                    Timestamp = g.Max(a => a.Timestamp)
                })
                .OrderBy(pa => pa.CourseId)
                .ThenBy(pa => pa.PageId);
            return Ok(groupedActivities.ToArray());
        }

        [HttpGet]
        [Authorize]
        [Route("stats/daily")]
        public async Task<IActionResult> GetDailyActivitiesAsync()
        {
            var username = User!.Identity!.Name!;
            var user = await _userRepository.GetUserByUsernameAsync(username);
            if (user == null)
                return BadRequest("User not found");

            var dailyActivities = await _userProgressRepository.GetDailyActivitiesAsync(user.Id, Constants.DailyStatsDays);
            foreach (var day in Enumerable.Range(0, Constants.DailyStatsDays)
                .Select(i => DateOnly.FromDateTime(DateTime.Now.Date.AddDays(-i))))
            {
                dailyActivities.TryAdd(day, 0);
            }
            
            return Ok(dailyActivities.OrderBy(da => da.Key).ToDictionary(kvp => kvp.Key, kvp => kvp.Value));
        }
    }
}
