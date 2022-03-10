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
    internal class ContactService
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
            catch (System.Exception ex)
            {
                //todo: its dangerous to return exception message !!
                response = Response<List<ContactDTO>>.Fail(ex.Message, 500);
            }

            return response;
        }

        public async Task<Response<ContactCreateDTO>> CreateContactAsync(Contact contact)
        {
            //todo: add proper error management !
            await _contactCollection.InsertOneAsync(contact);

            return Response<ContactCreateDTO>.Success(_mapper.Map<ContactCreateDTO>(contact), 200);
        }

        public async Task<Response<ContactDTO>> GetContactByIdAsync(string id)
        {
            Response<ContactDTO> response;
            Contact contact = null;
            Guid uuId;
            if (!Guid.TryParse(id,out uuId))
            {
                contact = await _contactCollection.Find<Contact>(c => c.Id == uuId).FirstOrDefaultAsync();
            }
            if (contact == null)
            {
                response = Response<ContactDTO>.Fail("")
            }
        }
    }
}
