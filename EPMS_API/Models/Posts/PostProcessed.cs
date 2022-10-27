namespace EPMS_API.Models.Posts;

public class PostProcessed
{
    public string postId { get; set; }
    public string postUserId { get; set; }
    public string postUserPhotoPath { get; set; }
    public string postUsername { get; set; }
    public string postUserBase { get; set; }
    public long postTime { get; set; }
    public string postContent { get; set; }
    public double postRatingValue { get; set; }
    public double postSelfRatingValue { get; set; }
    public PostFileInfo[] postFileInfo { get; set; }
    public bool isSubscribed { get; set; }
    public bool isDeletable { get; set; }

    public bool isSelf { get; set; }

    public PostProcessed(Post post, string postUserPhotoPath, string postUsername, string postUserBase, double postRatingValue,
        double postSelfRatingValue, bool isSubscribed, bool isDeletable, bool isSelf)
    {
        postId = post.Id;
        postUserId = post.UserId;
        postTime = post.PostTime;
        postContent = post.PostContent;
        postFileInfo = post.FileInfo;

        this.postRatingValue = postRatingValue;
        this.postSelfRatingValue = postSelfRatingValue;
        this.isSubscribed = isSubscribed;
        this.postUserPhotoPath = postUserPhotoPath;
        this.postUsername = postUsername;
        this.postUserBase = postUserBase;
        this.isDeletable = isDeletable;
        this.isSelf = isSelf;
    }
}