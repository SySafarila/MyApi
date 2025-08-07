namespace MyApi.Models
{
    public class BaseModel
    {
        public int id { get; set; }
        public required DateTime created_at { get; set; }
        public required DateTime updated_at { get; set; }
    }
}
