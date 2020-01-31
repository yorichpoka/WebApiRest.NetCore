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
    public class RestaurantsController : ControllerBase
    {
        private readonly IRestaurantBussiness _Bussiness;

        public RestaurantsController(IRestaurantBussiness business)
        {
            this._Bussiness = business;
        }

        // GET api/restaurants
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

        // GET api/restaurants/5
        [HttpGet("{restaurant_id}")]
        public async Task<IActionResult> Get(string restaurant_id)
        {
            try
            {
                var values = await this._Bussiness.Read(restaurant_id);

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

        // POST api/restaurants
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RestaurantModel restaurant)
        {
            try
            {
                if (restaurant == null)
                    throw new Exception("Missing parameter (restaurant)!");

                var value = await this._Bussiness.Create(restaurant);

                if (value == null || value.Id == null)
                    throw new Exception();

                return Created($"restaurant/{value.Id}", value);
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

        // PUT api/restaurants/5
        [HttpPut("{restaurant_id}")]
        public async Task<IActionResult> Put(string restaurant_id, [FromBody] RestaurantModel restaurant)
        {
            try
            {
                if (restaurant == null)
                    throw new Exception("Missing parameter (restaurant)!");

                if (restaurant_id == null)
                    throw new Exception("The resource id must not be null!");

                restaurant.Restaurant_Id = restaurant_id;

                await this._Bussiness.Update(restaurant);

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

        // DELETE api/restaurants/5
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