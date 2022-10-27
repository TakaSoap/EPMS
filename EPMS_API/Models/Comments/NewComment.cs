namespace EPMS_API.Models.Comment
{
    public class NewComment
    {
        public string postId { get; set; }
        public string content { get; set; }

        public NewComment(string postId, string content)
        {
            this.postId = postId;
            this.content = content;
        }
    }
}
