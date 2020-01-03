using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Common.AuthHelper
{
    /// <summary>
    /// 用户或角色或其他凭据实体,就像是订单详情一样
    /// 之前的名字是 Permission
    /// </summary>
    public class PermissionItem
    {
        /// <summary>
        /// 用户或角色或其他凭据名称
        /// </summary>
        public string Role { get; set; }
        /// <summary>
        /// 请求Url
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string Name { get; set; }
    }
}
