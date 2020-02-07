using Microsoft.AspNet.Identity.EntityFramework;

namespace WriterPlatformApp.DAL.Entities
{
    public class ApplicationUser: IdentityUser
    {
        public bool IsLocked { get; set; }
    }
}
