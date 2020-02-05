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
            NLogBuilder.ConfigureNLog("NLog.config");//��־�����ļ�����
            //��������������������һЩ�������ã���������������������  
            //��������webӦ�ó���������������ڹ������÷�����������ܵ�
            //����������־��������ϵ��ע��
            //����ʵ������һ����װ��Ӧ����Դ�Ķ���
            //��������������->��������->��������->��������
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)//����Ĭ��������
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                //����web����,����kestrel
                .ConfigureWebHostDefaults(webBuilder =>
                {//�������
                    webBuilder.UseStartup<Startup>()
                   .UseNLog()
                   .ConfigureLogging((hostingContext, builder) =>
                   {
                       //builder.ClearProviders();//����Զ��ṩ����־����
                       builder.SetMinimumLevel(LogLevel.Trace);//������־��С����
                       builder.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                       builder.AddConsole();
                       builder.AddDebug();
                   });
                });
    }
}
