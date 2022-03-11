using System;
using System.Collections.Generic;
using System.Text;

namespace SeturContacts.Shared.Messages
{
    public class CreateReportMessageCommand
    {
        public string Id { get; set; }
        public string Location { get; set; }
        public List<ContactData> Contacts { get; set; }

    }

    public class ContactData
    {
        public string Id { get; set; }
        public string UserID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Company { get; set; }
        public ContactInfo ContactDataInformation { get; set; }

    }
    public class ContactInfo
    {
        public string GSM { get; set; }
        public string Email { get; set; }
        public string Location { get; set; }
    }
}
