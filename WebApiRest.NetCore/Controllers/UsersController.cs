using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using WebApiRest.NetCore.Domain.Interfaces;
using WebApiRest.NetCore.Domain.Models;
using WebApiRest.NetCore.Tools;

namespace WebApiRest.NetCore.Controllers
{
  [Authorize]
  [Route("api/[controller]")]
  [ApiController]
  public class UsersController : ControllerBase
  {
    private readonly IUserDao _Dao;
    private readonly IConfiguration _Configuration;

    public UsersController(IUserDao dao, IConfiguration confirugation)
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
        var values = await this._Dao.ReadWithRoles();

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
    public async Task<IActionResult> Post([FromBody] UserModel user)
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
    public async Task<IActionResult> Auth([FromBody] UserModel user)
    {
      try
      {
        if (user == null)
          throw new Exception("Missing parameter (user)!");

        var value = await this._Dao.Read(user.Login, user.Password);

        if (value == null || value.Id == 0)
          throw new Exception("Login or password was incorrect.");

        var token = Methods.GetJWT(
                      this._Configuration.GetSection("AppSettings:SecurityKey").Value,
                      new TimeSpan(0, 0, 5, 0)
                    );

        // Set id user.
        token.User = value;

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

    // PUT api/users/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int? id, [FromBody] UserModel user)
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
