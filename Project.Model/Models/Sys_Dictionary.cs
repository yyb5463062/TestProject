using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Model.Models
{
    /// <summary>
    /// 系统字典实体
    /// </summary>
    [SugarTable("Sys_Dictionary")]
    public class Sys_Dictionary
    {
        /// <summary>
        /// 编码
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public int ID { get; set; }
        /// <summary>
        /// 字典分类
        /// </summary>
        public int DicType { get; set; }
        /// <summary>
        /// 	名称
        /// </summary>
        public string WorkName { get; set; }
        /// <summary>
        /// 	值
        /// </summary>
        public string WorkValue { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatTime { get; set; }
        /// <summary>
        /// 是否可用
        /// </summary>
        public int IsEnabled { get; set; }
        /// <summary>
        /// 默认值
        /// </summary>
        public string DefaultValue { get; set; }
    }
}
