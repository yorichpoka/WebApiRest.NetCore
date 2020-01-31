using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebApiRest.NetCore.Api.Filters;
using WebApiRest.NetCore.Domain.Enums;
using WebApiRest.NetCore.Domain.Hubs;
using WebApiRest.NetCore.Domain.Interfaces.Bussiness;
using WebApiRest.NetCore.Domain.Models;
using WebApiRest.NetCore.Domain;
using Microsoft.AspNetCore.Authentication;

namespace WebApiRest.NetCore.Api.Controllers
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
        private readonly IHubContext<UserHub> _hubContext;

        public UsersController(IUserBussiness dao, IRoleBussiness daoRole, IConfiguration confirugation, IHubContext<UserHub> hubContext)
        {
            this._Bussiness = dao;
            this._BussinessRole = daoRole;
            this._Configuration = confirugation;
            this._hubContext = hubContext;
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
                // Log
                Log.Error(ex.ExtToString());

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
                // Log
                Log.Error(ex.ExtToString());

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

                // Emit hub
                await this._hubContext.Clients.All.SendAsync(
                    HubClientMethods.UserCreated.ToString(), 
                    value
                );

                return Created($"user/{value.Id}", value);
            }
            catch (Exception ex)
            {
                // Log
                Log.Error(ex.ExtToString());
                
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

                // Verify user connected
                this._Bussiness.InitHubConnectionId(userModel.Id);

                var roleModel = await this._BussinessRole.Read(userModel.IdRole ?? 0);

                var token = this._Configuration.GetSection("AppSettings:SecurityKey").Value.ExtGetJwt(
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
                // Log
                Log.Error(ex.ExtToString());
                
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

        // POST api/users
        [AllowAnonymous]
        [HttpGet("login")]
        public ActionResult LoginByIdentityServer4()
        {
            return
                Challenge(
                    new AuthenticationProperties() {
                        RedirectUri = "api/users",
                    },
                    Microsoft.AspNetCore.Authentication.OpenIdConnect.OpenIdConnectDefaults.AuthenticationScheme
                );
        }

        // POST api/users
        [AllowAnonymous]
        [HttpGet("logout")]
        public async Task<ActionResult> LogoutByIdentityServer4()
        {
            var idToken = await Microsoft.AspNetCore.Authentication.AuthenticationHttpContextExtensions.GetTokenAsync(HttpContext, "id_token");
            var access_token = await Microsoft.AspNetCore.Authentication.AuthenticationHttpContextExtensions.GetTokenAsync(HttpContext, "access_token");
            var claims = HttpContext.User.Claims.Select(l => new {l.Type, l.Value})
                                                .ToArray();

            return
                Ok(
                    new {
                        idToken,
                        access_token,
                        claims
                    }
                );
        }

        // POST api/users
        [AllowAnonymous]
        [HttpDelete("signOut/{id}")]
        public async Task<IActionResult> SignOut(int id)
        {
            try
            {
                // get hubConnectionId of user connected
                var hubConnectionId = this._Bussiness.GetHubConnectionId(id);
                // Verify user connected
                bool state = await this._Bussiness.RemoveHubConnectionId(null, id);
                // Create object result
                var result =    new { 
                                    isExecuted = state, 
                                    message = state ? "User disconnected."
                                                    : "User not connected"
                                };
                // Emit hub to userd connected
                if (!string.IsNullOrEmpty(hubConnectionId))
                    await this._hubContext.Clients.Client(hubConnectionId)
                                                  .SendAsync(
                                                      HubClientMethods.UserDisconnected.ToString(), 
                                                      result
                                                    );

                return  Ok(result);
            }
            catch (Exception ex)
            {
                // Log
                Log.Error(ex.ExtToString());
                
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

                // Update id value
                user.Id = id.Value;

                // Get user updated
                user = await this._Bussiness.Update(user);

                // Get role object of user updated
                var roleModel = new RoleModel(); 
                
                // Check if user connected
                if (this._Bussiness.IsUserConnected(id.Value))
                    roleModel = await this._BussinessRole.Read(user.IdRole ?? 0);

                // Emit hub
                await this._hubContext.Clients.All.SendAsync(
                    HubClientMethods.UserUpdated.ToString(), 
                    new UserRoleModel {
                        Id = user.Id,
                        IdRole = user.Id,
                        Login = user.Login,
                        Password = user.Password,
                        Name = user.Name,
                        Role = roleModel
                    }
                );

                return Accepted();
            }
            catch (Exception ex)
            {
                // Log
                Log.Error(ex.ExtToString());
                
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

                // Check if user connected
                if (this._Bussiness.IsUserConnected(id.Value))
                    throw new Exception("Unable to delete an user connected!");

                await this._Bussiness.Delete(id.Value);

                // Emit hub
                await this._hubContext.Clients.All.SendAsync(
                    HubClientMethods.UserDeleted.ToString(), 
                    id
                );

                return Ok();
            }
            catch (Exception ex)
            {
                // Log
                Log.Error(ex.ExtToString());
                
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

        // DELETE api/users/5
        [HttpDelete("Array")]
        public async Task<IActionResult> Delete([FromQuery(Name = "id")]int[] id)
        {
            try
            {
                if (id.Length == 0)
                    throw new Exception("The array of id must not be 0!");

                // Check if user connected
                if (this._Bussiness.IsUserConnected(id))
                    throw new Exception("Unable to delete an user connected!");

                await this._Bussiness.Delete(id);

                // Emit hub
                await this._hubContext.Clients.All.SendAsync(
                    HubClientMethods.UsersDeleted.ToString(), 
                    id
                );

                return Ok();
            }
            catch (Exception ex)
            {
                // Log
                Log.Error(ex.ExtToString());
                
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
    }
}