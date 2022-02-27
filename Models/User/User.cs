using AspNetCore.Identity.MongoDbCore.Models;
using MongoDB.Bson;
using MongoDbGenericRepository.Attributes;

namespace IsIoTWeb.Models
{
    [CollectionName("users")]
    public class User : MongoIdentityUser<ObjectId>, IDocument
    {
        string IDocument.Id { get => base.Id.ToString(); set => base.Id = new ObjectId(value); }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
