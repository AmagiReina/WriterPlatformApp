namespace WriterPlatformApp.DAL.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using WriterPlatformApp.DAL.Entities;
    using WriterPlatformApp.DAL.Identity;

    internal sealed class Configuration : DbMigrationsConfiguration<WriterPlatformApp.DAL.DB.ApplicationContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(WriterPlatformApp.DAL.DB.ApplicationContext db)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            if (db.Users.Where(x => x.UserName == "Admin").FirstOrDefault() == null)
            {
                var roleUser = new IdentityRole { Name = "user" };
                var roleAdmin = new IdentityRole { Name = "admin" };

                db.Roles.AddOrUpdate(roleUser);

                db.Roles.AddOrUpdate(roleAdmin);

                var adminIdentity = new ApplicationUserManager(new UserStore<ApplicationUser>(db));
                string password = "Qwerty_12";
                var admin = new ApplicationUser { Email = "admin@mail.ru", UserName = "Admin" };
                var result = adminIdentity.Create(admin, password);
                if (result.Succeeded)
                {
                    adminIdentity.AddToRole(admin.Id, db.Roles.Where(r => r.Name == "admin").FirstOrDefault().Name);
                }

                db.Titles.AddOrUpdate(
                        new Title
                        {
                            TitleName = "Test",
                            PublicationDate = DateTime.Now,
                            Rating = 9,
                            GenreId = 3,
                            UserProfilesId = admin.Id
                        },
                        new Title
                        {
                            TitleName = "Test1",
                            PublicationDate = DateTime.Now,
                            Rating = 6,
                            GenreId = 2,
                            UserProfilesId = admin.Id
                        }
                        );

                db.UserProfiles.AddOrUpdate(
                          new UserProfile
                          {
                              ApplicationUser = admin,
                              UserName = admin.UserName,
                              Email = admin.Email,
                              Password = admin.PasswordHash,
                              isLocked = admin.IsLocked,
                              Role = roleAdmin.Name
                          }
                      );

            }

            db.Genres.AddOrUpdate(
                new Genre { GenreName = "Повесть" },
                new Genre { GenreName = "Роман" },
                new Genre { GenreName = "Рассказ" },
                new Genre { GenreName = "Новелла" },
                new Genre { GenreName = "Притча" },
                new Genre { GenreName = "Сказка" },
                new Genre { GenreName = "Комедия" },
                new Genre { GenreName = "Трагедия" },
                new Genre { GenreName = "Драма" }
            );


            db.RatingTypes.AddOrUpdate(
                new RatingType { RatingNumber = 1 },
                new RatingType { RatingNumber = 2 },
                new RatingType { RatingNumber = 3 },
                new RatingType { RatingNumber = 4 },
                new RatingType { RatingNumber = 5 },
                new RatingType { RatingNumber = 6 },
                new RatingType { RatingNumber = 7 },
                new RatingType { RatingNumber = 8 },
                new RatingType { RatingNumber = 9 },
                new RatingType { RatingNumber = 10 }
              );

        }
    }
}
