using AutoMapper;
using EsturContacts.Services.Contacts.Models;
using EsturContacts.Services.Contacts.DTOs;

namespace EsturContacts.Services.Contacts.Mapping
{
    public class GeneralMapping : Profile
    {

        public GeneralMapping()
        {
            //Contact Mapping
            CreateMap<Contact,ContactDTO>().ReverseMap();
            CreateMap<Contact,ContactCreateDTO>().ReverseMap();
            CreateMap<Contact,ContactUpdateDTO>().ReverseMap();

            //Contact Info Mapping
            CreateMap<ContactInfo,ContactInfoDTO>().ReverseMap();
        }
    }
}
