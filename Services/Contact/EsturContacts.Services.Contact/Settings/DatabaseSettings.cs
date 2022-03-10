namespace EsturContacts.Services.Contacts.Settings
{
    public class DatabaseSettings : IDatabaseSettings
    {
        public string ContactCollectionName { get; set; }
        //decidet to store it as an object
        //public string ContactInfoCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
