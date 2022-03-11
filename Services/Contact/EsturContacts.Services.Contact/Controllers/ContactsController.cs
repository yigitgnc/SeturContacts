using SeturContacts.Services.Contact.DTOs;
using SeturContacts.Services.Contact.Services;
using SeturContacts.Shared.ControlleBases;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace SeturContacts.Services.Contact.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : CustomBaseController
    {
        private readonly IContactDataService _contactService;
        public ContactController(IContactDataService contactService)
        {
            _contactService = contactService;
        }

        //todo: add filtering by user id when identityServer included !!!
        /// <summary>
        /// Returns All ContactDatas 
        /// </summary>
        /// <returns></returns>   
        [HttpGet]
        public async Task<IActionResult> GetAllContactDatasAsync()
        {
            var response = await _contactService.GetAllContactDatasAsync();
            return CreateActionResultInstance(response);
        }


        /// <summary>
        /// Returns a contact by id (guid as string)
        /// </summary>
        /// <param name="id">Guid</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetContactDataByID(string id)
        {
            var response = await _contactService.GetContactDataByIdAsync(id);
            return CreateActionResultInstance(response);
        }

        /// <summary>
        /// Creates a New ContactData
        /// </summary>
        /// <param name="contactCreateDTO"></param>
        /// <returns>Returns Created ContactData</returns>
        [HttpPost]
        public async Task<IActionResult> Create(ContactDataCreateDTO contactCreateDTO)
        {
            var response = await _contactService.CreateContactDataAsync(contactCreateDTO);
            return CreateActionResultInstance(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update(ContactDataUpdateDTO contactUpdateDTO)
        {
            var response = await _contactService.UpdateContactDataAsync(contactUpdateDTO);
            return CreateActionResultInstance(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var response = await _contactService.DeleteContactDataByIdAsync(id);
            return CreateActionResultInstance(response);
        }

    }
}
