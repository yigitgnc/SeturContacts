﻿namespace EsturContacts.Services.Contacts.DTOs
{
    internal class ContactUpdateDTO
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Company { get; set; }
        public ContactInfoDTO ContactInformation { get; set; }
    }
}