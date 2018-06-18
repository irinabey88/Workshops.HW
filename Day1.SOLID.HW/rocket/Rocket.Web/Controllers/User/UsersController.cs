using AutoMapper;
using Rocket.BL.Common.Services.User;
using Rocket.DAL.Common.DbModels.DbPersonalArea;
using Rocket.DAL.Common.DbModels.User;
using Rocket.DAL.Identity;
using Rocket.Web.Attributes;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;

namespace Rocket.Web.Controllers.User
{
    /// <summary>
    /// Контроллер WebApi работы с пользователями.
    /// </summary>
    [RoutePrefix("users")]
    public class UsersController : ApiController
    {
        private readonly IUserManagementService _userManagementService;

        public UsersController(IUserManagementService userNativeManagementService)
        {
            _userManagementService = userNativeManagementService ?? throw new ArgumentNullException(nameof(userNativeManagementService));
        }

        /// <summary>
        /// Возвращает всех пользователей.
        /// </summary>
        /// <returns>Все пользователи хранилища данных.</returns>
        [HttpGet]
        [Route("all")]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        [SwaggerResponse(HttpStatusCode.OK, "List of users", typeof(ICollection<BL.Common.Models.User.User>))]
        public IHttpActionResult GetAllUsers()
        {
            var users = _userManagementService.GetAllUsers();


            if (users == null)
            {
                return NotFound();
            }

            return Ok(users);
        }

        /// <summary>
        /// Возвращает пользователей для пейджинга.
        /// </summary>
        /// <param name="pageSize">Количество сведений о пользователях, выводимых на страницу.</param>
        /// <param name="pageNumber">Номер выводимой страницы со сведениями о пользователях.</param>
        /// <returns>Коллекция пользователей для пейджинга.</returns>
        [HttpGet]
        [Route("new/page_{pageNumber:int:min(1)}")]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        [SwaggerResponse(HttpStatusCode.OK, "List of users", typeof(ICollection<BL.Common.Models.User.User>))]
        public IHttpActionResult GetUsersPage(int pageNumber, int pageSize = 5)
        {
            var users = _userManagementService.GetUsersPage(pageSize, pageNumber);

            if (users == null)
            {
                return NotFound();
            }

            return Ok(users);
        }

        /// <summary>
        /// Возвращает пользователя с конкретным
        /// уникальным идентификатором.
        /// </summary>
        /// <param name="id">Уникальный идентификатор пользователя.</param>
        /// <returns>Пользователь хранилища.</returns>
        [HttpGet]
        [Route("{id}")]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        [SwaggerResponse(HttpStatusCode.OK, "User", typeof(BL.Common.Models.User.User))]
        public async Task<IHttpActionResult> GetUserById(string id)
        {
            var user = await _userManagementService.GetUser(id);

            return user == null ? (IHttpActionResult)NotFound() : Ok(user);
        }

        /// <summary>
        /// Регистрирует нового пользователя.
        /// </summary>
        /// <param name="user">Данные экземпляра пользователя.</param>
        /// <returns>Пользователь хранилища.</returns>
        [HttpPost]
        [Route("add")]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Model is not valid", typeof(string))]
        [SwaggerResponse(HttpStatusCode.OK, "New model description", typeof(BL.Common.Models.User.User))]
        public async Task<IHttpActionResult> AddUser([FromBody] BL.Common.Models.User.User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Model is not valid");
            }

            await _userManagementService.AddUser(user);               

            return Ok(user);            
        }

        /// <summary>
        /// Обновляет пользователя.
        /// </summary>
        /// <param name="user">Пользователь для обновления.</param>
        /// <returns>Сведения об обновлении пользователя.</returns>
        [HttpPut]
        [Route("update")]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Model is not valid", typeof(string))]
        [SwaggerResponse(HttpStatusCode.NoContent)]
        public IHttpActionResult UpdateUser([FromBody]BL.Common.Models.User.User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _userManagementService.UpdateUser(user);

            return new StatusCodeResult(HttpStatusCode.NoContent, Request);
        }

        /// <summary>
        /// Удаление пользователя с конкретным уникальным
        /// идентификатором.
        /// </summary>
        /// <param name="id">Уникальный идентификатор пользователя.</param>
        /// <returns>Сведения об удалении.</returns>
        [HttpDelete]
        [Route("{id}")]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.BadRequest, "User id invalid", typeof(string))]
        [SwaggerResponse(HttpStatusCode.OK)]
        public IHttpActionResult DeleteUserById(string id)
        {
            var usersCount = _userManagementService.GetAllUsers().Count;

            if (Convert.ToInt32(id) > usersCount)
            {
                return BadRequest("User id invalid");
            }

            _userManagementService.DeleteUser(id);

            return Ok($"User with id = {id} successfully deleted");
        }

        /// <summary>
        /// Удаление пользователя по его модели.
        /// </summary>
        /// <param name="user">Экземпляр пользователя.</param>
        /// <returns>Сведения об удалении.</returns>
        [HttpDelete]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.BadRequest, "User id invalid", typeof(string))]
        [SwaggerResponse(HttpStatusCode.OK)]
        public IHttpActionResult DeleteUserByModel([FromBody]BL.Common.Models.User.User user)
        {
            var usersLogin = user.Login;

            if (!_userManagementService.UserExists(f => f.Login == usersLogin))
            {
                return BadRequest("User invalid");
            }

            _userManagementService.DeleteUser(user.Id);

            return Ok($"User with id = {user.Id} successfully deleted");
        }
    }
}
