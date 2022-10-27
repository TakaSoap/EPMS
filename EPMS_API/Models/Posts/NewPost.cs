namespace EPMS_API.Models.Posts
{
    public class NewPost
    {
        public string content { get; set; }

        public PostFileInfo[] fileInfo { get; set; }

    }
}
