using AspNetCore.Identity.MongoDbCore.Models;
using MongoDB.Bson;
using MongoDbGenericRepository.Attributes;

namespace IsIoTWeb.Models
{
    [CollectionName("roles")]
    public class Role : MongoIdentityRole<ObjectId>, IDocument
    {
        string IDocument.Id { get => base.Id.ToString(); set => base.Id = new ObjectId(value); }
    }
}
