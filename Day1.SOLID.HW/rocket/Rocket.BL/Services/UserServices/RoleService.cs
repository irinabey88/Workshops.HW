using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Common.Logging;
using Microsoft.AspNet.Identity;
using Rocket.BL.Common.Models.UserRoles;
using Rocket.BL.Common.Services;
using Rocket.DAL.Common.DbModels.Identity;
using Rocket.DAL.Common.UoW;
using Rocket.DAL.Identity;

namespace Rocket.BL.Services.UserServices
{
    public class RoleService : BaseService, IRoleService
    {
        private readonly ILog _logger;
        private readonly RockeRoleManager _roleManager;

        public RoleService(IUnitOfWork unitOfWork, ILog logger, RockeRoleManager roleManager) 
            : base(unitOfWork)
        {
            _logger = logger;
            _roleManager = roleManager;
        }

        public async Task<bool> RoleIsExists(string filter)
        {
            _logger.Trace($"Request RoleIsExists : filter {filter}");
            return await _roleManager.RoleExistsAsync(filter).ConfigureAwait(false);
        }

        public IEnumerable<Role> GetAllRoles()
        {
            _logger.Trace($"Request GetAllRoles");
            return _roleManager.Roles.Include(t => t.Permissions).ToArray().Select(Mapper.Map<Role>);
        }

        public async Task<Role> GetById(string roleId)
        {
            _logger.Trace($"Request GetById : roleId {roleId}");

            var dbRole = await _roleManager.FindByIdAsync(roleId).ConfigureAwait(false);
            return Mapper.Map<Role>(dbRole);

        }

        public async Task<IdentityResult> Insert(Role role)
        {
            var dbRole = Mapper.Map<DbRole>(role);
            dbRole.Id = Guid.NewGuid().ToString();

            _logger.Trace($"Request Insert in queue: Role {dbRole}");
            var result = await _roleManager.CreateAsync(dbRole).ConfigureAwait(false);

            _logger.Trace($"Request Insert complete: Role {dbRole}");
            return result;
        }

        public async Task<IdentityResult> Update(string roleId, string roleName)
        {
            var dbRole = await _roleManager.FindByIdAsync(roleId);
            dbRole.Name = roleName;

            _logger.Trace($"Request Update in queue: Role {dbRole}");
            var result = await _roleManager.UpdateAsync(dbRole).ConfigureAwait(false);

            _logger.Trace($"Request Update complete: Role {dbRole}");
            return result;
        }

        public async Task<IdentityResult> Delete(string roleId)
        {
            var dbRole = await _roleManager.FindByIdAsync(roleId);

            _logger.Trace($"Request Delete in queue: Role {dbRole}");
            var result = await _roleManager.DeleteAsync(dbRole).ConfigureAwait(false);

            _logger.Trace($"Request Delete complete: Role {dbRole}");
            return result;
        }
    }
}