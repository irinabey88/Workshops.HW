using System;
using System.Collections.Generic;
using Rocket.BL.Common.Models.UserRoles;

namespace Rocket.BL.Common.Services
{
    public interface IPermissionService : IDisposable
    {
        /// <summary>
        /// Добавляет пермишен
        /// </summary>
        /// <param name="permission">Пермишен</param>
        /// <param name="user"></param>
        void Insert(Permission permission, string user);

        /// <summary>
        /// Удаляет пермишен
        /// </summary>
        /// <param name="id">Идентификатор пермишена</param>
        /// <param name="user"></param>
        void Delete(Permission permission, string user);

        /// <summary>
        /// Возвращает пермишены роли, нужно для UI
        /// </summary>
        /// <param name="idUser">Идентификатор пользователя</param>
        /// <returns>Коллекцию Permission</returns>
        IEnumerable<Permission> GetPermissionByUser(string idUser);


        /// <summary>
        /// Возвращает пермишены роли, нужно для UI
        /// </summary>
        /// <returns>Коллекцию Permission</returns>
        IEnumerable<Permission> GetAllPermissions();
    }
}