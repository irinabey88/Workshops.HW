using System.Collections.Generic;
using Rocket.DAL.Common.DbModels.Identity;

namespace Rocket.DAL.Migrations.InitialDataCreators.User.FakeData
{
    public class FakeDbRolesCreator
    {
        /// <summary>
        /// Создает новый экземпляр сгенерированных данных о ролях.
        /// </summary>

        /// <summary>
        /// Возвращает коллекцию сгенерированных ролей.
        /// </summary>
        public List<DbRole> Roles => new List<DbRole>()
        {
            new DbRole() {Name = "administrator"},
            new DbRole() {Name = "user"}
        };
    }
}