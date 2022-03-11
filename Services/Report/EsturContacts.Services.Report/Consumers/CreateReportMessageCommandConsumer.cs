using AutoMapper;
using MassTransit;
using MongoDB.Driver;
using Newtonsoft.Json;
using SeturContacts.Services.Report.DTOs.JsonDeserialize;
using SeturContacts.Services.Report.Models;
using SeturContacts.Services.Report.Services;
using SeturContacts.Services.Report.Settings;
using SeturContacts.Shared.Messages;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace SeturContacts.Services.Report.Consumers
{
    public class CreateReportMessageCommandConsumer : IConsumer<CreateReportMessageCommand>
    {

        private readonly IMongoCollection<ReportData> _reportCollection;
        private readonly IMapper _mapper;
        public CreateReportMessageCommandConsumer(IMapper mapper, IDatabaseSettings databaseSettings)
        {
            var mClient = new MongoClient(databaseSettings.ConnectionString);

            var database = mClient.GetDatabase(databaseSettings.DatabaseName);

            _reportCollection = database.GetCollection<ReportData>(databaseSettings.ReportDataCollectionName);

            _mapper = mapper;
        }

        public async Task Consume(ConsumeContext<CreateReportMessageCommand> context)
        {
            try
            {

                var report = await _reportCollection.Find(x => x.Id == context.Message.Id).FirstOrDefaultAsync();

                ////this is where the cheating begin :)
                HttpClient client = new HttpClient();
                string allContactsString = await client.GetStringAsync("http://localhost:5011/api/Contact");
                JsonGetContactsDTO jsonGetContacts = JsonConvert.DeserializeObject<JsonGetContactsDTO>(allContactsString);
                var contactDatas = jsonGetContacts.data;
                if (jsonGetContacts.data != null)
                {
                    report.Contacts = new List<ContactData>();
                    jsonGetContacts.data.ForEach(x =>
                    {
                        if (x.contactDataInformation.location == report.Location)
                        {
                            if (!string.IsNullOrWhiteSpace(x.contactDataInformation.gsm))
                            {
                                report.ContactsGsmOnlyCount++;
                            }
                            report.Contacts.Add(new ContactData
                            {
                                Id = x.id,
                                Company = x.company,
                                Name = x.name,
                                Surname = x.surname,
                                ContactDataInformation = new ContactInfo
                                {
                                    Email = x.contactDataInformation.email,
                                    GSM = x.contactDataInformation.gsm,
                                    Location = x.contactDataInformation.location
                                },
                                //no need it i dont even use identity
                                //UserID = x.userID
                            });
                        }
                    });
                }
                report.ContactCount = report.Contacts.Count;
                report.Status = "Tamamlandı !";
                var updateResult = await _reportCollection.FindOneAndReplaceAsync(x => x.Id == report.Id, report);

            }
            catch (System.Exception ex)
            {

            }

        }
    }
}
