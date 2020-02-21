using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WriterPlatformApp.DAL.Entities
{
    public class Title
    {
        [Key]
        public int Id { get; set; }

        public string TitleName { get; set; }

        public DateTime PublicationDate { get; set; }

        public int Rating { get; set; }

        public string ContentPath { get; set; }

        public int GenreId { get; set; }

        public string UserProfilesId { get; set; }

        public Genre Genres { get; set; }

        public UserProfile UserProfiles { get; set; }

        public ICollection<Message> Messages { get; set; }
    }
}
