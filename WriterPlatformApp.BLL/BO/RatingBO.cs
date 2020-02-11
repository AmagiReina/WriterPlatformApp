namespace WriterPlatformApp.BLL.BO
{
    public class RatingBO
    {
        public int Id { get; set; }

        public int RatingTypeId { get; set; }

        public string UserProfilesId { get; set; }

        public int TitleId { get; set; }

        public RatingTypeBO RatingTypes { get; set; }

        public UserBO UserProfiles { get; set; }

        public TitleBO Titles { get; set; }
    }
}
