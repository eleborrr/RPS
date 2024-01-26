using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RPS.Shared.Rating;

public class UserRatingMongoDto
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string UserId { get; set; }
    public int Rating { get; set; }
}