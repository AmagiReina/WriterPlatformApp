using System;

namespace WriterPlatformApp.WEB.ViewModels
{
    public class MessageViewModel
    {
        public int Id { get; set; }
        public string MessageText { get; set; }
        public DateTime MessageDate { get; set; }
        public string UserProfilesId { get; set; }
        public int TitleId { get; set; }
        public UserViewModel UserProfiles { get; set; }
        public TitleViewModel Titles { get; set; }
    }
}