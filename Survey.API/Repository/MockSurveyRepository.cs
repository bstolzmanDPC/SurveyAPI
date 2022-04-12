using Survey.API.Models;
using System.Collections.Concurrent;

namespace Survey.API.Repository
{
    public class MockSurveyRepository : ISurveyRepository
    {
        private List<Question> _questions;
        private Dictionary<Guid, int> _voteTotals;

        public MockSurveyRepository()
        {
            _questions = new List<Question>();
            _voteTotals = new Dictionary<Guid,int>();
        }

        /// <summary>
        /// Gets list of all questions on the survey
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<Question>> GetAllQuestions()
        {
            return Task.FromResult((IEnumerable<Question>)_questions);
        }

        /// <summary>
        /// Gets question by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<Question?> GetQuestionById(Guid id)
        {
            var question = _questions.Where(x => x.Id == id).FirstOrDefault();

            return Task.FromResult(question);
        }

        /// <summary>
        /// Adds question to background collection
        /// </summary>
        /// <param name="question"></param>
        /// <returns></returns>
        public Task<Guid> AddQuestion(Question question)
        {
            _questions.Add(question);

            return Task.FromResult(question.Id);
        }

        /// <summary>
        /// Updates a given question in the background collection
        /// </summary>
        /// <param name="question"></param>
        /// <returns></returns>
        public Task<bool> UpdateQuestion(Question question)
        {
            var storedQuestion = _questions.Where(x => x.Id == question.Id).FirstOrDefault();

            if (storedQuestion != null)
            {
                _questions.Remove(storedQuestion);

                _questions.Add(question);

                return Task.FromResult(true);
            }

            return Task.FromResult(false);
        }

        /// <summary>
        /// Removes question from background collection
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<bool> RemoveQuestion(Guid id)
        {
            var storedQuestion = _questions.Where(x => x.Id == id).FirstOrDefault();

            if (storedQuestion != null)
            {
                return Task.FromResult(_questions.Remove(storedQuestion));
            }

            return Task.FromResult(false);
        }

        /// <summary>
        /// Adds vote for a given option id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<bool> AddVote(Guid id)
        {
            if(_voteTotals.TryGetValue(id, out var voteTotals))
            {
                _voteTotals[id] = ++voteTotals;
            }
            else
            {
                _voteTotals.Add(id, 1);
            }

            return Task.FromResult(true);
        }

        /// <summary>
        /// Gets survey vote results for given question id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<QuestionVoteResult?> GetQuestionResults(Guid id)
        {
            var question = _questions.Where(x => x.Id == id).FirstOrDefault();

            if(question != null)
            {
                var result = new QuestionVoteResult
                {
                    Id = question.Id,
                    Prompt = question.Prompt
                };

                var optionResults = new List<OptionVoteResult>();

                foreach(var option in question.Options)
                {
                    var optionResult = new OptionVoteResult
                    {
                        Option = option
                    };

                    if(_voteTotals.TryGetValue(option.Id, out var voteTotal))
                    {
                        optionResult.Count = voteTotal;
                    }
                    else
                    {
                        optionResult.Count = 0;
                    }

                    optionResults.Add(optionResult);
                }

                result.OptionResults = optionResults;

                return Task.FromResult(result);
            }

            return Task.FromResult(default(QuestionVoteResult));
        }
    }
}
