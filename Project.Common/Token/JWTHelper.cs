using Microsoft.IdentityModel.Tokens;
using Project.Common.Appsettings;
using Project.Common.AuthHelper;
using Project.Common.DataConvert;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Project.Common.Token
{
    /// <summary>
    /// 生成token令牌
    /// </summary>
    public class JWTHelper
    {
        /// <summary>
        /// 获取token
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="Role"></param>
        public static string GetJwtStr(string userName, string Role)
        {
            string jwtStr = string.Empty;
            //bool suc = false;
            // 获取用户的角色名，请暂时忽略其内部是如何获取的，可以直接用 var userRole="Admin"; 来代替更好理解。
            var userRole = "Admin";
            if (userRole != null)
            {
                // 将用户id和角色名，作为单独的自定义变量封装进 token 字符串中。
                TokenModelJwt tokenModel = new TokenModelJwt { UserName = userName, Role = Role };
                // 登录，获取到一定规则的 Token 令牌
                jwtStr = JWTHelper.IssueJwt(tokenModel);
                //suc = true;
            }
            return $"Bearer {jwtStr}";
        }
        /// <summary>
        /// 生产token
        /// </summary>
        /// <param name="tokenModel"></param>
        /// <returns></returns>
        public static string IssueJwt(TokenModelJwt tokenModel)
        {
            string iss = AppSettingsHelper.app(new string[] { "Audience", "Issuer" });
            string aud = AppSettingsHelper.app(new string[] { "Audience", "Audience" });
            string secret = AppSettingsHelper.app(new string[] { "Audience", "Secret" });
            var claims = new List<Claim>
            {
                /*
                 * 特别重要：
                   1、这里将用户的部分信息，比如 uid 存到了Claim 中，如果你想知道如何在其他地方将这个 uid从 Token 中取出来，请看下边的SerializeJwt() 方法，或者在整个解决方案，搜索这个方法，看哪里使用了！
                   2、你也可以研究下 HttpContext.User.Claims ，具体的你可以看看 Policys/PermissionHandler.cs 类中是如何使用的。
                 */
                 new Claim(JwtRegisteredClaimNames.Jti,tokenModel.Uid.ToString()),
                 new Claim(JwtRegisteredClaimNames.Iat,$"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}"),
                 new Claim(JwtRegisteredClaimNames.Nbf,$"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}") ,
                 // 这个就是过期时间，目前是过期1000秒，可自定义，注意JWT有自己的缓冲过期时间
                 new Claim (JwtRegisteredClaimNames.Exp,$"{new DateTimeOffset(DateTime.Now.AddSeconds(60*30)).ToUnixTimeSeconds()}"),
                 new Claim(JwtRegisteredClaimNames.Iss,iss),
                 new Claim(JwtRegisteredClaimNames.Aud,aud),
            };
            // 可以将一个用户的多个角色全部赋予；
            // 作者：DX 提供技术支持；
            claims.AddRange(tokenModel.Role.Split(',').Select(s => new Claim(ClaimTypes.Role, s)));
            //秘钥 (SymmetricSecurityKey 对安全性的要求，密钥的长度太短会报出异常)
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var jwt = new JwtSecurityToken(
                issuer: iss,//颁发者
                audience:$"{tokenModel.UserName+tokenModel.Uid+DateTime.Now.ToString("yyyyMMddHHmmss")}",
                claims: claims,//自定义参数
                notBefore:DateTime.UtcNow,
                expires: DateTime.Now.AddSeconds(30),//过期时间
                signingCredentials: creds//签名证书
                );
            var jwtHandler = new JwtSecurityTokenHandler();
            var encodedJwt = jwtHandler.WriteToken(jwt);
            return encodedJwt;
        }

        /// <summary>
        /// 解析token
        /// </summary>
        /// <param name="jwtStr"></param>
        /// <returns></returns>
        public static TokenModelJwt DeSerializeJwt(string jwtStr)
        {
            if (!string.IsNullOrEmpty(jwtStr))
            {
                jwtStr = jwtStr.Replace("Bearer ", "");
            }
            var jwtHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken jwtToken = new JwtSecurityToken(jwtStr);
            object role;
            try
            {
                jwtToken.Payload.TryGetValue(ClaimTypes.Role, out role);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            var tm = new TokenModelJwt
            {
                Uid = (jwtToken.Id).ObjToInt(),
                Role = role != null ? role.ObjToString() : "",
            };
            return tm;
        }

    }

    /// <summary>
    /// 令牌
    /// </summary>
    public class TokenModelJwt
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Uid { get; set; }
        /// <summary>
        /// UserName
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 角色
        /// </summary>
        public string Role { get; set; }
        /// <summary>
        /// 职能
        /// </summary>
        public string Work { get; set; }
    }

}
