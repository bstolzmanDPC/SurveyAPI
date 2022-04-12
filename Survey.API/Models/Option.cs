using System.ComponentModel.DataAnnotations;

namespace Survey.API.Models
{
    public class Option
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        
        [Required]
        public string Text { get; set; }
    }
}
