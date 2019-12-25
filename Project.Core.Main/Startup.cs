using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Autofac.Extras.DynamicProxy;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;

namespace Project.Core.Main
{
    /// <summary>
    /// ���÷���͹ܵ�
    /// </summary>
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            //services.AddCors();
            //services.AddMvc();
            services.AddAutofac();
            var ConnectionString = Configuration.GetSection("AppSettings:SqlServerConnection").Value;
            #region Swagger

            services.AddSwaggerGen(p =>
            {
                p.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Version = "V0.1.0",
                    Title = "Project.Core API",
                    Description = "�ӿ�˵���ĵ�",
                    //TermsOfService = "None",
                    //Contact = new Swashbuckle.AspNetCore.Swagger.Contact
                    //{
                    //    Name = "����",
                    //    Url = "http://www.baidu.com"
                    //}
                });
                //��������

                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, "Project.Core.Main.xml");//������Ǹո����õ�xml�ļ���
                //var xmlModelPath = Path.Combine(basePath, "Blog.Core.Model.xml");//�������Model���xml�ļ���
                //p.IncludeXmlComments(xmlPath, true);//Ĭ�ϵĵڶ���������false�������controller��ע�ͣ��ǵ��޸�
                p.IncludeXmlComments(xmlPath, true);//Ĭ�ϵĵڶ���������false�������controller��ע�ͣ��ǵ���
            });

            #endregion

            #region Token�󶨵�ConfigureServices
            //���header��֤��Ϣ
            //c.OperationFilter<SwaggerHeader>();
            //var security = new Dictionary<string, IEnumerable<string>> { { "Blog.Core", new string[] { } }, };
            //c.AddSecurityRequirement(security);
            //�������ơ�Blog.Core�����Զ��壬����һ�¼���
            //c.AddSecurityDefinition("Blog.Core", new ApiKeyScheme
            //{
            //    Description = "JWT��Ȩ(���ݽ�������ͷ�н��д���) ֱ�����¿�������{token}\"",
            //    Name = "Authorization",//jwtĬ�ϵĲ�������
            //    In = "header",//jwtĬ�ϴ��Authorization��Ϣ��λ��(����ͷ��)
            //    Type = "apiKey"
            //});
            #endregion

            #region Token����ע��
            //services.AddSingleton<IMemoryCache>(factory =>
            //{
            //    var cache = new MemoryCache(new MemoryCacheOptions());
            //    return cache;
            //});
            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("Admin", policy => policy.RequireClaim("AdminType").Build());//ע��Ȩ�޹��������Զ�����
            //});
            #endregion

        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            //var basePath = Microsoft.DotNet.PlatformAbstractions.ApplicationEnvironment.ApplicationBasePath;
            var basePath = PlatformServices.Default.Application.ApplicationBasePath;
           // builder.(Assembly.GetExecutingAssembly()).PropertiesAutowired();

            try
            {
                //builder.RegisterType<Project.Services>().As<Project.IServices>();
                #region Service.dll ע�룬�ж�Ӧ�ӿ�
                //��ȡ��Ŀ����·������ע�⣬�����ʵ�����dll�ļ������ǽӿ� IService.dll ��ע��������Ȼ��Activatore
                var servicesDllFile = Path.Combine(basePath, "Project.Service.dll");
                var assemblysServices = Assembly.LoadFrom(servicesDllFile);//ֱ�Ӳ��ü����ļ��ķ���  ��������� ������ǵ�һ��������Ŀ������F6���룬Ȼ����F5ִ�У����������

                builder.RegisterAssemblyTypes(assemblysServices)
                    .AsImplementedInterfaces();
                   // .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);//ָ����ɨ������е�����ע��Ϊ�ṩ������ʵ�ֵĽӿڡ�


                // AOP ���أ������Ҫ��ָ���Ĺ��ܣ�ֻ��Ҫ�� appsettigns.json ��Ӧ��Ӧ true ���С�
                //var cacheType = new List<Type>();
                //if (Appsettings.app(new string[] { "AppSettings", "RedisCachingAOP", "Enabled" }).ObjToBool())
                //{
                //    cacheType.Add(typeof(BlogRedisCacheAOP));
                //}
                //if (Appsettings.app(new string[] { "AppSettings", "MemoryCachingAOP", "Enabled" }).ObjToBool())
                //{
                //    cacheType.Add(typeof(BlogCacheAOP));
                //}
                //if (Appsettings.app(new string[] { "AppSettings", "TranAOP", "Enabled" }).ObjToBool())
                //{
                //    cacheType.Add(typeof(BlogTranAOP));
                //}
                //if (Appsettings.app(new string[] { "AppSettings", "LogAOP", "Enabled" }).ObjToBool())
                //{
                //    cacheType.Add(typeof(BlogLogAOP));
                //}

                //builder.RegisterAssemblyTypes(assemblysServices)
                //          .AsImplementedInterfaces()
                //          .InstancePerLifetimeScope()
                //          .EnableInterfaceInterceptors();//����Autofac.Extras.DynamicProxy;
                //                                        // �������ע������������ôд  InterceptedBy(typeof(BlogCacheAOP), typeof(BlogLogAOP));
                //                                        // �����ʹ��Redis���棬����뿪�� redis ���񣬶˿ں��ҵ���6319�������һ��������Ч��������ʹ��memory���� BlogCacheAOP
                //          .InterceptedBy(cacheType.ToArray());//����������������б�����ע�ᡣ 
                #endregion

                #region Repository.dll ע�룬�ж�Ӧ�ӿ�
                var repositoryDllFile = Path.Combine(basePath, "Repository.dll");
                var assemblysRepository = Assembly.LoadFrom(repositoryDllFile);
                builder.RegisterAssemblyTypes(assemblysRepository).AsImplementedInterfaces();
                #endregion
            }
            catch (Exception ex)
            {
                //log.Error("Repository.dll��service.dll ��ʧ����Ϊ��Ŀ�����ˣ�������Ҫ��F6���룬��F5���У����鲢������\n" + ex.Message);

                //throw new Exception("��������� ������ǵ�һ��������Ŀ�����ȶ������������dotnet build��F6���룩��Ȼ���ٶ�api�� dotnet run��F5ִ�У���\n��Ϊ�����ˣ�������Ƿ�����ģʽ������bin�ļ����Ƿ����Repository.dll��service.dll ���������" + ex.Message + "\n" + ex.InnerException);
            }
            #region û�нӿڲ�ķ����ע��

            ////��Ϊû�нӿڲ㣬���Բ���ʵ�ֽ��ֻ���� Load ������
            ////ע�����ʹ��û�нӿڵķ��񣬲������ʹ�� AOP ���أ��ͱ�������Ϊ�鷽��
            ////var assemblysServicesNoInterfaces = Assembly.Load("Blog.Core.Services");
            ////builder.RegisterAssemblyTypes(assemblysServicesNoInterfaces);

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //�ж��Ƿ񿪷�����
            if (env.IsDevelopment())
            {
                // �ڿ��������У�ʹ���쳣ҳ�棬�������Ա�¶�����ջ��Ϣ�����Բ�Ҫ��������������
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // �ڷǿ��������У�ʹ��HTTP�ϸ�ȫ����(or HSTS) ���ڱ���web��ȫ�Ƿǳ���Ҫ�ġ�
                // ǿ��ʵʩ HTTPS �� ASP.NET Core����� app.UseHttpsRedirection
                //app.UseHsts();
            }
            #region Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiHelp V1");
                //c.RoutePrefix = "";//·�����ã�����Ϊ�գ���ʾֱ�ӷ��ʸ��ļ���
                                   //·�����ã�����Ϊ�գ���ʾֱ���ڸ�������localhost:8001�����ʸ��ļ�,ע��localhost:8001/swagger�Ƿ��ʲ����ģ�
                                   //���ʱ��ȥlaunchSettings.json�а�"launchUrl": "swagger/index.html"ȥ���� Ȼ��ֱ�ӷ���localhost:8001/index.html����
            });
            #endregion
            app.UseRouting();
            //app.UseMvc();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
