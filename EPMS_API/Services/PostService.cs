using EPMS_API.Models;
using EPMS_API.Models.Comment;
using EPMS_API.Models.Posts;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace EPMS_API.Services;

public class PostService
{
    private readonly IMongoCollection<Post> _postsCollection;
    private readonly IMongoCollection<User> _usersCollection;
    private readonly IMongoCollection<Rating> _ratingsCollection;
    private readonly IMongoCollection<Comment> _commentsCollection;
    private readonly IMongoCollection<InternshipBase> _basesCollection;
    private readonly IMongoCollection<Subscription> _subscriptionsCollection;



    public PostService(IOptions<EpmsDatabaseSettings> epmsDatabaseSettings)
    {
        var mongoClient = new MongoClient("mongodb://");

        var mongoDatabase = mongoClient.GetDatabase("epms_DB");

        _postsCollection = mongoDatabase.GetCollection<Post>("post");

        _usersCollection = mongoDatabase.GetCollection<User>("user");

        _ratingsCollection = mongoDatabase.GetCollection<Rating>("rating");

        _commentsCollection = mongoDatabase.GetCollection<Comment>("comment");

        _basesCollection = mongoDatabase.GetCollection<InternshipBase>("internship_base");

        _subscriptionsCollection = mongoDatabase.GetCollection<Subscription>("subscription");
    }

    public async Task<List<Post>> GetAsync() => await _postsCollection.Find(_ => true).ToListAsync();

    public async Task<List<Post>> GetSelfAsync(string userId) => await _postsCollection.Find(x => x.UserId == userId).ToListAsync();

    public async Task<Post?> GetAsync(string id) => await _postsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Post newPost) => await _postsCollection.InsertOneAsync(newPost);

    public async Task UpdateAsync(string id, Post updatedPost) =>
        await _postsCollection.ReplaceOneAsync(x => x.Id == id, updatedPost);

    public async Task RemoveAsync(string id) =>
        await _postsCollection.DeleteOneAsync(x => x.Id == id);
    public async Task<User?> GetUserAsync(string id) => await _usersCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task<List<Rating>> GetRatingsByPostIdAsync(string postId) => await _ratingsCollection.Find(x => x.PostId == postId).ToListAsync();

    public async Task<Rating?> GetRatingByUserIdAndPostId(string userId, string postId) => await _ratingsCollection
        .Find(x => x.UserId == userId && x.PostId == postId).FirstOrDefaultAsync();

    public async Task<User?> GetUserByAuthingId(string authingId) => await _usersCollection.Find(x => x.UserIdAuthing == authingId).FirstOrDefaultAsync();

    public async Task<List<Post>> GetPostByContentAsync(NewPostContent newPostContent) => await _postsCollection.Find(x => x.PostContent == newPostContent.Content).ToListAsync();

    public async Task<List<Comment>> GetCommentsByPostIdAsync(string postId) => await _commentsCollection.Find(x => x.PostId == postId).ToListAsync();

    public async Task<InternshipBase?> GetBaseAsync(string id) => await _basesCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateCommentAsync(Comment newComment) => await _commentsCollection.InsertOneAsync(newComment);

    public async Task RemoveCommentAsync(string commentId) => await _commentsCollection.DeleteOneAsync(x => x.Id == commentId);

    public async Task UpdateRatingAsync(Rating newRating) => await _ratingsCollection.ReplaceOneAsync(x => x.UserId == newRating.UserId && x.PostId == newRating.PostId, newRating, new ReplaceOptions { IsUpsert = true });

    public async Task SubscribeAsync(Subscription subscription) => await _subscriptionsCollection.InsertOneAsync(subscription);

    public async Task UnsubscribeAsync(Subscription subscription) => await _subscriptionsCollection.DeleteOneAsync(x => x.SubscriberId == subscription.SubscriberId && x.SubscribedId == subscription.SubscribedId);

    public async Task<Subscription?> GetSubscription(string subscriberId, string subscribedId) => await _subscriptionsCollection.Find(x => x.SubscriberId == subscriberId && x.SubscribedId == subscribedId).FirstOrDefaultAsync();

}