using System;
using System.Security.Claims;
using System.Threading.Tasks;
using WriterPlatformApp.BLL.BO;
using WriterPlatformApp.BLL.Identity;

namespace WriterPlatformApp.BLL.Implementatiton
{
    public interface IUserBOImpl : IDisposable
    {
        /**
         *  Создание пользователя
         * */
        Task<OperationDetails> Create(UserBO userBo);
        /**
         *  Нахождение пользователя по id
         */
        UserBO GetUserById(string id);
        /**
         * Аутентификация пользователя
         * */
        Task<ClaimsIdentity> Authenticate(UserBO userBo);
        /**
         * Удаление пользователя
         * */
        Task<OperationDetails> Remove(UserBO userBo);
        /**
         * Изменить данные пользователя
         * */
        Task<OperationDetails> Edit(UserBO userBo);
        /**
         * Проверка на удаленный аккаунт
         * */
        bool GetLocked(UserBO userBo);
        /**
         * Смена пароля
         * */
        Task<OperationDetails> ChangePassword(ChangePasswordBO userBo);
    }
}
