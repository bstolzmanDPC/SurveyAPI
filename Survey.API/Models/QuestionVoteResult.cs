namespace Survey.API.Models
{
    public class QuestionVoteResult
    {
        public Guid Id { get; set; }
        public string Prompt { get; set; }
        public IEnumerable<OptionVoteResult> OptionResults { get; set; }
    }
}
