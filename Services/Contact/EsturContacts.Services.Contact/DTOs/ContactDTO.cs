using System;

namespace EsturContacts.Services.Contacts.DTOs
{
    internal class ContactDTO
    {
        public Guid Id { get; set; }
        public string UserID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Company { get; set; }
        public ContactInfoDTO ContactInformation { get; set; }
    }
}
