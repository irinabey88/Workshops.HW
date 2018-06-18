using System;
using System.Net;
using System.Web.Http;
using Rocket.BL.Common.Services;
using Swashbuckle.Swagger.Annotations;

namespace Rocket.Web.Controllers
{
    public class LoggerController : ApiController
    {
        private readonly ILogService _logService;

        public LoggerController(ILogService logService)
        {
            _logService = logService ?? throw new ArgumentNullException(nameof(logService));
        }

        [HttpGet]
        [Route("all")]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.OK)]
        public IHttpActionResult GetAllRoles()
        {
            var result = _logService.GetLogInfo();
            return Ok(result);
        }
    }
}
