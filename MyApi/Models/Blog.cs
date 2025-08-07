namespace MyApi.Models
{
    public class Blog : BaseModel
    {
        public required string title { get; set; }
        public required string description { get; set; }
        public required string content { get; set; }
        public int views { get; set; } = 0;
        public List<Comment> comments { get; set; } = new();
    }
}
