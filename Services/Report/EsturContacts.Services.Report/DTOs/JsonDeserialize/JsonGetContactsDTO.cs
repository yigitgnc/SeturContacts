using System.Collections.Generic;

namespace SeturContacts.Services.Report.DTOs.JsonDeserialize
{
    public class JsonGetContactsDTO
    {
        public List<JsonContactDataDTO> data { get; set; }
        public object errors { get; set; }
    }
}
