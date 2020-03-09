using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using WriterPlatformApp.DAL.Entities;

namespace WriterPlatformApp.DAL.DB
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {

        public ApplicationContext() : base("name=WriterPlatformDb")
        {
           
        }


        public virtual DbSet<UserProfile> UserProfiles { get; set; }

        public virtual DbSet<Genre> Genres { get; set; }

        public virtual DbSet<Title> Titles { get; set; }

        public virtual DbSet<Message> Messages { get; set; }

        public virtual DbSet<Rating> Ratings { get; set; }

        public virtual DbSet<RatingType> RatingTypes { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Title>()
                .HasMany(t => t.Messages)               
                .WithRequired()
                .HasForeignKey(m => m.TitleId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Title>()
                .HasRequired(t => t.Genres)
                .WithMany()
                .HasForeignKey<int>(t => t.GenreId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Rating>()
              .HasRequired(r => r.RatingTypes)
              .WithMany()
              .HasForeignKey<int>(r => r.RatingTypeId)
              .WillCascadeOnDelete(false);

            modelBuilder.Entity<Rating>()
              .HasRequired(r => r.Titles)
              .WithMany()
              .HasForeignKey<int>(r => r.TitleId)
              .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
           
        }

    }
}


