using JavascriptLearningTool.Helpers;
using JavascriptLearningTool.Models;
using JavascriptLearningTool.Repositories;
using JavascriptLearningTool.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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


        [HttpGet]
        [Route("{testId:int}")]
        public async Task<IActionResult> GetTestQuestions(int testId)
        {
            var test = await _testRepository.GetTestAsync(testId);
            if (test == null)
            {
                return NotFound();
            }
            var questions = await _questionRepository.GetTestQuestionsAsync(testId, test.Duration);
            return Ok(questions.ToArray());
        }

        [HttpGet]
        [Route(Constants.ComprehensiveTestRoute)]
        public async Task<IActionResult> GetComprehensiveTestQuestions()
        {
            var questions = await _questionRepository.GetQuestionsComprehensiveAsync(Constants.ComprehensiveTestDuration);
            return Ok(questions.ToArray());
        }
    }
}
