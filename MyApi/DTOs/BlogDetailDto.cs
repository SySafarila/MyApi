using MyApi.Models;

namespace MyApi.DTOs
{
    public class BlogDetailDto : BaseDto
    {
        public string title { get; set; }
        public string description { get; set; }
        public string content { get; set; }
        public int views { get; set; } = 0;
        public List<CommentDto> comments { get; set; } = new();
    }
}
