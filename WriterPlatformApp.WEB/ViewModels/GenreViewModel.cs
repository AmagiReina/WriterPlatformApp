using System.ComponentModel.DataAnnotations;

namespace WriterPlatformApp.WEB.ViewModels
{
    public class GenreViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Жанр")]
        public string GenreName { get; set; }
    }
}