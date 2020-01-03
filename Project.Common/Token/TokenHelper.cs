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
        }

        public static void RefreshToken(string userName,string Role)
        {
            GetJwtStr(userName,Role);
        }
        
    }
}
