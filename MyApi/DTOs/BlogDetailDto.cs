using MyApi.Models;

namespace MyApi.DTOs
{
    public class BlogDetailDto : BaseDto
    {
        public required string title { get; set; }
        public required string description { get; set; }
        public required string content { get; set; }
        public int views { get; set; } = 0;
        public List<CommentDto> comments { get; set; } = new();
    }
}
