using EPMS_API.Models;
using EPMS_API.Models.Comment;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace EPMS_API.Services;

public class CommentService
{
    private readonly IMongoCollection<Comment> _commentsCollection;

    public CommentService(IOptions<EpmsDatabaseSettings> epmsDatabaseSettings)
    {
        var mongoClient = new MongoClient("mongodb://");

        var mongoDatabase = mongoClient.GetDatabase("epms_DB");

        _commentsCollection = mongoDatabase.GetCollection<Comment>("comment");
    }

    public async Task<List<Comment>> GetAsync() => await _commentsCollection.Find(_ => true).ToListAsync();

    public async Task<Comment?> GetAsync(string id) => await _commentsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Comment newComment) => await _commentsCollection.InsertOneAsync(newComment);

    public async Task UpdateAsync(string id, Comment updatedComment) =>
        await _commentsCollection.ReplaceOneAsync(x => x.Id == id, updatedComment);

    public async Task RemoveAsync(string id) =>
        await _commentsCollection.DeleteOneAsync(x => x.Id == id);
}