using System;

namespace WriterPlatformApp.BLL.BO
{
    public class MessageBO
    {
        public int Id { get; set; }
        public string MessageText { get; set; }
        public DateTime MessageDate { get; set; }
        public string UserProfilesId { get; set; }
        public int TitleId { get; set; }
        public UserBO UserProfiles { get; set; }
        public TitleBO Titles { get; set; }
    }
}
