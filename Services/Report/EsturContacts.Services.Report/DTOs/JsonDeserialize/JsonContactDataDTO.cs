namespace SeturContacts.Services.Report.DTOs.JsonDeserialize
{
    public class JsonContactDataDTO
    {
        public string id { get; set; }
        public object userID { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string company { get; set; }
        public JsonContactDataInformationDTO contactDataInformation { get; set; }
    }
}
