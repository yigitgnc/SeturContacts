using System;

namespace EsturContacts.Services.Contact.DTOs
{
    internal class ContactDataDTO
    {
        public Guid Id { get; set; }
        public string UserID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Company { get; set; }
        public ContactDataInfoDTO ContactDataInformation { get; set; }
    }
}
