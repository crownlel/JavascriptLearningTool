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
        private readonly UserRepository _userRepository;
        private readonly AnswerRepository _answerRepository;

        public TestsController(TestRepository testRepository, QuestionRepository questionRepository, UserRepository userRepository, AnswerRepository answerRepository)
        {
            _testRepository = testRepository;
            _questionRepository = questionRepository;
            _userRepository = userRepository;
            _answerRepository = answerRepository;
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
        [Route("{testId:int}/weighted")]
        [Authorize]
        public async Task<IActionResult> GetTestQuestionsWeighted(int testId)
        {
            var username = User!.Identity!.Name!;
            var user = await _userRepository.GetUserByUsernameAsync(username);
            if (user == null)
                return BadRequest("User not found");
            var test = await _testRepository.GetTestAsync(testId);
            if (test == null)
            {
                return NotFound();
            }
            var questions = await _questionRepository.GetQuestionsWeightedAsync(user.Id, test.Duration, testId);
            return Ok(questions.ToArray());
        }

        [HttpGet]
        [Route(Constants.ComprehensiveTestRoute)]
        public async Task<IActionResult> GetComprehensiveTestQuestions()
        {
            var questions = await _questionRepository.GetQuestionsComprehensiveAsync(Constants.ComprehensiveTestDuration);
            return Ok(questions.ToArray());
        }

        [HttpGet]
        [Route($"{Constants.ComprehensiveTestRoute}/weighted")]
        [Authorize]
        public async Task<IActionResult> GetComprehensiveTestQuestionsWeighted()
        {
            var username = User!.Identity!.Name!;
            var user = await _userRepository.GetUserByUsernameAsync(username);
            if (user == null)
                return BadRequest("User not found");
            var questions = await _questionRepository.GetQuestionsWeightedAsync(user.Id, Constants.ComprehensiveTestDuration);
            return Ok(questions.ToArray());
        }

        [HttpPost]
        [Route("submit")]
        [Authorize]
        public async Task<IActionResult> SubmitTest([FromBody] IEnumerable<Answer> submission)
        {
            var username = User!.Identity!.Name!;
            var user = await _userRepository.GetUserByUsernameAsync(username);
            if (user == null)
                return BadRequest("User not found");

            await _answerRepository.SaveUserAnswersAsync(user.Id, submission);
            return Ok();
        }

        [HttpGet]
        [Route("progress")]
        [Authorize]
        public async Task<IActionResult> GetQuestionProgress()
        {

            var username = User!.Identity!.Name!;
            var user = await _userRepository.GetUserByUsernameAsync(username);
            if (user == null)
                return BadRequest("User not found");

            var questions = await _questionRepository.GetQuestionProgressAsync(user.Id);
            return Ok(questions);
        }
    }
}
