using System.ComponentModel.DataAnnotations;

namespace WriterPlatformApp.WEB.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool isLocked { get; set; }
        public string Role { get; set; }
        
    }
}