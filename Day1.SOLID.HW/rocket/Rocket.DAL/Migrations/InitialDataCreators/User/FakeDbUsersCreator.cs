using System.Collections.Generic;
using System.Collections.ObjectModel;
using Rocket.DAL.Common.DbModels.Identity;
using Rocket.DAL.Common.DbModels.User;
using Microsoft.AspNet.Identity.EntityFramework;
using Rocket.DAL.Common.DbModels.DbPersonalArea;

namespace Rocket.DAL.Migrations.InitialDataCreators.User
{
    /// <summary>
    /// Представляет набор сгенерированных данных о пользователях,
    /// в моделях хранинения данных.
    /// </summary>
    public class FakeDbUsersCreator
    {
        /// <summary>
        /// Создает новый экземпляр сгенерированных данных о пользователях.
        /// </summary>

        /// <summary>
        /// Возвращает коллекцию сгенерированных пользователей.
        /// </summary>
        public List<DbUser> Users => new List<DbUser>()
        {
            new DbUser()
            {
                EmailConfirmed = true,
                Email = "adminuser@gmail.com",
                PhoneNumber = "+375221133654",
                TwoFactorEnabled = false,
                LockoutEnabled = false,
                AccessFailedCount = 0,
                UserName = "adminUser",
                FirstName = "Иван",
                LastName = "Иванов",

                DbUserProfile = new DbUserProfile()
                {
                    Email = new Collection<DbEmail>()
                    {
                        new DbEmail()
                        {
                            Name = "secondmail@gmail.com",
                        }
                    },
                },
            },
            new DbUser()
            {
                EmailConfirmed = true,
                Email = "userfirst@gmail.com",
                PhoneNumber = "+375221159654",
                TwoFactorEnabled = false,
                LockoutEnabled = false,
                AccessFailedCount = 0,
                UserName = "firstUser",
                FirstName = "Петр",
                LastName = "Петров",

                DbUserProfile = new DbUserProfile()
                {
                    Email = new Collection<DbEmail>()
                    {
                        new DbEmail()
                        {
                            Name = "lastemail@gmail.com",
                        }
                    },
                },
            },
            new DbUser()
            {
                EmailConfirmed = true,
                Email = "second@gmail.com",
                PhoneNumber = "+375221975854",
                TwoFactorEnabled = false,
                LockoutEnabled = false,
                AccessFailedCount = 0,
                UserName = "secondUser",
                FirstName = "Кирил",
                LastName = "Булатов",


                DbUserProfile = new DbUserProfile()
                {
                    Email = new Collection<DbEmail>()
                    {
                        new DbEmail()
                        {
                            Name = "kiril@gmail.com",
                        }
                    },
                },
            },
        };
    }
}