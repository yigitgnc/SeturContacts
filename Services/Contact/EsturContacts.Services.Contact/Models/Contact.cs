using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;

namespace EsturContacts.Services.Contact.Models
{
    public class Contact
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public Guid Id { get; set; }
        public string UserID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Company { get; set; }
        public ContactInfo ContactInformation { get; set; }


    }
}
