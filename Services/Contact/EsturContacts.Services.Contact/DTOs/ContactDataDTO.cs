using System;

namespace SeturContacts.Services.Contact.DTOs
{
    public class ContactDataDTO
    {
        public string Id { get; set; }
        public string UserID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Company { get; set; }
        public ContactDataInfoDTO ContactDataInformation { get; set; }
    }
}
