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

        public CoursesController(CourseRepository courseRepository, UserRepository userRepository, UserProgressRepository userProgressRepository)
        {
            _courseRepository = courseRepository;
            _userRepository = userRepository;
            _userProgressRepository = userProgressRepository;
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

            return Ok(userCourse);
        }
    }
}
