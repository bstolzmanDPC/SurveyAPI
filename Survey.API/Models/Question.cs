using System.ComponentModel.DataAnnotations;

namespace Survey.API.Models
{
    public class Question
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string Prompt { get; set; }

        public IEnumerable<Option> Options { get; set; }
    }
}
