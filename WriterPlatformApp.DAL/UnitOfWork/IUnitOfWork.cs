using System;
using System.Threading.Tasks;
using WriterPlatformApp.DAL.Entities;
using WriterPlatformApp.DAL.Identity;
using WriterPlatformApp.DAL.Repostiory;

namespace WriterPlatformApp.DAL.UnitOfWork
{
    public interface IUnitOfWork: IDisposable
    {
        GenericRepository<Genre> Genre { get; }
        GenericRepository<Message> Message { get; }
        GenericRepository<Title> Title { get; }
        GenericRepository<UserProfile> UserProfile { get; }
        GenericRepository<Rating> Rating { get; }
        GenericRepository<RatingType> RatingType { get; }
        ApplicationRoleManager RoleManager { get; }
        ApplicationUserManager UserManager { get; }
        Task SaveAsync();
    }
}
