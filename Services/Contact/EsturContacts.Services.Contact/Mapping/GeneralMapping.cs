using AutoMapper;
using EsturContacts.Services.Contact.DTOs;
using EsturContacts.Services.Contact.Models;

namespace EsturContacts.Services.Contact.Mapping
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
