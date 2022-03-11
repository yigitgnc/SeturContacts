namespace SeturContacts.Services.Contact.Settings
{
    public class DatabaseSettings : IDatabaseSettings
    {
        public string ContactDataCollectionName { get; set; }
        //decidet to store it as an object
        //public string ContactDataInfoCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
