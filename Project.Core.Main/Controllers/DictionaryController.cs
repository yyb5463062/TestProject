using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Project.Common.Logs;
using Project.Common.Token;
using Project.IService.Interface;

namespace Project.Core.Main.Controllers
{
    /// <summary>
    /// 字典操作
    /// </summary>
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class DictionaryController : BaseApiController
    {
        private readonly ISys_DictionaryService _service;
        private readonly ILogger<DictionaryController> _logger;
        public DictionaryController(ISys_DictionaryService service, ILogger<DictionaryController> logger)
        {
            this._service = service;
            this._logger = logger;
        }
        // GET: api/Dictionary
        [HttpGet]
        public object Get(string token)
        {
            _logger.LogInformation("获取字典表记录");
            return Ok(_service.Query());
            //return Ok("验证失败!");
        }

        // GET: api/Dictionary/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            //id = id / 0;
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
