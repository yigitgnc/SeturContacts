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
            var report = await _reportCollection.Find(x => x.Id == context.Message.Id).FirstOrDefaultAsync();

            ////this is where the cheating begin :)
            HttpClient client = new HttpClient();
            string allContactsString = await client.GetStringAsync("http://localhost:5011/api/Contact");
            JsonGetContactsDTO jsonGetContacts = JsonConvert.DeserializeObject<JsonGetContactsDTO>(allContactsString);
            report.Contacts = _mapper.Map<List<ContactData>>(jsonGetContacts.data);
            report.Status = "Tamamlandı !";
            var updateResult = await _reportCollection.FindOneAndReplaceAsync(x => x.Id == report.Id, report);


        }
    }
}
