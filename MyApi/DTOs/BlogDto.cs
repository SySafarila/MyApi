using MyApi.Models;

namespace MyApi.DTOs
{
    public class BlogDto : BaseDto
    {
        public required string title { get; set; }
        public required string description { get; set; }
        public int views { get; set; } = 0;
    }
}
