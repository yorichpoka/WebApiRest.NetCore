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
    public class MenusController : ControllerBase
    {
        private readonly IMenuBussiness _Bussiness;
        private readonly IHubContext<MenuHub> _hubContext;

        public MenusController(IMenuBussiness bussiness, IHubContext<MenuHub> hubContext)
        {
            this._Bussiness = bussiness;
            this._hubContext = hubContext;
        }

        // GET api/groupMenus
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

        // GET api/groupMenus/5
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

        // POST api/groupMenus
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] MenuModel groupMenu)
        {
            try
            {
                if (groupMenu == null)
                    throw new Exception("Missing parameter (groupMenu)!");

                var value = await this._Bussiness.Create(groupMenu);

                if (value == null || value.Id == 0)
                    throw new Exception();

                // Emit hub
                await this._hubContext.Clients.All.SendAsync("MenuCreated", value);

                return Created($"groupMenu/{value.Id}", value);
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

        // PUT api/groupMenus/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int? id, [FromBody] MenuModel menu)
        {
            try
            {
                if (menu == null)
                    throw new Exception("Missing parameter (groupMenu)!");

                if (id.Value == 0)
                    throw new Exception("The resource id must not be 0!");

                menu.Id = id.Value;

                await this._Bussiness.Update(menu);

                // Emit hub
                await this._hubContext.Clients.All.SendAsync("MenuUpdated", menu);

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

        // DELETE api/groupMenus/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id.Value == 0)
                    throw new Exception("The resource id must not be 0!");

                await this._Bussiness.Delete(id.Value);

                // Emit hub
                await this._hubContext.Clients.All.SendAsync("MenuDeleted", id.Value);

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
                await this._hubContext.Clients.All.SendAsync("MenusDeleted", id);

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