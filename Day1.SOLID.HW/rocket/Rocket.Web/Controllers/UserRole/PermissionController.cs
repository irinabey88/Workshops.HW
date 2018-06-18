using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using Rocket.BL.Common.Models.User;
using Rocket.BL.Common.Models.UserRoles;
using Rocket.BL.Common.Services;
using Swashbuckle.Swagger.Annotations;

namespace Rocket.Web.Controllers.UserRole
{
    [RoutePrefix("permission")]
    public class PermissionController : ApiController
    {
        private readonly IPermissionService _permissionService;

        public PermissionController(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }
        
        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult GetPermissionById(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return BadRequest("Incorrect id value");
            }

            var model = _permissionService.GetPermissionByUser(id);

            return model == null ? (IHttpActionResult)NotFound() : Ok(model);
        }

        [HttpGet]
        [Route("GetPermissionByRole{id}")]
        public IHttpActionResult GetPermissionByRole(string user)
        {
            if (string.IsNullOrWhiteSpace(user))
            {
                return BadRequest("Incorrect user value");
            }

            var model = _permissionService.GetPermissionByUser(user);
            return model == null ? (IHttpActionResult)NotFound() : Ok(model);
        }

        [HttpGet]
        [Route("all")]
        public IHttpActionResult GetAllPermissions()
        {
            var model = _permissionService.GetAllPermissions();
            return model == null ? (IHttpActionResult)NotFound() : Ok(model);
        }

        [HttpPost]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Data is not valid", typeof(string))]
        [SwaggerResponse(HttpStatusCode.Created, "New Permission description", typeof(Permission))]
        public IHttpActionResult InsertPermission(Permission permission, string user)
        {
            if (permission == null || string.IsNullOrWhiteSpace(user))
            {
                return BadRequest("Model cannot be empty");
            }

            _permissionService.Insert(permission, user);
            return Created($"permission/{permission.PermissionId}", permission);
        }       

        [HttpDelete]
        [Route("{id:int:min(1)}")]
        public IHttpActionResult DeletePermissionById(Permission permission, string user)
        {
            if (permission == null || string.IsNullOrWhiteSpace(user))
            {
                return BadRequest("Incorrect data value");
            }

            _permissionService.Delete(permission, user);
            return new StatusCodeResult(HttpStatusCode.Accepted, Request);
        }
    }
}