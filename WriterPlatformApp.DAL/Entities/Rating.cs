using System.ComponentModel.DataAnnotations;

namespace WriterPlatformApp.DAL.Entities
{
    public class Rating
    {
        [Key]
        public int Id { get; set; }

        public int RatingTypeId { get; set; }

        public string UserProfilesId { get; set; }

        public int TitleId { get; set; }

        public RatingType RatingTypes { get; set; }

        public UserProfile UserProfiles { get; set; }

        public Title Titles { get; set; }
    }
}
