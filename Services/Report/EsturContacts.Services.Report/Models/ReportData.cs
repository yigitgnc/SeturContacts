using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace SeturContacts.Services.Report.Models
{
    public class ReportData
    {
        [BsonId]
        //[BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonRepresentation(MongoDB.Bson.BsonType.DateTime)]
        public DateTime CreatedDate { get; set; }
        public string Status { get; set; }
        public string Location { get; set; }

        public List<ContactData> Contacts { get; set; }

        [BsonIgnore]
        public int ContactCount => Contacts.Count; 

        [BsonIgnore]
        public int ContactsGsmOnlyCount
        {
            get
            {
                //todo: thats definately the worst way to count it !!!!
                int count = 0;
                foreach (var item in Contacts)
                {
                    if (string.IsNullOrWhiteSpace(item.ContactDataInformation.GSM))
                    {
                        count++;
                    }
                }
                return count;

            }
        }

    }
}
