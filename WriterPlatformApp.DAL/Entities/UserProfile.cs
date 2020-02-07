using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace WriterPlatformApp.DAL.Entities
{
  
    public class UserProfile
    {
        [Key]
        [ForeignKey("ApplicationUser")]
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool isLocked { get; set; }
        public string Role { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
