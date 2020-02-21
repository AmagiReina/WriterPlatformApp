namespace WriterPlatformApp.DAL.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
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
            // check

            const int ZERO = 0;
            if (db.Users.Select(x => x).Count() == ZERO) // проверка таблицы с пользователями
            {
                #region Создание ролей
                var roleUser = new IdentityRole { Name = "user" };
                var roleAdmin = new IdentityRole { Name = "admin" };

                db.Roles.AddOrUpdate(roleUser);
                db.Roles.AddOrUpdate(roleAdmin);
                #endregion

                #region Создание администраторской учетки
                var adminIdentity = new ApplicationUserManager(new UserStore<ApplicationUser>(db));
                string password = "Qwerty_12";             
                var admin = new ApplicationUser { Email = "admin@mail.ru", UserName = "Admin" };
                var result = adminIdentity.Create(admin, password);
                #endregion

                #region Добавление роли администратора администраторской учетке
                if (result.Succeeded)
                {
                    adminIdentity.AddToRole(admin.Id, db.Roles.Where(r => r.Name == "admin").FirstOrDefault().Name);
                }
                #endregion

                #region Добавление тестовых произведений
                db.Titles.AddOrUpdate(
                        new Title
                        {
                            TitleName = "Test",
                            PublicationDate = DateTime.Now,
                            Rating = ZERO,
                            GenreId = 3,
                            UserProfilesId = admin.Id
                        },
                        new Title
                        {
                            TitleName = "Test1",
                            PublicationDate = DateTime.Now,
                            Rating = ZERO,
                            GenreId = 2,
                            UserProfilesId = admin.Id
                        },
                        new Title
                        {
                            TitleName = "Test2",
                            PublicationDate = DateTime.Now,
                            Rating = ZERO,
                            GenreId = 2,
                            UserProfilesId = admin.Id
                        },
                        new Title
                        {
                            TitleName = "Test3",
                            PublicationDate = DateTime.Now,
                            Rating = ZERO,
                            GenreId = 2,
                            UserProfilesId = admin.Id
                        },
                        new Title
                        {
                            TitleName = "Test4",
                            PublicationDate = DateTime.Now,
                            Rating = ZERO,
                            GenreId = 2,
                            UserProfilesId = admin.Id
                        },
                        new Title
                        {
                            TitleName = "Test5",
                            PublicationDate = DateTime.Now,
                            Rating = ZERO,
                            GenreId = 2,
                            UserProfilesId = admin.Id
                        },
                        new Title
                        {
                            TitleName = "Test6",
                            PublicationDate = DateTime.Now,
                            Rating = ZERO,
                            GenreId = 2,
                            UserProfilesId = admin.Id
                        },
                        new Title
                        {
                            TitleName = "Test7",
                            PublicationDate = DateTime.Now,
                            Rating = ZERO,
                            GenreId = 2,
                            UserProfilesId = admin.Id
                        },
                        new Title
                        {
                            TitleName = "Test8",
                            PublicationDate = DateTime.Now,
                            Rating = ZERO,
                            GenreId = 2,
                            UserProfilesId = admin.Id
                        },
                        new Title
                        {
                            TitleName = "Test9",
                            PublicationDate = DateTime.Now,
                            Rating = ZERO,
                            GenreId = 2,
                            UserProfilesId = admin.Id
                        },
                        new Title
                        {
                            TitleName = "Test10",
                            PublicationDate = DateTime.Now,
                            Rating = ZERO,
                            GenreId = 2,
                            UserProfilesId = admin.Id
                        }
                );
                #endregion

                #region Добавляем UserProfile            
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
                #endregion
            }


            #region Добавляем стартовые жанры
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
            #endregion

            #region Добавляем рейтинг
            if (db.RatingTypes.Select(x => x).Count() == ZERO)
            {
                IEnumerable<int> numbers = Enumerable.Range(1, 10).Select(x => x);

                foreach (var item in numbers)
                {
                    db.RatingTypes.AddOrUpdate(
                        new RatingType { RatingNumber = item }
                     );
                }
            }
            #endregion

        }
    }
}
