using System;
using System.ComponentModel.DataAnnotations;

namespace WriterPlatformApp.DAL.Entities
{
    public class Message
    {
        [Key]
        public int Id { get; set; }

        public string MessageText { get; set; }

        public DateTime MessageDate { get; set; }

        public string UserProfilesId { get; set; }

        public int TitleId { get; set; }

        public UserProfile UserProfiles { get; set; }

        public Title Titles { get; set; }
    }
}
