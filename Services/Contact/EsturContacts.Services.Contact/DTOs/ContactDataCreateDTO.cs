namespace EsturContacts.Services.Contact.DTOs
{
    public class ContactDataCreateDTO
    {
        public string UserID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Company { get; set; }
        public ContactDataInfoDTO ContactDataInformation { get; set; }
    }
}
