using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace FeedbackService.Model
{
    public class Feedback
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Name")]
        public string UserName { get; set; } = null!;

        public decimal Rating { get; set; }

        public string Comment { get; set; } = null!;
    }
}
