using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WriterPlatformApp.WEB.ViewModels
{
    public class TitleViewModel
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Название")]
        public string TitleName { get; set; }
        [Display(Name = "Дата публикации")]
        public DateTime PublicationDate { get; set; }
        [Display(Name = "Рейтинг")]
        public int Rating { get; set; }
        public string ContentPath { get; set; }
        [Display(Name = "Жанр")]
        public int GenreId { get; set; }
        public string UserProfilesId { get; set; }
        public GenreViewModel Genres { get; set; }
        public UserViewModel UserProfiles { get; set; }
        public ICollection<MessageViewModel> Messages { get; set; }
    }
}