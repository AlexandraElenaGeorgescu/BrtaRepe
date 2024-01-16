    using MongoDB.Driver;
    using NewsAPI.Models;
    using NewsAPI.Settings;

    namespace NewsAPI.Services {
      public class AnnouncementsCollectionService : IAnnouncementCollectionService{

      private readonly IMongoCollection<Announcement> _announcements;

      public AnnouncementsCollectionService(IMongoDBSettings settings)
      {
      var client = new MongoClient(settings.ConnectionString);
      var database = client.GetDatabase(settings.DatabaseName);

      _announcements = database.GetCollection<Announcement>(settings.AnnouncementsCollectionName);
      }

      public async Task<List<Announcement>> GetAll()
      {
      var result = await _announcements.FindAsync(announcement => true);

      return result.ToList();
      }

        


      public async Task< bool> Create(Announcement announcement)
      {
        announcement.Id = Guid.NewGuid();

        await  _announcements.InsertOneAsync(announcement);

        return true;
      }

      public async Task<bool> Delete(Guid id)
      {
        var announcementInList = await _announcements.FindAsync(a => a.Id == id);
            
        if(announcementInList == null)
        {
            return false;
        }

        var isDeleted= await _announcements.DeleteOneAsync(a => a.Id == id);

        return isDeleted.IsAcknowledged && isDeleted.DeletedCount>0;
      }

      public async Task< Announcement> Get(Guid id)
      {
      return (await _announcements.FindAsync(announcement => announcement.Id == id)).FirstOrDefault();

      }





      public async Task<List<Announcement>> GetAnnouncementsByCategoryId(string categoryId)
      {
                           

        var  filteredAnnouncements = await _announcements.FindAsync(a => a.CategoryId == categoryId);

        return filteredAnnouncements.ToList();
      }

      public async Task< bool> Update(Guid id, Announcement announcement)
      {
        var existingAnnouncement = await _announcements.ReplaceOneAsync(a => a.Id == id,announcement);
        if(existingAnnouncement.IsAcknowledged==false && existingAnnouncement.ModifiedCount == 0)
        {
         await  _announcements.InsertOneAsync(announcement);
        }
        return (existingAnnouncement.IsAcknowledged && existingAnnouncement.ModifiedCount > 0);
                           
                            
      }
    }
}

