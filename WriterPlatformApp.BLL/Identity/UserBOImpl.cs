using AutoMapper;
using Microsoft.AspNet.Identity;
using System;
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
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public UserBOImpl(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public UserBO GetUserById(string id)
        {
            var user = unitOfWork.UserManager.FindById(id);
;
            UserBO userMap = mapper.Map<UserBO>(user);

            return userMap;
        }

        public bool GetLocked(UserBO userBo)
        {
            var user = unitOfWork.UserManager.FindByName(userBo.UserName);
            
            if (user != null)
            {
                if (user.IsLocked)
                    return true;
                else
                    return false;
            }
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
                { Id = user.Id, UserName = userBo.UserName,
                Email = userBo.Email, Password = user.PasswordHash};
                unitOfWork.UserProfile.Create(userProfile);

                // Добавляем роль
                await unitOfWork.UserManager.AddToRoleAsync(user.Id, userBo.Role);
                userProfile.Role = userBo.Role;
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
            ApplicationUser user = await unitOfWork.UserManager.FindByIdAsync(userBo.Id);
            UserProfile profile = unitOfWork.UserProfile.FindByString(userBo.Id);

            if (user != null && !user.IsLocked && profile != null && !profile.isLocked)
            {
                user.UserName = userBo.UserName;
                user.Email = userBo.Email;
                
                profile.UserName = userBo.UserName;
                profile.Email = userBo.Email;

                unitOfWork.UserProfile.Update(profile);
                unitOfWork.UserProfile.Save();

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

        public async Task<OperationDetails> ChangePassword(ChangePasswordBO changePasswordBo)
        {
            ApplicationUser user = await unitOfWork.UserManager.FindByIdAsync(changePasswordBo.Id);
            UserProfile profile = unitOfWork.UserProfile.FindByString(changePasswordBo.Id);

            if (user != null && !user.IsLocked && profile != null && !profile.isLocked)
            {
                user.PasswordHash = unitOfWork.UserManager.PasswordHasher.HashPassword(changePasswordBo.NewPassword);
                IdentityResult resultReset = await unitOfWork.UserManager.UpdateAsync(user);

                profile.Password = user.PasswordHash;
                if (resultReset.Succeeded)
                {
                    unitOfWork.UserProfile.Update(profile);
                    unitOfWork.UserProfile.Save();
                    await unitOfWork.SaveAsync();
                }
                return new OperationDetails(false, "Не удалось сменить пароль", "");
            }
            else
            {
                return new OperationDetails(false, "Пользователя с таким именем не существует", "UserName");
            }
        }

        public void Remove(UserBO userBo)
        {
            ApplicationUser user = unitOfWork.UserManager.FindByName(userBo.UserName);

            if (user != null && user.IsLocked == false)
            {
                Random random = new Random();
                user.UserName = "Anonymous" + random.Next();
                user.IsLocked = true;
                // сохраняем профиль
                var profile = unitOfWork.UserProfile.GetAll().Where(x => x.Id == user.Id)
                    .FirstOrDefault();
                profile.UserName = user.UserName;
                unitOfWork.UserProfile.Save();
                IdentityResult result = unitOfWork.UserManager.Update(user);
                if (result.Succeeded)
                {
                    unitOfWork.SaveAsync();
                }
            }
        }

    }
}
