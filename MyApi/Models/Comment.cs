using System.Text.Json.Serialization;

namespace MyApi.Models
{
    public class Comment : BaseModel
    {
        public required string content { get; set; }

        // foreign key
        public required int blog_id { get; set; }

        [JsonIgnore]
        public Blog Blog { get; set; } = null!;
    }
}
