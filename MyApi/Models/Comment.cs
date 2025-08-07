using System.Text.Json.Serialization;

namespace MyApi.Models
{
    public class Comment : BaseModel
    {
        public string content { get; set; }

        // foreign key
        public int blog_id { get; set; }

        [JsonIgnore]
        public Blog Blog { get; set; } = null!;
    }
}
