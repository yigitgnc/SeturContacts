namespace EsturContacts.Services.Contacts.Settings
{
    public interface IDatabaseSettings
    {
        public string ContactCollectionName { get; set; }
        public string ContactInfoCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
