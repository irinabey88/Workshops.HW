using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Common.Logging;
using Microsoft.AspNet.Identity;
using Rocket.BL.Common.Models.UserRoles;
using Rocket.BL.Common.Services;
using Rocket.DAL.Common.UoW;
using Rocket.DAL.Identity;

namespace Rocket.BL.Services.UserServices
{
    public static class Permissions
    {
        public static string Read => "read.news";
    }

    /// <summary>
    /// Добавление/удаление пермишенов у ролей + логирование
    /// </summary>
    public class PermissionService : BaseService , IPermissionService
    {
        private readonly RocketUserManager _userManager;
        private readonly RockeRoleManager _roleManager;
        private readonly ILog _logger;
        private readonly string _claimName = "permission";

        /// <summary>
        /// Создает новый экземпляр <see cref="PermissionService"/>
        /// с заданным unit of work
        /// </summary>
        /// <param name="unitOfWork">Экземпляр unit of work</param>
        /// <param name="userManager"></param>
        /// <param name="roleManager"></param>
        public PermissionService(IUnitOfWork unitOfWork,RocketUserManager userManager,
            RockeRoleManager roleManager, ILog logger): base(unitOfWork)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
        }

        /// <summary>
        /// Добавляет пермишен
        /// </summary>
        /// <param name="permission">Пермишен</param>
        /// <param name="user"></param>
        public void Insert(Permission permission, string user)
        {
            var permValueName = new Claim("ValueName", permission.ValueName);
            var permDescription = new Claim("Description", permission.Description);
            
            _userManager.AddClaim(user, permValueName);
            _userManager.AddClaim(user, permDescription);
            _logger.Debug($"Permission {permission.Description} added in DB");
        }
     

        /// <summary>
        /// Удаляет пермишен
        /// </summary>
        /// <param name="id">Идентификатор пермишена</param>
        public void Delete(Permission permission, string user)
        {
            var userId = _userManager.FindById(user);

            var claims = _userManager.GetClaimsAsync(userId.ToString());

            var permis = (Claim)claims.Result.FirstOrDefault(a => (a.Type == "ValueName") && (a.Value == permission.ValueName));

            if (permis == null)
            {
                return;
            }
            else
            {
                _userManager.RemoveClaim(user, permis);
            }
        }

        /// <summary>
        /// Возвращает пермишены роли, нужно для UI
        /// </summary>
        /// <returns>Коллекцию Permission</returns>
        public IEnumerable<Permission> GetAllPermissions()
        {
            var resault = (IEnumerable<Permission>)_userManager.Users.SelectMany(u => u.Claims).ToList();

            return resault;
        }

        /// <summary>
        /// Возвращает пермишены роли, нужно для UI
        /// </summary>
        /// <param name="idUser">Идентификатор пользователя</param>
        /// <returns>Коллекцию Permission</returns>
        public IEnumerable<Permission> GetPermissionByUser(string idUser)
        {
            var userId = _userManager.FindById(idUser);

            var claims = _userManager.GetClaimsAsync(userId.ToString());

            var permis = (IEnumerable<Permission>)claims.Result;

            return permis;            
        }
    }
}