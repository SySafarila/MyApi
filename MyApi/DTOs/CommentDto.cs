namespace MyApi.DTOs
{
    public class CommentDto : BaseDto
    {
        public required int blog_id { get; set; }
        public required string content { get; set; }
    }
}
