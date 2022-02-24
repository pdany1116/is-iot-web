using AspNetCore.Identity.MongoDbCore.Models;
using MongoDB.Bson;
using MongoDbGenericRepository.Attributes;

namespace IsIoTWeb.Models
{
    [CollectionName("users")]
    public class ApplicationUser : MongoIdentityUser<ObjectId>
    {
    }
}
