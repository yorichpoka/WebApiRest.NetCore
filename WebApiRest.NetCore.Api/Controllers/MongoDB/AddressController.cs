using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Threading.Tasks;
using WebApiRest.NetCore.Api.Filters;
using WebApiRest.NetCore.Domain;
using WebApiRest.NetCore.Domain.Interfaces.Bussiness.MongoDB;
using WebApiRest.NetCore.Domain.Models.MongoDB;

namespace WebApiRest.NetCore.Api.Controllers.MongoDB
{
    [Authorize]
    [Route("api/mongodb/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(CustomActionFilter))]
    public class AddressController : ControllerBase
    {
        private readonly IAddressBussiness _Bussiness;

        public AddressController(IAddressBussiness business)
        {
            this._Bussiness = business;
        }

        // GET api/addresss
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

        // GET api/addresss
        [HttpGet("top/{top}")]
        public async Task<IActionResult> GetTop(int top)
        {
            try
            {
                var values = await this._Bussiness.Read(top);

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

        // GET api/addresss/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
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

        // POST api/addresss
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddressModel address)
        {
            try
            {
                if (address == null)
                    throw new Exception("Missing parameter (address)!");

                var value = await this._Bussiness.Create(address);

                if (value == null || value.Id == null)
                    throw new Exception();

                return Created($"address/{value.Id}", value);
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

        // PUT api/addresss/5
        [HttpPut("{address_id}")]
        public async Task<IActionResult> Put(string address_id, [FromBody] AddressModel address)
        {
            try
            {
                if (address == null)
                    throw new Exception("Missing parameter (address)!");

                if (address_id == null)
                    throw new Exception("The resource id must not be null!");

                address.Address_Id = address_id;

                await this._Bussiness.Update(address);

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

        // DELETE api/addresss/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                if (id == null)
                    throw new Exception("The resource id must not be 0!");

                await this._Bussiness.Delete(id);

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

                await this._Bussiness.Delete(id);

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