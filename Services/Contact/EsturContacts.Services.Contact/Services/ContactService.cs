using AutoMapper;
using EsturContacts.Services.Contacts.DTOs;
using EsturContacts.Services.Contacts.Models;
using EsturContacts.Services.Contacts.Settings;
using EsturContacts.Shared.DTOs;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EsturContacts.Services.Contacts.Services
{
    internal class ContactService : IContactService
    {
        private readonly IMongoCollection<Contact> _contactCollection;
        private readonly IMapper _mapper;

        public ContactService(IMapper mapper, IDatabaseSettings databaseSettings)
        {
            var mClient = new MongoClient(databaseSettings.ConnectionString);

            var database = mClient.GetDatabase(databaseSettings.DatabaseName);


            _contactCollection = database.GetCollection<Contact>(databaseSettings.ContactCollectionName);
            _mapper = mapper;
        }

        public async Task<Response<List<ContactDTO>>> GetAllContactsAsync()
        {
            //todo: add better error management !
            Response<List<ContactDTO>> response;
            try
            {

                var contacts = await _contactCollection.Find(c => true).ToListAsync();
                response = Response<List<ContactDTO>>.Success(_mapper.Map<List<ContactDTO>>(contacts), 200);
            }
            catch (Exception ex)
            {
                //todo: its dangerous to return exception message !!
                response = Response<List<ContactDTO>>.Fail(ex.Message, 500);
            }

            return response;
        }

        public async Task<Response<ContactDTO>> CreateContactAsync(ContactCreateDTO contact)
        {
            Contact newContact = _mapper.Map<Contact>(contact);
            newContact.ContactInformation = _mapper.Map<ContactInfo>(contact.ContactInformation);
            
            await _contactCollection.InsertOneAsync(newContact);

            return Response<ContactDTO>.Success(_mapper.Map<ContactDTO>(contact), 200);
        }

        public Task<Response<ContactUpdateDTO>> UpdateContactAsync(ContactDTO contact)
        {
            Contact updateContact = _mapper.Map<Contact>(contact);
            throw new NotImplementedException();
        }


        public async Task<Response<ContactDeleteDTO>> DeleteContactByIdAsync(string id)
        {
            Response<ContactDeleteDTO> response;
            try
            {
                Contact contact = null;
                Guid uuId;
                if (!Guid.TryParse(id, out uuId))
                {
                    contact = await _contactCollection.Find<Contact>(c => c.Id == uuId).FirstOrDefaultAsync();
                    await _contactCollection.DeleteOneAsync(c => c.Id == contact.Id);
                }
                if (contact == null)
                {
                    response = Response<ContactDeleteDTO>.Fail("Contact Not Found", 404);
                }
                else
                {
                    response = Response<ContactDeleteDTO>.Success(_mapper.Map<ContactDeleteDTO>(contact), 200);
                }
            }
            catch (Exception ex)
            {
                //no time to proper error handling :(
                response = Response<ContactDeleteDTO>.Fail(ex.Message, 500);
            }

            return response;
        }
        public async Task<Response<ContactDTO>> GetContactByIdAsync(string id)
        {
            Response<ContactDTO> response;
            try
            {
                Contact contact = null;
                Guid uuId;
                if (!Guid.TryParse(id, out uuId))
                {
                    contact = await _contactCollection.Find<Contact>(c => c.Id == uuId).FirstOrDefaultAsync();
                }
                if (contact == null)
                {
                    response = Response<ContactDTO>.Fail("Contact Not Found", 404);
                }
                else
                {
                    response = Response<ContactDTO>.Success(_mapper.Map<ContactDTO>(contact), 200);
                }
            }
            catch (Exception ex)
            {
                //no time to proper error handling :(
                response = Response<ContactDTO>.Fail(ex.Message, 500);
            }

            return response;
        }

    }
}
