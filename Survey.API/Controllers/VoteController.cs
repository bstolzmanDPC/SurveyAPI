using Survey.API.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace Survey.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoteController : ControllerBase
    {
        private readonly ISurveyRepository _repository;
        private readonly ILogger<VoteController> _logger;

        public VoteController(ISurveyRepository repository, ILogger<VoteController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        /// <summary>
        /// Adds vote for given option id
        /// </summary>
        /// <param name="optionId"></param>
        /// <returns></returns>
        [HttpPost("{optionId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Vote(Guid optionId)
        {
            try
            {
                return Ok(await _repository.AddVote(optionId));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error adding vote for option {id}", optionId);
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Gets survey results by question id
        /// </summary>
        /// <param name="questionId"></param>
        /// <returns></returns>
        [HttpGet("Results/{questionId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetQuestionResults(Guid questionId)
        {
            try
            {
                return Ok(await _repository.GetQuestionResults(questionId));
            }
            catch(Exception e)
            {
                _logger.LogError(e, "Error retrieving voting results for question {id}", questionId);
                return StatusCode(500);
            }
        }
    }
}
