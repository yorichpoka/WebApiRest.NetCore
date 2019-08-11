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
    public class AuthorizationsController : ControllerBase
    {
        private readonly IAuthorizationBussiness _Bussiness;

        public AuthorizationsController(IAuthorizationBussiness bussiness)
        {
            this._Bussiness = bussiness;
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

                return Created($"authorization/{value.IdMenu}/{value.IdRole}", value);
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