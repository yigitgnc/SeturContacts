namespace EsturContacts.Services.Contact.Settings
{
    public interface IDatabaseSettings
    {
        public string ContactDataCollectionName { get; set; }
        //public string ContactDataInfoCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
