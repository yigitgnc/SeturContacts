using EsturContacts.Services.Contacts.DTOs;
using EsturContacts.Services.Contacts.Models;
using EsturContacts.Shared.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EsturContacts.Services.Contacts.Services
{
    internal interface IContactService
    {
        //Rehberde kişi oluşturma
        Task<Response<ContactDTO>> CreateContactAsync(ContactCreateDTO contact);
        //Rehberde kişi kaldırma
        Task<Response<ContactDeleteDTO>> DeleteContactByIdAsync(string id);

        //Rehberdeki kişiye iletişim bilgisi ekleme
        //Rehberdeki kişiden iletişim bilgisi kaldırma
        //gonna use same method for both of these by only updating its ContactInformation property !
        Task<Response<ContactUpdateDTO>> UpdateContactAsync(ContactDTO contact);

        //Rehberdeki bir kişiyle ilgili iletişim bilgilerinin de yer aldığı detay bilgilerin getirilmesi
        Task<Response<ContactDTO>> GetContactByIdAsync(string id);
        //Rehberdeki kişilerin listelenmesi
        Task<Response<List<ContactDTO>>> GetAllContactsAsync();

    }
}
