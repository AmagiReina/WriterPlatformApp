using System;
using System.Collections.Generic;

namespace WriterPlatformApp.BLL.BO
{
    public class TitleBO
    {
        public int Id { get; set; }
        public string TitleName { get; set; }
        public DateTime PublicationDate { get; set; }
        public int Rating { get; set; }
        public byte[] Content { get; set; }
        public int GenreId { get; set; }
        public string UserProfilesId { get; set; }
        public GenreBO Genres { get; set; }
        public UserBO UserProfiles { get; set; }
        public ICollection<MessageBO> Messages { get; set; }
    }
}
