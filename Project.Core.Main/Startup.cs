using System;
using System.IO;
using System.Reflection;
using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.PlatformAbstractions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Project.Common.Appsettings;
using Project.Core.Main.Extensions;
using Project.Core.Main.Filters;

namespace Project.Core.Main
{
    /// <summary>
    /// ���÷���͹ܵ�
    /// </summary>
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Env = env;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Env { get; }
       

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddSingleton(new NLogHelper());//������NLog
            services.AddSingleton(new AppSettingsHelper(Env.ContentRootPath));

            services.AddMemoryCacheSetup();//3.X֮�����ֻ��AddControllers������(���ټ���������)�������AddMvc���ʡ

            services.AddCors(options => options.AddPolicy("any", builder => builder.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin().AllowCredentials()));//����

            services.AddSwaggerSetup();//���swagger����

            services.AddAuthorizationSetup();//���Ȩ����֤

            services.AddControllers(p =>
            {
                //p.Filters.Add(typeof(WebApiExceptionFilterAttribute));//ȫ���쳣���˼�¼��־
            })
            //ȫ������Json���л�����
            .AddNewtonsoftJson(options =>
            {
                //����ѭ������
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                //��ʹ���շ���ʽ��key
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                //����ʱ���ʽ
                //options.SerializerSettings.DateFormatString = "yyyy-MM-dd";
            });
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterType<ControllerBase>().PropertiesAutowired();
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
            catch //(Exception ex)
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
        //�����м�����м����ɹܵ�
        //�м�����Ǵ���http��i�����Ӧ��
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

            //����
            app.UseCors("LimitRequests");
            //ʹ�þ�̬�ļ�
            app.UseStaticFiles();
            // ʹ��cookie
            app.UseCookiePolicy();
            // ���ش�����
            app.UseStatusCodePages();//�Ѵ����뷵��ǰ̨��������404
            //·���м��
            app.UseRouting();
            // �ȿ�����֤
            app.UseAuthentication();
            // Ȼ������Ȩ�м��
            app.UseAuthorization();
            //
            //app.UseMiniProfiler();

            //�ս���м����·���м������ʹ��
            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapControllers();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "api/{controller=Login}/{action=GetJwtStr}/"
                    ); 
            });
        }
    }
}
