using MyApi.Models;

namespace MyApi.DTOs
{
    public class BlogDto : BaseDto
    {
        public string title { get; set; }
        public string description { get; set; }
        public int views { get; set; } = 0;
    }
}
