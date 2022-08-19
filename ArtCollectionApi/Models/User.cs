using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ArtCollectionApi.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string FirstName {get; set;} = null!;
        public string LastName {get; set;} = null!;
        public string Email {get; set;} = null!;
        public string Password {get; set;} = null!;
        public bool IsAdmin {get; set;}
        public DateTime AccessExpirationDate {get; set;}
        public DateTime CreationDate {get; set;}
    }
}