using WebApiRest.NetCore.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using System.Net;
using WebApiRest.NetCore.Models.Tools;
using Microsoft.Extensions.Logging;
using WebApiRest.NetCore.Models.Dao;
using WebApiRest.NetCore.Models;

namespace WebApiRest.NetCore.Controllers
{
  [Authorize]
  [Route("api/[controller]")]
  [ApiController]
  public class UsersController : ControllerBase
  {
    private readonly IUserDao _Dao;
    private readonly IConfiguration _Configuration;
    private readonly ILogger _Logger;

    public UsersController(IUserDao dao, IConfiguration confirugation, ILoggerFactory loggerFactory)
    {
      this._Dao = dao;
      this._Configuration = confirugation;
    }

    // GET api/users
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

    // GET api/users/5
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

    // POST api/users
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] UserDto user)
    {
      try
      {
        if (user == null)
          throw new Exception("Missing parameter (user)!");

        var value = await this._Dao.Create(user);

        if (value == null || value.Id == 0)
          throw new Exception();

        return Created($"user/{value.Id}", value);
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

    // POST api/users
    [AllowAnonymous]
    [HttpPost]
    [Route("auth")]
    public async Task<IActionResult> Auth([FromBody] UserDto user)
    {
      try
      {
        if (user == null)
          throw new Exception("Missing parameter (user)!");

        var value = await this._Dao.Read(user.Login, user.Password);

        if (value == null || value.Id == 0)
          throw new Exception("Login or password was incorrect.");

        var token = Methods.GetJWT(
                      this._Configuration.GetSection("AppSettings:SecurityKey").Value
                    );

        return StatusCode(202, token);
      }
      catch (Exception ex)
      {
        switch (ex.InnerException)
        {
          case Exception exception:
            return BadRequest(exception.Message);
          // Not InnerException
          default:
            return Unauthorized(ex.Message);
        }
      }
    }

    // POST api/users
    [AllowAnonymous]
    [HttpGet]
    [Route("auth")]
    public IActionResult Auth(string statusCode)
    {
        return StatusCode(
          HttpStatusCode.Unauthorized.ExtConvertToInt32(),
          @"You must connect to ""api/users/auth"" with the ""login"" and ""password"" settings.."
        );
    }

    // PUT api/users/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int? id, [FromBody] UserDto user)
    {
      try
      {
        if (user == null)
          throw new Exception("Missing parameter (user)!");

        if (id.Value == 0)
          throw new Exception("The resource id must not be 0!");

        user.Id = id.Value;

        await this._Dao.Update(user);

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

    // DELETE api/users/5
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
