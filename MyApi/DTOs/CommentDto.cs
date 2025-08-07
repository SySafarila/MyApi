namespace MyApi.DTOs
{
    public class CommentDto : BaseDto
    {
        public int blog_id { get; set; }
        public string content { get; set; }
    }
}
