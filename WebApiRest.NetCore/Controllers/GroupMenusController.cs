using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WebApiRest.NetCore.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using WebApiRest.NetCore.Domain.Models;
using WebApiRest.NetCore.Filters;

namespace WebApiRest.NetCore.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(CustomActionFilter))]
    public class GroupMenusController : ControllerBase
    {
        private readonly IGroupMenuDao _Dao;

        public GroupMenusController(IGroupMenuDao dao)
        {
            this._Dao = dao;
        }

        // GET api/groupMenus
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var values = await this._Dao.Read();

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

        // GET api/groupMenus/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var values = await this._Dao.Read(id);

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

        // POST api/groupMenus
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] GroupMenuModel groupMenu)
        {
            try
            {
                if (groupMenu == null)
                    throw new Exception("Missing parameter (groupMenu)!");

                var value = await this._Dao.Create(groupMenu);

                if (value == null || value.Id == 0)
                    throw new Exception();

                return Created($"groupMenu/{value.Id}", value);
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

        // PUT api/groupMenus/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int? id, [FromBody] GroupMenuModel groupMenu)
        {
            try
            {
                if (groupMenu == null)
                    throw new Exception("Missing parameter (groupMenu)!");

                if (id.Value == 0)
                    throw new Exception("The resource id must not be 0!");

                groupMenu.Id = id.Value;

                await this._Dao.Update(groupMenu);

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

        // DELETE api/groupMenus/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id.Value == 0)
                    throw new Exception("The resource id must not be 0!");

                await this._Dao.Delete(id.Value);

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