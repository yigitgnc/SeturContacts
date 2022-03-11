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

        public List<ReportContact> Contacts { get; set; }
    }
}
