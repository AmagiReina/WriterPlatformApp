using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using WriterPlatformApp.DAL.Entities;

namespace WriterPlatformApp.DAL.Identity
{
    /**
      * Управление ролями: добавление в базу и т.д. (роль репозиториев)
      * */
    public class ApplicationRoleManager : RoleManager<ApplicationRole>
    {
        public ApplicationRoleManager(RoleStore<ApplicationRole> store) : base(store)
        { }
    }
}
