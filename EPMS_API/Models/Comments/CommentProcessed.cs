namespace EPMS_API.Models.Comment
{
    public class CommentProcessed
    {
        public string commentId { get; set; }
        public string commentUserName { get; set; }
        public string commentText { get; set; }
        public bool isDeletable { get; set; }

        public CommentProcessed(string commentId, string commentUserName, string commentText, bool isDeletable)
        {
            this.commentId = commentId;
            this.commentUserName = commentUserName;
            this.commentText = commentText;
            this.isDeletable = isDeletable;
        }
    }
}
