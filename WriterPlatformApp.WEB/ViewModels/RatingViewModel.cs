namespace WriterPlatformApp.WEB.ViewModels
{
    public class RatingViewModel
    {
        public int Id { get; set; }

        public int RatingTypeId { get; set; }

        public string UserProfilesId { get; set; }

        public int TitleId { get; set; }

        public RatingTypeViewModel RatingTypes { get; set; }

        public UserViewModel UserProfiles { get; set; }

        public TitleViewModel Titles { get; set; }
    }
}