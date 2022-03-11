using EsturContacts.Services.Contact.DTOs;
using EsturContacts.Services.Contact.Models;
using EsturContacts.Shared.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EsturContacts.Services.Contact.Services
{
    internal interface IContactDataService
    {
        //Rehberde kişi oluşturma
        Task<Response<ContactDataDTO>> CreateContactDataAsync(ContactDataCreateDTO contact);
        //Rehberde kişi kaldırma
        //i wanna return a ContactData Model instead of NoContent because i want to show the deleted user's basic informations to say goodbye :(
        Task<Response<ContactDataDeleteDTO>> DeleteContactDataByIdAsync(string id);

        //Rehberdeki kişiye iletişim bilgisi ekleme
        //Rehberdeki kişiden iletişim bilgisi kaldırma
        //gonna use same method for both of these by only updating its ContactDataInformation property !
        Task<Response<NoContent>> UpdateContactDataAsync(ContactDataUpdateDTO contact);

        //Rehberdeki bir kişiyle ilgili iletişim bilgilerinin de yer aldığı detay bilgilerin getirilmesi
        Task<Response<ContactDataDTO>> GetContactDataByIdAsync(string id);
        //Rehberdeki kişilerin listelenmesi
        Task<Response<List<ContactDataDTO>>> GetAllContactDatasAsync();

    }
}
