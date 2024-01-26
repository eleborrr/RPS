using Microsoft.Extensions.Options;
using MongoDB.Driver;
using RPS.Shared.Configs;
using RPS.Shared.Rating;

namespace RPS.Infrastructure.MongoClient;

public class MongoDbClient: IMongoDbClient
{
    private readonly IMongoCollection<UserRatingMongoDto> _ratingCollection;

    public MongoDbClient(IOptions<MongoDbConfig> mongoDbConfig)
    {
        var mongoClient = new MongoDB.Driver.MongoClient(
            mongoDbConfig.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            mongoDbConfig.Value.DatabaseName);

        _ratingCollection = mongoDatabase.GetCollection<UserRatingMongoDto>(
            mongoDbConfig.Value.RatingCollectionName);
    }

    public async Task<List<UserRatingMongoDto>> GetAsync() =>
        await _ratingCollection.Find(_ => true).ToListAsync();

    public async Task<UserRatingMongoDto?> GetAsync(string key) =>
        await _ratingCollection.Find(b => b.UserId == key).FirstOrDefaultAsync();

    public async Task CreateAsync(UserRatingMongoDto newRating) =>
        await _ratingCollection.InsertOneAsync(newRating);

    public async Task UpdateAsync(string key, UserRatingMongoDto updatedRating) =>
        await _ratingCollection.ReplaceOneAsync(b => b.UserId == key, updatedRating);

    public async Task RemoveAsync(string key) =>
        await _ratingCollection.DeleteOneAsync(b => b.UserId == key);
}