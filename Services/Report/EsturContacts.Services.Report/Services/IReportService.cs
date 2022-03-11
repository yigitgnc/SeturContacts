using SeturContacts.Services.Report.DTOs;
using SeturContacts.Services.Report.Models;
using SeturContacts.Shared.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SeturContacts.Services.Report.Services
{
    public interface IReportService
    {
        //Rehberdeki kişilerin bulundukları konuma göre istatistiklerini çıkartan bir rapor talebi
        Task<Response<ReportData>> CreateReportDataAsync(ReportDataCreateDTO contact);
        //Sistemin oluşturduğu raporların listelenmesi
        Task<Response<List<ReportData>>> GetAllReportDatasAsync();
        //Sistemin oluşturduğu bir raporun detay bilgilerinin getirilmesi
        Task<Response<ReportData>> GetReportDataByIdAsync(string id);
    }
}
