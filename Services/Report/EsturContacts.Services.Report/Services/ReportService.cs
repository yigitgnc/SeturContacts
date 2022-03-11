using SeturContacts.Services.Report.DTOs;
using SeturContacts.Services.Report.Models;
using SeturContacts.Shared.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SeturContacts.Services.Report.Services
{
    public class ReportService : IReportService
    {
        public Task<Response<ReportData>> CreateReportDataAsync(ReportDataCreateDTO contact)
        {
            throw new System.NotImplementedException();
        }

        public Task<Response<List<ReportData>>> GetAllReportDatasAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<Response<ReportData>> GetReportDataByIdAsync(string id)
        {
            throw new System.NotImplementedException();
        }
    }
}
