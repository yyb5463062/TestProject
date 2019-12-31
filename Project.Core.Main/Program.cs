using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;

namespace Project.Core.Main
{
    public class Program
    {
        public static void Main(string[] args)
        {
            NLogBuilder.ConfigureNLog("NLog.config");//日志配置文件读入
            //创建主机生成器（都是一些主机配置），创建主机，运行主机
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)//创建默认生成器
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                //配置web主机
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>()
                   .UseNLog()
                   .ConfigureLogging((hostingContext, builder) =>
                   {
                       //builder.ClearProviders();//清空自动提供的日志功能
                       builder.SetMinimumLevel(LogLevel.Trace);//设置日志最小级别
                       builder.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                       builder.AddConsole();
                       builder.AddDebug();
                   });
                });
    }
}
