using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiRest.NetCore.Api.Filters;
using WebApiRest.NetCore.Domain.Interfaces.Bussiness;
using WebApiRest.NetCore.Domain.Models;
using WebApiRest.NetCore.Domain.Hubs;
using Microsoft.AspNetCore.SignalR;
using Serilog;
using WebApiRest.NetCore.Domain;

namespace WebApiRest.NetCore.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(CustomActionFilter))]
    public class AuthorizationsController : ControllerBase
    {
        private readonly IAuthorizationBussiness _Bussiness;
        private readonly IHubContext<AuthorizationHub> _hubContext;

        public AuthorizationsController(IAuthorizationBussiness bussiness, IHubContext<AuthorizationHub> hubContext)
        {
            this._Bussiness = bussiness;
            this._hubContext = hubContext;
        }

        // GET api/authorizations
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var values = await this._Bussiness.Read();

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

        // GET api/authorizations/5
        [HttpGet("{idRole}/{idMenu}")]
        public async Task<IActionResult> Get(int idRole, int idMenu)
        {
            try
            {
                var values = await this._Bussiness.Read(idRole, idMenu);

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

        // POST api/authorizations
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AuthorizationModel authorization)
        {
            try
            {
                if (authorization == null)
                    throw new Exception("Missing parameter (authorization)!");

                var value = await this._Bussiness.Create(authorization);

                // Emit hub
                await this._hubContext.Clients.All.SendAsync("AuthorizationCreated", value);

                return Created($"authorization/{value.IdMenu}/{value.IdRole}", value);
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

        // PUT api/authorizations/5
        [HttpPut("{idRole}/{idMenu}")]
        public async Task<IActionResult> Put(int? idRole, int? idMenu, [FromBody] AuthorizationModel authorization)
        {
            try
            {
                if (authorization == null)
                    throw new Exception("Missing parameter (authorization)!");

                if (idRole.Value == 0)
                    throw new Exception("The resource idRole must not be 0!");

                if (idMenu.Value == 0)
                    throw new Exception("The resource idMenu must not be 0!");

                authorization.IdRole = idRole.Value;
                authorization.IdMenu = idMenu.Value;

                await this._Bussiness.Update(authorization);

                // Emit hub
                await this._hubContext.Clients.All.SendAsync("AuthorizationUpdated", authorization);

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

        // DELETE api/authorizations/5
        [HttpDelete("{idRole}/{idMenu}")]
        public async Task<IActionResult> Delete(int? idRole, int? idMenu)
        {
            try
            {
                if (idRole.Value == 0)
                    throw new Exception("The resource idRole must not be 0!");

                if (idMenu.Value == 0)
                    throw new Exception("The resource idMenu must not be 0!");

                await this._Bussiness.Delete(idRole.Value, idMenu.Value);

                // Emit hub
                await this._hubContext.Clients.All.SendAsync("AuthorizationDeleted", new { idRole = idRole.Value, idMenu = idMenu.Value });

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
                        return NoContent();
                }
            }
        }

        // DELETE api/users/5
        [HttpDelete("Array")]
        public async Task<IActionResult> Delete([FromQuery(Name = "id")]string[] id)
        {
            try
            {
                if (id.Length == 0)
                    throw new Exception("The array of id must not be 0!");

                // Get ids array from parameter
                var ids = new List<int[]>();

                // Convert to array of arry
                id.Select(l => l.Split('-'))
                  .ToList()
                  .ForEach(l =>
                  {
                      ids.Add(
                          new[] { int.Parse(l[0]), int.Parse(l[1]) }
                      );
                  }
                   );

                await this._Bussiness.Delete(ids.ToArray());
                
                // Emit hub
                await this._hubContext.Clients.All.SendAsync("AuthorizationsDeleted", ids.Select(l => new { idRole = l[0], idMenu = l[1] }));

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
                        return NoContent();
                }
            }
        }
    }
}