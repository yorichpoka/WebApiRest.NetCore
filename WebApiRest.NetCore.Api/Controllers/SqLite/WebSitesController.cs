using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Threading.Tasks;
using WebApiRest.NetCore.Api.Filters;
using WebApiRest.NetCore.Domain;
using WebApiRest.NetCore.Domain.Interfaces.Bussiness.SqLite;
using WebApiRest.NetCore.Domain.Models.SqLite;

namespace WebApiRest.NetCore.Api.Controllers.SqLite
{
    [Authorize]
    [Route("api/sqlite/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(CustomActionFilter))]
    public class WebSitesController : ControllerBase
    {
        private readonly IWebSiteBussiness _Bussiness;

        public WebSitesController(IWebSiteBussiness business)
        {
            this._Bussiness = business;
        }

        // GET api/webSites
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

        // GET api/webSites/5
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

        // POST api/webSites
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] WebSiteModel webSite)
        {
            try
            {
                if (webSite == null)
                    throw new Exception("Missing parameter (webSite)!");

                var value = await this._Bussiness.Create(webSite);

                if (value == null || value.Key == 0)
                    throw new Exception();

                return Created($"webSite/{value.Id}", value);
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

        // PUT api/webSites/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int? id, [FromBody] WebSiteModel webSite)
        {
            try
            {
                if (webSite == null)
                    throw new Exception("Missing parameter (webSite)!");

                if (id.Value == 0)
                    throw new Exception("The resource id must not be 0!");

                webSite.Key = id.Value;

                await this._Bussiness.Update(webSite);

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

        // DELETE api/webSites/5
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