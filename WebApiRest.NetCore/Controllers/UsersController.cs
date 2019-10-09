using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;
using WebApiRest.NetCore.Domain.Interfaces.Bussiness;
using WebApiRest.NetCore.Domain.Models;
using WebApiRest.NetCore.Filters;
using WebApiRest.NetCore.Tools;

namespace WebApiRest.NetCore.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(CustomActionFilter))]
    public class UsersController : ControllerBase
    {
        private readonly IUserBussiness _Bussiness;
        private readonly IRoleBussiness _BussinessRole;
        private readonly IConfiguration _Configuration;

        public UsersController(IUserBussiness dao, IRoleBussiness daoRole, IConfiguration confirugation)
        {
            this._Bussiness = dao;
            this._BussinessRole = daoRole;
            this._Configuration = confirugation;
        }

        // GET api/users
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var values = await this._Bussiness.ReadWithRoles();

                if (values == null)
                    throw new Exception();

                return Ok(values);
            }
            catch (Exception ex)
            {
                switch (ex.InnerException)
                {
                    case Exception exception:
                        return BadRequest(exception.Message);
                    // Not InnerException
                    default:
                        return NoContent();
                }
            }
        }

        // GET api/users/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var values = await this._Bussiness.Read(id);

                if (values == null)
                    throw new Exception();

                return Ok(values);
            }
            catch (Exception ex)
            {
                switch (ex.InnerException)
                {
                    case Exception exception:
                        return BadRequest(exception.Message);
                    // Not InnerException
                    default:
                        return NoContent();
                }
            }
        }

        // POST api/users
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserModel user)
        {
            try
            {
                if (user == null)
                    throw new Exception("Missing parameter (user)!");

                var value = await this._Bussiness.Create(user);

                if (value == null || value.Id == 0)
                    throw new Exception();

                return Created($"user/{value.Id}", value);
            }
            catch (Exception ex)
            {
                switch (ex.InnerException)
                {
                    case Exception exception:
                        return BadRequest(exception.Message);
                    // Not InnerException
                    default:
                        return NoContent();
                }
            }
        }

        // POST api/users
        [AllowAnonymous]
        [HttpPost("auth")]
        public async Task<IActionResult> Auth([FromBody] UserModel user)
        {
            try
            {
                if (user == null)
                    throw new Exception("Missing parameter (user)!");

                var userModel = await this._Bussiness.Read(user.Login, user.Password);

                if (userModel == null || userModel.Id == 0)
                    throw new Exception("Login or password was incorrect.");

                var roleModel = await this._BussinessRole.Read(userModel.IdRole);

                var token = Methods.GetJWT(
                              this._Configuration.GetSection("AppSettings:SecurityKey").Value,
                              new TimeSpan(
                                  0, 
                                  0, 
                                  this._Configuration.GetValue<int>("AppSettings:DurationTokenJwt"), 
                                  0
                              )
                            );

                // Set id user.
                token.User = new UserRoleModel
                {
                    Id = userModel.Id,
                    IdRole = roleModel.Id,
                    Login = userModel.Login,
                    Password = userModel.Password,
                    Name = userModel.Name,
                    Role = roleModel
                };

                return StatusCode(202, token);
            }
            catch (Exception ex)
            {
                switch (ex.InnerException)
                {
                    case Exception exception:
                        return BadRequest(exception.Message);
                    // Not InnerException
                    default:
                        return Unauthorized(ex.Message);
                }
            }
        }

        // PUT api/users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int? id, [FromBody] UserModel user)
        {
            try
            {
                if (user == null)
                    throw new Exception("Missing parameter (user)!");

                if (id.Value == 0)
                    throw new Exception("The resource id must not be 0!");

                user.Id = id.Value;

                await this._Bussiness.Update(user);

                return Accepted();
            }
            catch (Exception ex)
            {
                switch (ex.InnerException)
                {
                    case Exception exception:
                        return BadRequest(exception.Message);
                    // Not InnerException
                    default:
                        return NoContent();
                }
            }
        }

        // DELETE api/users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id.Value == 0)
                    throw new Exception("The resource id must not be 0!");

                await this._Bussiness.Delete(id.Value);

                return Ok();
            }
            catch (Exception ex)
            {
                switch (ex.InnerException)
                {
                    case Exception exception:
                        return BadRequest(exception.Message);
                    // Not InnerException
                    default:
                        return NoContent();
                }
            }
        }
    }
}