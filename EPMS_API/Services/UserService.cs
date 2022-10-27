using EPMS_API.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace EPMS_API.Services;

public class UserService
{
    private readonly IMongoCollection<User> _usersCollection;
    private readonly IMongoCollection<InternshipBase> _basesCollection;

    public UserService(IOptions<EpmsDatabaseSettings> epmsDatabaseSettings)
    {
        var mongoClient = new MongoClient("mongodb://");

        var mongoDatabase = mongoClient.GetDatabase("epms_DB");

        _usersCollection = mongoDatabase.GetCollection<User>("user");
        _basesCollection = mongoDatabase.GetCollection<InternshipBase>("internship_base");

    }

    public async Task<List<User>> GetAsync() => await _usersCollection.Find(_ => true).ToListAsync();

    public async Task<User?> GetAsync(string id) => await _usersCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(User newUser) => await _usersCollection.InsertOneAsync(newUser);

    public async Task UpdateAsync(string id, User updatedUser) =>
        await _usersCollection.ReplaceOneAsync(x => x.Id == id, updatedUser);

    public async Task<User?> GetUserByAuthingId(string authingId) => await _usersCollection.Find(x => x.UserIdAuthing == authingId).FirstOrDefaultAsync();

    public async Task<List<User>> GetStudentsAsync(string baseId) => await _usersCollection.Find(x => x.baseId == baseId && x.UserType == "student").ToListAsync();

    public async Task RemoveAsync(string id) =>
        await _usersCollection.DeleteOneAsync(x => x.Id == id);

    public async Task<InternshipBase?> GetBaseAsync(string id) => await _basesCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
}