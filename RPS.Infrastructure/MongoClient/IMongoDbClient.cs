using RPS.Shared.Rating;

namespace RPS.Infrastructure.MongoClient;

public interface IMongoDbClient
{
    public Task<List<UserRatingMongoDto>> GetAsync();

    public Task<UserRatingMongoDto> GetAsync(string key);

    public Task CreateAsync(UserRatingMongoDto newMetadata);

    public Task UpdateAsync(string key, UserRatingMongoDto updatedMetadata);

    public Task RemoveAsync(string key);
}