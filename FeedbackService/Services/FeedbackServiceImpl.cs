using FeedbackService.Model;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
namespace FeedbackService.Services
{
    public class FeedbackServiceImpl
    {
        private readonly IMongoCollection<Feedback> _feedbackCollection;

        public FeedbackServiceImpl(IOptions<DatabaseSetting> dataBaseSetting)
        {
            var mongoClient = new MongoClient(dataBaseSetting.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                dataBaseSetting.Value.DatabaseName);

            _feedbackCollection = mongoDatabase.GetCollection<Feedback>(
                dataBaseSetting.Value.FeedbackCollectionName);
        }

        public async Task<List<Feedback>> GetAsync() =>
            await _feedbackCollection.Find(_ => true).ToListAsync();

        public async Task<Feedback> GetAsync(string id) =>
            await _feedbackCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Feedback newFeedback) =>
            await _feedbackCollection.InsertOneAsync(newFeedback);

        public async Task UpdateAsync(string id, Feedback updateFeedback) =>
            await _feedbackCollection.ReplaceOneAsync(x => x.Id == id, updateFeedback);

        public async Task RemoveAsync(string id) =>
            await _feedbackCollection.DeleteOneAsync(x => x.Id == id);
    }
}
