using System.ComponentModel.DataAnnotations;

namespace WriterPlatformApp.WEB.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string Email { get; set; }
        public bool isLocked { get; set; }
        public string Role { get; set; }
    }
}