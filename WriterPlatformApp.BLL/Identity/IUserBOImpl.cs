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
    }
}
