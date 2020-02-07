using System;
using System.Collections.Generic;

namespace WriterPlatformApp.WEB.ViewModels
{
    public class TitleViewModel
    {
        public int Id { get; set; }
        public string TitleName { get; set; }
        public DateTime PublicationDate { get; set; }
        public int Rating { get; set; }
        public byte[] Content { get; set; }
        public int GenreId { get; set; }
        public string UserProfilesId { get; set; }
        public GenreViewModel Genres { get; set; }
        public UserViewModel UserProfiles { get; set; }
        public ICollection<MessageViewModel> Messages { get; set; }
    }
}