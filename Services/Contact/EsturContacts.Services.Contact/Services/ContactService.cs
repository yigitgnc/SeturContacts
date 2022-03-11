using AutoMapper;
using SeturContacts.Services.Contact.DTOs;
using SeturContacts.Services.Contact.Models;
using SeturContacts.Services.Contact.Settings;
using SeturContacts.Shared.DTOs;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SeturContacts.Services.Contact.Services
{
    public class ContactDataService : IContactDataService
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

        public async Task<Response<List<ContactData>>> GetAllContactDatasAsync()
        {
            //todo: add better error management !
            Response<List<ContactData>> response;
            try
            {

                var contacts = await _contactCollection.Find(c => true).ToListAsync();
                response = Response<List<ContactData>>.Success(_mapper.Map<List<ContactData>>(contacts), 200);
            }
            catch (Exception ex)
            {
                //todo: its dangerous to return exception message !!
                response = Response<List<ContactData>>.Fail(ex.Message, 500);
            }

            return response;
        }

        public async Task<Response<ContactData>> CreateContactDataAsync(ContactDataCreateDTO contact)
        {
            ContactData newContactData = _mapper.Map<ContactData>(contact);
            newContactData.ContactDataInformation = _mapper.Map<ContactDataInfo>(contact.ContactDataInformation);
            newContactData.Id = MongoDB.Bson.ObjectId.GenerateNewId().ToString();

            await _contactCollection.InsertOneAsync(newContactData);

            return Response<ContactData>.Success(newContactData, 200);
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
                contact = await _contactCollection.Find(c => c.Id == id).FirstOrDefaultAsync();
                await _contactCollection.DeleteOneAsync(c => c.Id == contact.Id);

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
        public async Task<Response<ContactData>> GetContactDataByIdAsync(string id)
        {
            Response<ContactData> response;
            try
            {
                ContactData contact = null;
                contact = await _contactCollection.Find(c => c.Id == id).FirstOrDefaultAsync();

                if (contact == null)
                {
                    response = Response<ContactData>.Fail("ContactData Not Found", 404);
                }
                else
                {
                    response = Response<ContactData>.Success(_mapper.Map<ContactData>(contact), 200);
                }
            }
            catch (Exception ex)
            {
                //no time to proper error handling :(
                response = Response<ContactData>.Fail(ex.Message, 500);
            }

            return response;
        }

    }
}
