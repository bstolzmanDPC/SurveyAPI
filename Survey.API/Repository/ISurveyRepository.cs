using Survey.API.Models;

namespace Survey.API.Repository
{
    public interface ISurveyRepository
    {
        Task<Guid> AddQuestion(Question question);
        Task<bool> RemoveQuestion(Guid id);
        Task<bool> UpdateQuestion(Question question);
        Task<IEnumerable<Question>> GetAllQuestions();
        Task<Question?> GetQuestionById(Guid id);
        Task<QuestionVoteResult?> GetQuestionResults(Guid id);
        Task<bool> AddVote(Guid id);
    }
}