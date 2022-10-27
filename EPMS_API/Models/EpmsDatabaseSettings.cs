namespace EPMS_API.Models
{
    public class EpmsDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string UserCollectionName { get; set; } = null!;

        public string CommentCollectionName { get; set; } = null!;

        public string PostCollectionName { get; set; } = null!;

        public string RatingCollectionName { get; set; } = null!;

        public string InternshipBaseCollectionName { get; set; } = null!;

        public string SubscriptionCollectionName { get; set; } = null!;
    }
}
