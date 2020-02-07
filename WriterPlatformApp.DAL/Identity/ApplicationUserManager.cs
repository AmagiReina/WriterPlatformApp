using Microsoft.AspNet.Identity;
using WriterPlatformApp.DAL.Entities;

namespace WriterPlatformApp.DAL.Identity
{
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store) : base(store)
        { }
    }
}
