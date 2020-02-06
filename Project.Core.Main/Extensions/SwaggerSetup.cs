using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Project.Common.Appsettings;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Core.Main.Extensions
{
    /// <summary>
    /// Swagger 启动服务
    /// </summary>
    public static class SwaggerSetup
    {
        public static void AddSwaggerSetup(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            var basePath = AppContext.BaseDirectory;
            //var basePath2 = Microsoft.DotNet.PlatformAbstractions.ApplicationEnvironment.ApplicationBasePath;
            var ApiName = AppSettingsHelper.app(new string[] { "Startup", "ApiName" });
            var Version = AppSettingsHelper.app(new string[] { "Startup", "Version" });

            var ApiXML = AppSettingsHelper.app(new string[] { "Swagger", "ApiXML" });
            var ModelXML = AppSettingsHelper.app(new string[] { "Swagger", "ModelXML" });

            services.AddSwaggerGen(c =>
            {
                //遍历出全部的版本，做文档信息展示
                //typeof(ApiVersions).GetEnumNames().ToList().ForEach(version =>
                //{
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = Version,
                    Title = $"{ApiName} 接口文档——Netcore 3.1",
                    Description = $"{ApiName} HTTP API " + Version,
                    //Contact = new OpenApiContact { Name = ApiName, Email = "222222222222@xxx.com", Url = new Uri("https://www.baidu.com/") },
                    //License = new OpenApiLicense { Name = ApiName, Url = new Uri("https://www.baidu.com/") }
                });
                c.OrderActionsBy(o => o.RelativePath);
                //});


                try
                {
                    //就是这里
                    var xmlPath = Path.Combine(basePath, ApiXML);//这个就是刚刚配置的xml文件名
                    c.IncludeXmlComments(xmlPath, true);//默认的第二个参数是false，这个是controller的注释，记得修改

                    var xmlModelPath = Path.Combine(basePath, ModelXML);//这个就是Model层的xml文件名
                    c.IncludeXmlComments(xmlModelPath);

                    #region Token绑定到ConfigureServices
                    // 添加Header验证消息
                    // c.OperationFilter<SwaggerHeader>();
                    //var security = new Dictionary<string, IEnumerable<string>> { { "Blog.Core", new string[] { } }, };
                    //var security = new OpenApiSecurityRequirement();
                    //OpenApiSecurityScheme key = new OpenApiSecurityScheme();
                    //key.BearerFormat = ApiName;
                    //security.Add(key, new List<string>());
                    //c.AddSecurityRequirement(security);
                    //// 方案名称“Blog.Core”可自定义，上下一致即可
                    //c.AddSecurityDefinition("Blog.Core", new ApiKeyScheme
                    //{
                    //    Description = "JWT授权(数据将在请求头中进行传输) 直接在下框中输入Bearer {token}（注意两者之间是一个空格）\"",
                    //    Name = "Authorization", // jwt默认的参数名称
                    //    In = "header", // jwt默认存放Authorization信息的位置(请求头中)
                    //    Type = "apiKey"
                    //});
                    #endregion
                }
                catch //(Exception ex)
                {
                    //log.Error("Blog.Core.xml和Blog.Core.Model.xml 丢失，请检查并拷贝。\n" + ex.Message);
                }
                // 开启加权小锁
                c.OperationFilter<AddResponseHeadersFilter>();
                c.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();

                c.OperationFilter<SecurityRequirementsOperationFilter>();

                // 必须是 oauth2
                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "JWT授权(数据将在请求头中进行传输) 直接在下框中输入Bearer {token}（注意两者之间是一个空格）\"",
                    Name = "Authorization",//jwt默认的参数名称
                    In = ParameterLocation.Header,//jwt默认存放Authorization信息的位置(请求头中)
                    Type = SecuritySchemeType.ApiKey
                });
            });
        }
    }
}
