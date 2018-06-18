using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Rocket.DAL.Common.DbModels.Identity;
using Rocket.DAL.Common.DbModels.ReleaseList;
using Rocket.DAL.Common.DbModels.User;
using Rocket.DAL.Migrations.InitialDataCreators.User;
using Rocket.DAL.Migrations.InitialDataCreators.User.FakeData;
using Rocket.DAL.Migrations.InitialDataCreators.UserRole;

namespace Rocket.DAL.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<Rocket.DAL.Context.RocketContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Rocket.DAL.Context.RocketContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            // Добавление в репозиторий первоначальной тестовой информации о пользователей.
            if (!context.Users.Any())
            {
                var initialUserdatas = new FakeDbUsersCreator().Users;

                initialUserdatas.ForEach(user => context.Users.Add(user));
                context.SaveChanges();
            }

            // Добавление в репозиторий первоначальной информации о половой принадлежности пользователя.
            List<DbGender> initialGenderDatas = new DbGendersCreator().Items.ToList();
            if (!context.DbGenders.Any())
            {
                context.DbGenders.AddRange(initialGenderDatas);
                context.SaveChanges();
            }

            // Добавление в репозиторий первоначальной информации о статусах аккаунта пользователей.
            List<DbAccountStatus> initialAccountStatusDatas = new DbAccountStatusesCreator().Items;
            if (!context.DbAccountStatuses.Any())
            {
                context.DbAccountStatuses.AddRange(initialAccountStatusDatas);
                context.SaveChanges();
            }

            CreateFakeUser(context);

            // Добавление в репозиторий первоначальной информации о странах мира (всего 251 наименование стран взято из международного
            // классификатора ISO).
            List<DbCountry> initialCountryDatas = new DbCountriesCreator().Items;
            if (!context.DbCountries.Any())
            {
                context.DbCountries.AddRange(initialCountryDatas);
                context.SaveChanges();
            }

            // Добавление в репозиторий первоначальной информации об основных языках мира (всего 13 основных мировых разговорных языков).
            List<DbLanguage> initialLanguageDatas = new DbLanguagesCreator().Items;
            if (!context.DbLanguages.Any())
            {
                context.DbLanguages.AddRange(initialLanguageDatas);
                context.SaveChanges();
            }

            // Добавление в репозиторий первоначальной информации о том, как обращаться к пользователю (Mr. и Ms.).
            List<DbHowToCall> initialHowToCallDatas = new DbHowToCallCreator().Items;
            if (!context.DbHowToCalls.Any())
            {
                context.DbHowToCalls.AddRange(initialHowToCallDatas);
                context.SaveChanges();
            }

            // Добавление в репозиторий первоначальной информации о ролях пользователей.
            List<DbRole> initialRolesDatas = new DbUserRolesCreator().Items;
        }

        [Conditional("DEBUG")]
        private void CreateFakeUser(Rocket.DAL.Context.RocketContext context)
        {
            var initialUserdatas = new FakeDbUsersCreator().Users;

            if (!context.Users.Any())
            {
                initialUserdatas.ForEach(user => context.Users.Add(user));
            context.SaveChanges();
            }

            if (!context.Roles.Any())
            {
                List<DbRole> initialRolesDatas = new FakeDbRolesCreator().Roles;
                initialRolesDatas.ForEach(role => context.Roles.Add(role));
            }
        }
    }
}

