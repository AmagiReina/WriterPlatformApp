using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Threading.Tasks;
using WriterPlatformApp.DAL.DB;
using WriterPlatformApp.DAL.Entities;
using WriterPlatformApp.DAL.Identity;
using WriterPlatformApp.DAL.Repostiory;

namespace WriterPlatformApp.DAL.UnitOfWork
{
    public class UnitOfWorkImpl : IUnitOfWork
    {
        private ApplicationContext db;
        private ApplicationUserManager userManager;
        private ApplicationRoleManager roleManager;
        private GenericRepository<Genre> genre;
        private GenericRepository<Message> message;
        private GenericRepository<Title> title;
        private GenericRepository<UserProfile> userProfile;
        private GenericRepository<Rating> rating;
        private GenericRepository<RatingType> ratingType;
        private bool disposed = false;

        public UnitOfWorkImpl()
        {
            db = new ApplicationContext();           
        }

        public GenericRepository<Genre> Genre
        {
            get
            {
                if (genre == null)
                {
                    genre = new GenericRepository<Genre>(db);
                }

                return genre;
            }
        }

        public GenericRepository<Message> Message
        {
            get
            {
                if (message == null)
                {
                    message = new GenericRepository<Message>(db);
                }
                return message;
            }
        }

        public GenericRepository<Title> Title
        {
            get
            {
                if (title == null)
                {
                    title = new GenericRepository<Title>(db);
                }
                return title;
            }
        }

        public GenericRepository<UserProfile> UserProfile
        {
            get
            {
                if (userProfile == null)
                {
                    userProfile = new GenericRepository<UserProfile>(db);
                }
                return userProfile;
            }
        }

        public GenericRepository<Rating> Rating
        {
            get
            {
                if (rating == null)
                {
                    rating = new GenericRepository<Rating>(db);
                }
                return rating;
            }
        }

        public ApplicationRoleManager RoleManager
        {
            get
            {
                if (roleManager == null)
                {
                    roleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(db));
                }
                return roleManager;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                if (userManager == null)
                {
                    userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(db));
                }
                return userManager;
            }
        }

        public GenericRepository<RatingType> RatingType
        {
            get
            {
                if (ratingType == null)
                {
                    ratingType = new GenericRepository<RatingType>(db);
                }
                return ratingType;
            }
        }

        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }

        public void Save()
        {
            db.SaveChanges();
        }


        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
