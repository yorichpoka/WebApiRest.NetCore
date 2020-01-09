using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Threading.Tasks;
using WebApiRest.NetCore.Api.Filters;
using WebApiRest.NetCore.Domain;
using WebApiRest.NetCore.Domain.Interfaces.Bussiness;
using WebApiRest.NetCore.Domain.Models.MongoDB;

namespace WebApiRest.NetCore.Api.Controllers.MongoDB
{
    [Authorize]
    [Route("api/mongodb/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(CustomActionFilter))]
    public class DocumentsController : ControllerBase
    {
        private readonly IDocumentBussiness _Bussiness;

        public DocumentsController(IDocumentBussiness business)
        {
            this._Bussiness = business;
        }

        // GET api/documents/5
        [HttpGet("{document_id}")]
        public async Task<IActionResult> Get(string document_id)
        {
            try
            {
                var values = await this._Bussiness.Read(document_id);

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

        // POST api/documents
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] DocumentModel document)
        {
            try
            {
                if (document == null)
                    throw new Exception("Missing parameter (document)!");

                var value = await this._Bussiness.Create(document);

                if (value == null || value.Id == null)
                    throw new Exception();

                return Created($"document/{value.Id}", value);
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

        // PUT api/documents/5
        [HttpPut("{document_id}")]
        public async Task<IActionResult> Put(string document_id, [FromBody] DocumentModel document)
        {
            try
            {
                if (document == null)
                    throw new Exception("Missing parameter (document)!");

                if (document_id == null)
                    throw new Exception("The resource id must not be null!");

                document.Document_Id = document_id;

                await this._Bussiness.Update(document);

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

        // DELETE api/documents/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string document_id)
        {
            try
            {
                if (document_id == null)
                    throw new Exception("The resource id must not be 0!");

                await this._Bussiness.Delete(document_id);

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