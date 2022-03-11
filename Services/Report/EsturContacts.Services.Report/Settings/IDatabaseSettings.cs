namespace SeturContacts.Services.Report.Settings
{
    public interface IDatabaseSettings
    {
        public string ReportDataCollectionName { get; set; }
        //public string ContactDataInfoCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
