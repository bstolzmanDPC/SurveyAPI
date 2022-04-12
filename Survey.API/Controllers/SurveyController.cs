using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Survey.API.Models;
using Survey.API.Repository;

namespace Survey.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SurveyController : ControllerBase
    {
        private readonly ISurveyRepository _repository;
        private readonly ILogger<SurveyController> _logger;

        public SurveyController(ISurveyRepository repository, ILogger<SurveyController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        /// <summary>
        /// Retrieves all questions for the survey
        /// </summary>
        /// <returns></returns>
        [HttpGet("Question")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllQuestions()
        {
            try
            {
                return Ok(await _repository.GetAllQuestions());
            }
            catch (Exception e)
            {
                _logger.LogError(e, "There was an error retrieving questions. See exception.");
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Gets question by given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Question/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetAllQuestions(Guid id)
        {
            try
            {
                return Ok(await _repository.GetQuestionById(id));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "There was an error retrieving questions. See exception.");
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Adds question to the survey
        /// </summary>
        /// <param name="question"></param>
        /// <returns></returns>
        [HttpPost("Question")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddQuestion(Question question)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return Ok(await _repository.AddQuestion(question));
                }

                return BadRequest();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "There was an error adding question. See exception.");
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Updates existing question
        /// </summary>
        /// <param name="question"></param>
        /// <returns></returns>
        [HttpPut("Question")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateQuestion(Question question)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return Ok(await _repository.UpdateQuestion(question));
                }

                return BadRequest();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "There was an error updating question {id}. See exception.", question.Id);
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Removes question matching provided Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("Question/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RemoveQuestion(Guid id)
        {
            try
            {
                return Ok(await _repository.RemoveQuestion(id));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "There was an error removing question {id}. See exception.", id);
                return StatusCode(500);
            }
        }

    }
}
