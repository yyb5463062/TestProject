﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System.Linq;

namespace Project.Common.Appsettings
{
    /// <summary>
    /// appsettings.json操作类
    /// </summary>
    public class AppSettingsHelper
    {
        static IConfiguration Configuration { get; set; }
        //static string contentPath { get; set; }
        //public static string SqlServerConnection = Configuration.GetSection("AppSettings:SqlServerConnection").Value;
        //static AppSettingsHelper()
        //{
        //    string Path = "appsettings.json";
        //    Configuration = new ConfigurationBuilder()
        //        .SetBasePath(Directory.GetCurrentDirectory())
        //        .Add(new JsonConfigurationSource
        //        {
        //            Path = Path,
        //            Optional = false,
        //            ReloadOnChange = true
        //        })
        //        .Build();// 这样的话，可以直接读目录里的json文件，而不是 bin 文件夹下的，所以不用修改复制属性
        //}

        public AppSettingsHelper(string contentPath)
        {
            string Path = "appsettings.json";

            Configuration = new ConfigurationBuilder()
                .SetBasePath(contentPath)
                .Add(new JsonConfigurationSource { Path = Path, Optional = false, ReloadOnChange = true })//这样的话，可以直接读目录里的json文件，而不是 bin 文件夹下的，所以不用修改复制属性
                .Build();
        }
        /// <summary>
        /// 组合要操作的字符
        /// </summary>
        /// <param name="sections"></param>
        /// <returns>返回要访问的字符串</returns>
        public static string app(params string[] sections)
        {
            try
            {
                if(sections.Any())
                {
                    return Configuration[string.Join(":", sections)];
                }
            }
            catch
            {
                
            }
            return "";
        }
    }
}
