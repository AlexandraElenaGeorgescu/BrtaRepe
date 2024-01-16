namespace NewsAPI.Settings
{
    public class MongoDBSettings : IMongoDBSettings
    {
        public string AnnouncementsCollectionName { get; set; }
        public string ConnectionString { get ; set; }
        public string DatabaseName { get ; set ; }
        public string CategoriesCollectionName { get; set; }
    }
}
