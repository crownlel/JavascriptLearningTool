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
    public class TestsController : Controller
    {
        private readonly TestRepository _testRepository;
        private readonly QuestionRepository _questionRepository;

        public TestsController(TestRepository testRepository, QuestionRepository questionRepository)
        {
            _testRepository = testRepository;
            _questionRepository = questionRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetTests()
        {
            var tests = await _testRepository.GetTestsAsync();
            return Ok(tests.ToArray());
        }

        [HttpPost]
        [Route("{testId:int}")]
        public async Task<IActionResult> GetTestQuestions(int testId, [FromBody] int totalQuestions)
        {
            var questions = await _questionRepository.GetTestQuestionsAsync(testId, totalQuestions);
            return Ok(questions.ToArray());
        }
    }
}
