using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SeturContacts.Services.Report.DTOs;
using SeturContacts.Services.Report.Services;
using SeturContacts.Shared.ControlleBases;
using SeturContacts.Shared.Messages;
using System.Threading.Tasks;

namespace SeturContacts.Services.Report.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : CustomBaseController
    {

        private readonly ISendEndpointProvider _sendEndpointProvider;

        private readonly IReportService _reportService;
        public ReportsController(IReportService reportService, ISendEndpointProvider sendEndpointProvider)
        {
            _sendEndpointProvider = sendEndpointProvider;
            _reportService = reportService;
        }

        /// <summary>
        /// Returns All Created Reports
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllReports()
        {
            var response = await _reportService.GetAllReportDatasAsync();
            return CreateActionResultInstance(response); ;
        }

        /// <summary>
        /// Creates new report
        /// </summary>
        /// <param name="reportDataCreateDTO"></param>
        /// <returns>returns created report object</returns>
        [HttpPost]
        public async Task<IActionResult> CreateNewReport(ReportDataCreateDTO reportDataCreateDTO)
        {
            var sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(new System.Uri("queue:create-report-service"));
            var response = await _reportService.CreateReportDataAsync(reportDataCreateDTO);

            var createReportMessageCommand = new CreateReportMessageCommand();
            createReportMessageCommand.Location = reportDataCreateDTO.Location;
            createReportMessageCommand.Id = response.Data.Id;
            response.Data.Contacts.ForEach(x =>
            {
                createReportMessageCommand.Contacts.Add(new ContactData
                {
                    Id = x.Id,
                    Company = x.Company,
                    Name = x.Name,
                    Surname = x.Surname,
                    ContactDataInformation = new ContactInfo
                    {
                        Email = x.ContactDataInformation.Email,
                        GSM = x.ContactDataInformation.GSM,
                        Location = x.ContactDataInformation.Location
                    },
                    UserID = x.UserID
                });
            });

            await sendEndpoint.Send(createReportMessageCommand);

            return CreateActionResultInstance(response);


        }
        /// <summary>
        /// Returns Report by given ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>returns Report Object</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetReportByID(string id)
        {
            var response = await _reportService.GetReportDataByIdAsync(id);
            return CreateActionResultInstance(response);
        }
    }
}
