using System.ComponentModel.DataAnnotations;

namespace MyApi.DTOs
{
    public class BlogRequestDto
    {
        [Required]
        [StringLength(100)]
        public string title { get; set; }
        [Required]
        [StringLength(100)]
        public string description { get; set; }
        [Required]
        public string content { get; set; }
    }
}
