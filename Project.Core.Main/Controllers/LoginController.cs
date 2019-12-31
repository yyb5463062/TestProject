using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class LoginController : ControllerBase
    {
        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="name">账号</param>
        /// <param name="pass">密码</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult GetJwtStr(string name, string pass)
        {
            string jwtStr = string.Empty;
            bool suc = false;
            // 获取用户的角色名，请暂时忽略其内部是如何获取的，可以直接用 var userRole="Admin"; 来代替更好理解。
            var userRole = "admin";
            if (userRole != null)
            {
                // 将用户id和角色名，作为单独的自定义变量封装进 token 字符串中。
                TokenModelJwt tokenModel = new TokenModelJwt { Uid = 1, Role = userRole };
                // 登录，获取到一定规则的 Token 令牌
                jwtStr = JWTHelper.IssueJwt(tokenModel);
                suc = true;
            }
            else
            {
                jwtStr = "login fail!";
            }
            return Ok(new
            {
                success = suc,
                token = jwtStr,
            });
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
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Login/5
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
