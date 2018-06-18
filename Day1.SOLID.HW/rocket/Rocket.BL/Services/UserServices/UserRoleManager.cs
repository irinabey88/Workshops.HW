using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Logging;
using Microsoft.AspNet.Identity;
using Rocket.BL.Common.Services;
using Rocket.DAL.Common.UoW;
using Rocket.DAL.Identity;

namespace Rocket.BL.Services.UserServices
{
    public class UserRoleManager : BaseService, IUserRoleManager
    {
        private const string DefaultRoleId = "test"; // todo MP закинуть в хранилище дефолтроль когда будет конкретная база с guid
        private readonly ILog _logger;
        private readonly RocketUserManager _userManager;

        public UserRoleManager(IUnitOfWork unitOfWork, ILog logger, RocketUserManager userManager) : base(unitOfWork)
        {
            _logger = logger;
            _userManager = userManager;
        }

        /// <summary>
        /// add user to role
        /// </summary>
        /// <param name="userId"> Идентификатор пользователя. </param>
        /// <param name="role"> Идентификатор роли. </param>
        /// <returns> Task </returns>
        public async Task<IdentityResult> AddToRole(string userId, string role = DefaultRoleId)
        {
            _logger.Trace($"Request AddRole in queue: Role {role}, user {userId}");
            var result = await _userManager.AddToRoleAsync(userId, role).ConfigureAwait(false);
            
            _logger.Trace($"Request AddRole complete: user {userId} added to role {role}");
            return result;
        }

        /// <summary>
        /// Удалить роль у юзера
        /// </summary>
        /// <param name="userId"> Идентификатор пользователя. </param>
        /// <param name="roleId"> Идентификатор роли. </param>
        /// <returns> bool </returns>
        public async Task<IdentityResult> RemoveFromRole(string userId, string roleId)
        {
            _logger.Trace($"Request RemoveFromRole in queue: Role {roleId}, user {userId}");
            var result = await _userManager.RemoveFromRoleAsync(userId, roleId).ConfigureAwait(false);

            _logger.Trace($"Request RemoveFromRole complete: {roleId} removed from {userId}");
            return result;
        }

        /// <summary>
        /// Returns the roles for the user
        /// </summary>
        /// <param name="userId"> Идентификатор пользователя. </param>
        /// <returns>Список ролей</returns>
        public virtual async Task<IEnumerable<string>> GetRoles(string userId)
        {
            _logger.Trace($"Request GetRoles : user {userId}");
            return await _userManager.GetRolesAsync(userId).ConfigureAwait(false);
        }

        /// <summary>
        /// Returns true if the user is in the specified role
        /// </summary>
        /// <param name="userId"> Идентификатор пользователя. </param>
        /// <param name="roleId"> Идентификатор роли. </param>
        /// <returns>bool</returns>
        public virtual async Task<bool> IsInRole(string userId, string roleId)
        {
            _logger.Trace($"Request IsInRole : user {userId}");
            return await _userManager.IsInRoleAsync(userId, roleId).ConfigureAwait(false);
        }
    }
}