using AutoMapper;
using SeturContacts.Services.Report.DTOs;
using SeturContacts.Services.Report.DTOs.JsonDeserialize;
using SeturContacts.Services.Report.Models;

namespace SeturContacts.Services.Report.Mapping
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            CreateMap<ReportData, ReportDataCreateDTO>().ReverseMap();
            CreateMap<ContactData, JsonContactDataDTO>().ReverseMap();
        }
    }
}
