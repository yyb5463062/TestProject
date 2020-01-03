using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Common.AuthHelper
{
    /// <summary>
    /// 用户或角色或其他凭据实体
    /// </summary>
    public class Permissions
    {
        /// <summary>
        /// 用户或角色或其他凭据名称
        /// </summary>
        public  string Name{ get; set; }
        /// <summary>
        /// 请求Url
        /// </summary>
        public  string Url{ get; set; }
    }
}
