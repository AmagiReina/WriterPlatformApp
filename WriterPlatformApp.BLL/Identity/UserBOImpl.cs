using Microsoft.AspNet.Identity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WriterPlatformApp.BLL.BO;
using WriterPlatformApp.BLL.Identity;
using WriterPlatformApp.DAL.Entities;
using WriterPlatformApp.DAL.UnitOfWork;

namespace WriterPlatformApp.BLL.Implementatiton
{
    public class UserBOImpl: IUserBOImpl
    {
        private IUnitOfWork unitOfWork;

        public UserBOImpl(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public bool GetLocked(UserBO userBo)
        {
            var user = unitOfWork.UserManager.FindByName(userBo.UserName);

            if (user.IsLocked)
                return true;
            else
                return false;
        }

        public async Task<ClaimsIdentity> Authenticate(UserBO userBo)
        {
            ClaimsIdentity claim = null;
            // Находим пользователя
            ApplicationUser user = await unitOfWork.UserManager.FindAsync(userBo.UserName, userBo.Password);
            // Авторизуем его и возвращаем обЪект ClaimsIdentity
            if (user != null && !user.IsLocked)
                claim = await unitOfWork.UserManager.CreateIdentityAsync(
                    user, DefaultAuthenticationTypes.ApplicationCookie);

            return claim;
        }

        public async Task<OperationDetails> Create(UserBO userBo)
        {
            ApplicationUser user = await unitOfWork.UserManager.FindByNameAsync(userBo.UserName);
            if (user == null)
            {
                user = new ApplicationUser { UserName = userBo.UserName, Email = userBo.Email };
                
                var result = await unitOfWork.UserManager.CreateAsync(user, userBo.Password);

                if (result.Errors.Count() > 0)
                     return new OperationDetails(false, result.Errors.FirstOrDefault(), "");

                // Создаем профиль клиента
                UserProfile userProfile = new UserProfile
                { Id = user.Id,  Email = user.Email, UserName = user.UserName };
                unitOfWork.UserProfile.Create(userProfile);

                // Добавляем роль
                await unitOfWork.UserManager.AddToRoleAsync(user.Id, userBo.Role);
                await unitOfWork.SaveAsync();

                return new OperationDetails(true, "Регистрация успешно завершена", "");
            }
            else
            {
                return new OperationDetails(false, "Пользователя с таким логином уже существует", "UserName");
            }
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }

        public async Task<OperationDetails> Edit(UserBO userBo)
        {
            ApplicationUser user = await unitOfWork.UserManager.FindByNameAsync(userBo.UserName);

            if (user != null && !user.IsLocked)
            {
                user.UserName = userBo.UserName;
                user.Email = userBo.Email;
                IdentityResult result = await unitOfWork.UserManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    await unitOfWork.SaveAsync();

                }
                return new OperationDetails(true, "Данные пользователя успешно изменены", "");
            }
            else
            {
                return new OperationDetails(false, "Пользователя с таким именем не существует", "UserName");
            }
        }

        public async Task<OperationDetails> Remove(UserBO userBo)
        {
            ApplicationUser user = await unitOfWork.UserManager.FindByNameAsync(userBo.UserName);

            if (user != null && !user.IsLocked)
            {
                user.UserName = "Аноним";
                user.IsLocked = true;
                IdentityResult result = await unitOfWork.UserManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    await unitOfWork.SaveAsync();
                    
                }
                return new OperationDetails(true, "Пользователь удален", "");
            }
            else
            {
                return new OperationDetails(false, "Пользователя с таким именем не существует", "UserName");
            }


        }
    }
}
