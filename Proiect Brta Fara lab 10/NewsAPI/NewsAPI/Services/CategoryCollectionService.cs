using MongoDB.Driver;
using NewsAPI.Models;
using NewsAPI.Settings;

namespace NewsAPI.Services
{
    public class CategoryCollectionService : ICategoryCollectionservice
    {
        private readonly IMongoCollection<Category> categories;

        public CategoryCollectionService(IMongoDBSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            categories = database.GetCollection<Category>(settings.CategoriesCollectionName);
        }

        public async Task<bool> Create(Category model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model), "Category model cannot be null.");
            }

            await categories.InsertOneAsync(model);
            return true;
        }

        public async Task<Category> Get(Guid id)
        {
            var result = (await categories.FindAsync<Category>(c => c.Id == id)).FirstOrDefaultAsync();
            return await result;
        }

        public async Task<List<Category>> GetAll()
        {
            var result = await categories.FindAsync<Category>(c => true);
            return await result.ToListAsync();
        }

        public async Task<bool> Update(Guid id, Category model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model), "Category model cannot be null.");
            }

            model.Id = id;
            var result = await categories.ReplaceOneAsync(c => c.Id == id, model);

            return result.IsAcknowledged && result.ModifiedCount > 0;
        }

        public async Task<bool> Delete(Guid id)
        {
            var result = await categories.DeleteOneAsync(c => c.Id == id);

            return result.IsAcknowledged && result.DeletedCount > 0;
        }
    }
}
