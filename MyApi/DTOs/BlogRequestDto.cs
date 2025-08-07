using System.ComponentModel.DataAnnotations;

namespace MyApi.DTOs
{
    public class BlogRequestDto
    {
        [Required]
        [StringLength(100)]
        public required string title { get; set; }
        [Required]
        [StringLength(100)]
        public required string description { get; set; }
        [Required]
        public required string content { get; set; }
    }
}
