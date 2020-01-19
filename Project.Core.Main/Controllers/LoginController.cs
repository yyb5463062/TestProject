using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Common.Token;

namespace Project.Core.Main.Controllers
{
    /// <summary>
    /// 登录
    /// </summary>
    [Route("api/[controller]/[Action]")]
    [ApiController]
    [Authorize]
    public class LoginController : ControllerBase
    {
        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="name">账号</param>
        /// <param name="pass">密码</param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public IActionResult GetJwtStr(string name, string pass)
        {
            TokenHelper.GetJwtStr(name, pass);
            return Ok(TokenHelper.token);
        }
        // GET: api/Login
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Login/5
        //[HttpGet("{id}", Name = "Get")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST: api/Login
        [HttpPost]
        public IActionResult Post([FromBody] string value)
        {
            return Ok("成功");
        }

        // PUT: api/Login/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] string value)
        {
            return Ok("成功");
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok("成功");
        }
    }
}
