using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
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
    public class RolesController : ControllerBase
    {
        private readonly IRoleBussiness _Bussiness;
        private readonly IHubContext<RoleHub> _hubContext;

        public RolesController(IRoleBussiness business, IHubContext<RoleHub> hubContext)
        {
            this._Bussiness = business;
            this._hubContext = hubContext;
        }

        // GET api/roles
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

        // GET api/roles/5
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

        // POST api/roles
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RoleModel role)
        {
            try
            {
                if (role == null)
                    throw new Exception("Missing parameter (role)!");

                var value = await this._Bussiness.Create(role);

                if (value == null || value.Id == 0)
                    throw new Exception();

                // Emit hub
                await this._hubContext.Clients.All.SendAsync("RoleCreated", value);

                return Created($"role/{value.Id}", value);
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

        // PUT api/roles/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int? id, [FromBody] RoleModel role)
        {
            try
            {
                if (role == null)
                    throw new Exception("Missing parameter (role)!");

                if (id.Value == 0)
                    throw new Exception("The resource id must not be 0!");

                role.Id = id.Value;

                await this._Bussiness.Update(role);

                // Emit hub
                await this._hubContext.Clients.All.SendAsync("RoleUpdated", role);

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

        // DELETE api/roles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id.Value == 0)
                    throw new Exception("The resource id must not be 0!");

                await this._Bussiness.Delete(id.Value);

                // Emit hub
                await this._hubContext.Clients.All.SendAsync("RoleDeleted", id);

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
        public async Task<IActionResult> Delete([FromQuery(Name = "id")]int[] id)
        {
            try
            {
                if (id.Length == 0)
                    throw new Exception("The array of id must not be 0!");

                await this._Bussiness.Delete(id);

                // Emit hub
                await this._hubContext.Clients.All.SendAsync("RolesDeleted", id);

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