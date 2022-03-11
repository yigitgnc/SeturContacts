namespace SeturContacts.Services.Report.Settings
{
    public class DatabaseSettings : IDatabaseSettings
    {
        public string ReportDataCollectionName { get; set; }
        //decidet to store it as an object
        //public string ContactDataInfoCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
