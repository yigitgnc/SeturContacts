using SeturContacts.Services.Contact.DTOs;
using SeturContacts.Services.Contact.Models;
using SeturContacts.Shared.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SeturContacts.Services.Contact.Services
{
    public interface IContactDataService
    {
        //Rehberde kişi oluşturma
        Task<Response<ContactData>> CreateContactDataAsync(ContactDataCreateDTO contact);
        //Rehberde kişi kaldırma
        //i wanna return a ContactData Model instead of NoContent because i want to show the deleted user's basic informations to say goodbye :(
        Task<Response<ContactDataDeleteDTO>> DeleteContactDataByIdAsync(string id);

        //Rehberdeki kişiye iletişim bilgisi ekleme
        //Rehberdeki kişiden iletişim bilgisi kaldırma
        //gonna use same method for both of these by only updating its ContactDataInformation property !
        Task<Response<NoContent>> UpdateContactDataAsync(ContactDataUpdateDTO contact);

        //Rehberdeki bir kişiyle ilgili iletişim bilgilerinin de yer aldığı detay bilgilerin getirilmesi
        Task<Response<ContactData>> GetContactDataByIdAsync(string id);
        //Rehberdeki kişilerin listelenmesi
        Task<Response<List<ContactData>>> GetAllContactDatasAsync();

    }
}
