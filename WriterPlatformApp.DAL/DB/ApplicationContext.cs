using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using WriterPlatformApp.DAL.Entities;
using WriterPlatformApp.DAL.Identity;

namespace WriterPlatformApp.DAL.DB
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {

        public ApplicationContext() : base("name=WriterPlatformDb")
        {
            Database.SetInitializer(new DbInitializer());
        }


        public virtual DbSet<UserProfile> UserProfiles { get; set; }

        public virtual DbSet<Genre> Genres { get; set; }

        public virtual DbSet<Title> Titles { get; set; }

        public virtual DbSet<Message> Messages { get; set; }

        public virtual DbSet<Rating> Ratings { get; set; }

    
    }


    class DbInitializer : CreateDatabaseIfNotExists<ApplicationContext>
    {
        protected override void Seed(ApplicationContext dbContext)
        {
            
        }
    }
}


