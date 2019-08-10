using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WebApiRest.NetCore.Domain.Interfaces.Bussiness;
using WebApiRest.NetCore.Domain.Models;
using WebApiRest.NetCore.Filters;

namespace WebApiRest.NetCore.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(CustomActionFilter))]
    public class RolesController : ControllerBase
    {
        private readonly IRoleBussiness _Dao;

        public RolesController(IRoleBussiness dao)
        {
            this._Dao = dao;
        }

        // GET api/roles
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

        // GET api/roles/5
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

        // POST api/roles
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RoleModel role)
        {
            try
            {
                if (role == null)
                    throw new Exception("Missing parameter (role)!");

                var value = await this._Dao.Create(role);

                if (value == null || value.Id == 0)
                    throw new Exception();

                return Created($"role/{value.Id}", value);
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

                await this._Dao.Update(role);

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

        // DELETE api/roles/5
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