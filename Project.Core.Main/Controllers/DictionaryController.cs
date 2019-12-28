using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.IService.Interface;

namespace Project.Core.Main.Controllers
{
    /// <summary>
    /// 字典操作
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DictionaryController : ControllerBase
    {
        ISys_DictionaryService _service;

        public DictionaryController(ISys_DictionaryService service)
        {
            this._service = service;
        }
        // GET: api/Dictionary
        [HttpGet]
        public object Get()
        {
            return Ok(_service.Query());
        }

        // GET: api/Dictionary/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Dictionary
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Dictionary/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
