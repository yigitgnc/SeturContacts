using AutoMapper;
using SeturContacts.Services.Contact.DTOs;
using SeturContacts.Services.Contact.Models;

namespace SeturContacts.Services.Contact.Mapping
{
    public class GeneralMapping : Profile
    {

        public GeneralMapping()
        {
            //ContactData Mapping
            CreateMap<ContactData, ContactDataDTO>().ReverseMap();
            CreateMap<ContactData, ContactDataCreateDTO>().ReverseMap();
            CreateMap<ContactData, ContactDataUpdateDTO>().ReverseMap();
            CreateMap<ContactData, ContactDataDeleteDTO>().ReverseMap();

            //ContactData Info Mapping
            CreateMap<ContactDataInfo, ContactDataInfoDTO>().ReverseMap();
        }
    }
}
