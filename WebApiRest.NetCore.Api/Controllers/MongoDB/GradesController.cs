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
    public class GradesController : ControllerBase
    {
        private readonly IGradeBussiness _Bussiness;

        public GradesController(IGradeBussiness business)
        {
            this._Bussiness = business;
        }

        // GET api/grades
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

        // GET api/grades
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

        // GET api/grades/5
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

        // POST api/grades
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] GradeModel grade)
        {
            try
            {
                if (grade == null)
                    throw new Exception("Missing parameter (grade)!");

                var value = await this._Bussiness.Create(grade);

                if (value == null || value.Id == null)
                    throw new Exception();

                return Created($"grade/{value.Id}", value);
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

        // PUT api/grades/5
        [HttpPut("{grade_id}")]
        public async Task<IActionResult> Put(string grade_id, [FromBody] GradeModel grade)
        {
            try
            {
                if (grade == null)
                    throw new Exception("Missing parameter (grade)!");

                if (grade_id == null)
                    throw new Exception("The resource id must not be null!");

                grade.Grade_Id = grade_id;

                await this._Bussiness.Update(grade);

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

        // DELETE api/grades/5
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