using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Common.Token
{
    /// <summary>
    /// token缓存
    /// </summary>
    public class TokenHelper
    {
        /// <summary>
        /// 记录token
        /// </summary>
        public static string token { get; set; }
        /// <summary>
        /// token生成日期
        /// </summary>
        public static DateTime DateTimeNow { get; set; }
        /// <summary>
        /// 获取token
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="Role"></param>
        public static void GetJwtStr(string userName, string Role)
        {
            string jwtStr = string.Empty;
            bool suc = false;
            // 获取用户的角色名，请暂时忽略其内部是如何获取的，可以直接用 var userRole="Admin"; 来代替更好理解。
            var userRole = "Admin";
            if (userRole != null)
            {
                // 将用户id和角色名，作为单独的自定义变量封装进 token 字符串中。
                TokenModelJwt tokenModel = new TokenModelJwt { Uid = 1, Role = userRole };
                // 登录，获取到一定规则的 Token 令牌
                jwtStr = JWTHelper.IssueJwt(tokenModel);
                suc = true;
            }
            token = jwtStr;
            DateTimeNow = DateTime.Now;
        }
        /// <summary>
        /// 刷新token
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="Role"></param>
        public static void RefreshToken(string userName,string Role)
        {
            GetJwtStr(userName,Role);
        }
        /// <summary>
        /// 验证token
        /// </summary>
        /// <param name="jwt"></param>
        /// <returns></returns>
        public static bool IsToken(string jwt)
        {
            if ((DateTime.Now-DateTimeNow)>TimeSpan.FromMinutes(1))
                GetJwtStr("", "");
            if (token == jwt)
                return true;
            else
                return false;
        }
    }
}
