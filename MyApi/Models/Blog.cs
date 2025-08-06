namespace MyApi.Models
{
    public class Blog : BaseModel
    {
        public string title { get; set; }
        public string description { get; set; }
        public string content { get; set; }
    }
}
