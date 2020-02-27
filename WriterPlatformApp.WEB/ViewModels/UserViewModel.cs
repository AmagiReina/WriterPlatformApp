using System.ComponentModel.DataAnnotations;

namespace WriterPlatformApp.WEB.ViewModels
{
    public class UserViewModel
    {
        [Display(Name = "Автор")]
        public string UserName { get; set; }
        public string Password { get; set; }
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }
        public bool isLocked { get; set; }
        public string Role { get; set; }
    }
}