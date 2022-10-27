using EPMS_API.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace EPMS_API.Services;

public class RatingService
{
    private readonly IMongoCollection<Rating> _ratingsCollection;

    public RatingService(IOptions<EpmsDatabaseSettings> epmsDatabaseSettings)
    {
        var mongoClient = new MongoClient("mongodb://");

        var mongoDatabase = mongoClient.GetDatabase("epms_DB");

        _ratingsCollection = mongoDatabase.GetCollection<Rating>("rating");
    }

    public async Task<List<Rating>> GetAsync() => await _ratingsCollection.Find(_ => true).ToListAsync();

    public async Task<Rating?> GetAsync(string id) => await _ratingsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Rating newRating) => await _ratingsCollection.InsertOneAsync(newRating);

    public async Task UpdateAsync(string id, Rating updatedRating) =>
        await _ratingsCollection.ReplaceOneAsync(x => x.Id == id, updatedRating);

    public async Task RemoveAsync(string id) =>
        await _ratingsCollection.DeleteOneAsync(x => x.Id == id);
}