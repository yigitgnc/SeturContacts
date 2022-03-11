using AutoMapper;
using EsturContacts.Services.Contact.DTOs;
using EsturContacts.Services.Contact.Models;
using EsturContacts.Services.Contact.Settings;
using EsturContacts.Shared.DTOs;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EsturContacts.Services.Contact.Services
{
    internal class ContactDataService : IContactDataService
    {
        private readonly IMongoCollection<ContactData> _contactCollection;
        private readonly IMapper _mapper;

        public ContactDataService(IMapper mapper, IDatabaseSettings databaseSettings)
        {
            var mClient = new MongoClient(databaseSettings.ConnectionString);

            var database = mClient.GetDatabase(databaseSettings.DatabaseName);


            _contactCollection = database.GetCollection<ContactData>(databaseSettings.ContactDataCollectionName);
            _mapper = mapper;
        }

        public async Task<Response<List<ContactDataDTO>>> GetAllContactDatasAsync()
        {
            //todo: add better error management !
            Response<List<ContactDataDTO>> response;
            try
            {

                var contacts = await _contactCollection.Find(c => true).ToListAsync();
                response = Response<List<ContactDataDTO>>.Success(_mapper.Map<List<ContactDataDTO>>(contacts), 200);
            }
            catch (Exception ex)
            {
                //todo: its dangerous to return exception message !!
                response = Response<List<ContactDataDTO>>.Fail(ex.Message, 500);
            }

            return response;
        }

        public async Task<Response<ContactDataDTO>> CreateContactDataAsync(ContactDataCreateDTO contact)
        {
            ContactData newContactData = _mapper.Map<ContactData>(contact);
            newContactData.ContactDataInformation = _mapper.Map<ContactDataInfo>(contact.ContactDataInformation);

            await _contactCollection.InsertOneAsync(newContactData);

            return Response<ContactDataDTO>.Success(_mapper.Map<ContactDataDTO>(contact), 200);
        }

        public async Task<Response<NoContent>> UpdateContactDataAsync(ContactDataUpdateDTO contact)
        {
            ContactData updateContactData = _mapper.Map<ContactData>(contact);

            var updateResult = await _contactCollection.FindOneAndReplaceAsync(x => x.Id == contact.Id, updateContactData);

            if (updateResult == null)
            {
                return Response<NoContent>.Fail("ContactData Not Found !", 404);
            }
            return Response<NoContent>.Success(204);
        }


        public async Task<Response<ContactDataDeleteDTO>> DeleteContactDataByIdAsync(string id)
        {
            Response<ContactDataDeleteDTO> response;
            try
            {
                ContactData contact = null;
                Guid uuId;
                if (!Guid.TryParse(id, out uuId))
                {
                    contact = await _contactCollection.Find(c => c.Id == uuId).FirstOrDefaultAsync();
                    await _contactCollection.DeleteOneAsync(c => c.Id == contact.Id);
                }
                if (contact == null)
                {
                    response = Response<ContactDataDeleteDTO>.Fail("ContactData Not Found", 404);
                }
                else
                {
                    response = Response<ContactDataDeleteDTO>.Success(_mapper.Map<ContactDataDeleteDTO>(contact), 200);
                }
            }
            catch (Exception ex)
            {
                //no time to proper error handling :(
                response = Response<ContactDataDeleteDTO>.Fail(ex.Message, 500);
            }

            return response;
        }
        public async Task<Response<ContactDataDTO>> GetContactDataByIdAsync(string id)
        {
            Response<ContactDataDTO> response;
            try
            {
                ContactData contact = null;
                Guid uuId;
                if (!Guid.TryParse(id, out uuId))
                {
                    contact = await _contactCollection.Find(c => c.Id == uuId).FirstOrDefaultAsync();
                }
                if (contact == null)
                {
                    response = Response<ContactDataDTO>.Fail("ContactData Not Found", 404);
                }
                else
                {
                    response = Response<ContactDataDTO>.Success(_mapper.Map<ContactDataDTO>(contact), 200);
                }
            }
            catch (Exception ex)
            {
                //no time to proper error handling :(
                response = Response<ContactDataDTO>.Fail(ex.Message, 500);
            }

            return response;
        }

    }
}
