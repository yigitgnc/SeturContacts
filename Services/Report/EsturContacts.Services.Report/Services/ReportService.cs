using AutoMapper;
using MongoDB.Driver;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SeturContacts.Services.Report.DTOs;
using SeturContacts.Services.Report.DTOs.JsonDeserialize;
using SeturContacts.Services.Report.Models;
using SeturContacts.Services.Report.Settings;
using SeturContacts.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace SeturContacts.Services.Report.Services
{
    public class ReportService : IReportService
    {
        private readonly IMongoCollection<ReportData> _reportCollection;
        private readonly IMapper _mapper;

        public ReportService(IMapper mapper, IDatabaseSettings databaseSettings)
        {
            var mClient = new MongoClient(databaseSettings.ConnectionString);

            var database = mClient.GetDatabase(databaseSettings.DatabaseName);

            _reportCollection = database.GetCollection<ReportData>(databaseSettings.ReportDataCollectionName);
            _mapper = mapper;

        }

        public async Task<Response<ReportData>> CreateReportDataAsync(ReportDataCreateDTO report)
        {
          

            ReportData newReportData = _mapper.Map<ReportData>(report);
            newReportData.Id = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
            newReportData.Status = "Hazırlanıyor...";
            newReportData.CreatedDate = DateTime.Now;

            //this is where the cheating begin :)
            //HttpClient client = new HttpClient();
            //string allContactsString = await client.GetStringAsync("http://localhost:5011/api/Contact");
            //JsonGetContactsDTO jsonGetContacts = JsonConvert.DeserializeObject<JsonGetContactsDTO>(allContactsString);
            //newReportData.Contacts = _mapper.Map<List<ContactData>>(jsonGetContacts.data);

            await _reportCollection.InsertOneAsync(newReportData);

            return Response<ReportData>.Success(newReportData, 200);
        }

        public async Task<Response<List<ReportData>>> GetAllReportDatasAsync()
        {
            //todo: add better error management !
            Response<List<ReportData>> response;
            try
            {

                var contacts = await _reportCollection.Find(c => true).ToListAsync();
                response = Response<List<ReportData>>.Success(_mapper.Map<List<ReportData>>(contacts), 200);
            }
            catch (Exception ex)
            {
                //todo: its dangerous to return exception message !!
                response = Response<List<ReportData>>.Fail(ex.Message, 500);
            }

            return response;
        }

        public async Task<Response<ReportData>> GetReportDataByIdAsync(string id)
        {
            Response<ReportData> response;
            try
            {
                ReportData report = null;
                report = await _reportCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
                if (report == null)
                {
                    response = Response<ReportData>.Fail("ReportData Not Found", 404);
                }
                else
                {
                    response = Response<ReportData>.Success(_mapper.Map<ReportData>(report), 200);
                }
            }
            catch (Exception ex)
            {
                //no time to proper error handling :(
                response = Response<ReportData>.Fail(ex.Message, 500);
            }

            return response;
        }
    }
}
