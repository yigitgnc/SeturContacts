using MongoDB.Bson.Serialization.Attributes;

namespace SeturContacts.Services.Report.Models
{
    public class ContactData
    {
        [BsonId]
        public string Id { get; set; }
        public string UserID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Company { get; set; }
        public ContactInfo ContactDataInformation { get; set; }
    }
}
