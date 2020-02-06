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
    /// 配置服务和管道
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
            //services.AddSingleton(new NLogHelper());//单纯用NLog
            services.AddSingleton(new AppSettingsHelper(Env.ContentRootPath));

            services.AddMemoryCacheSetup();//3.X之后如果只用AddControllers则必须加(不再集成在里面)，如果用AddMvc则可省

            services.AddCors(options => options.AddPolicy("any", builder => builder.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin().AllowCredentials()));//跨域

            services.AddSwaggerSetup();//添加swagger服务

            services.AddAuthorizationSetup();//添加权限认证

            services.AddControllers(p =>
            {
                //p.Filters.Add(typeof(WebApiExceptionFilterAttribute));//全局异常过滤记录日志
            })
            //全局配置Json序列化处理
            .AddNewtonsoftJson(options =>
            {
                //忽略循环引用
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                //不使用驼峰样式的key
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                //设置时间格式
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
                #region Service.dll 注入，有对应接口
                //获取项目绝对路径，请注意，这个是实现类的dll文件，不是接口 IService.dll ，注入容器当然是Activatore
                var servicesDllFile = Path.Combine(basePath, "Project.Service.dll");
                var assemblysServices = Assembly.LoadFrom(servicesDllFile);//直接采用加载文件的方法  ※※★※※ 如果你是第一次下载项目，请先F6编译，然后再F5执行，※※★※※

                builder.RegisterAssemblyTypes(assemblysServices)
                    .AsImplementedInterfaces();
                   // .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);//指定已扫描程序集中的类型注册为提供所有其实现的接口。


                // AOP 开关，如果想要打开指定的功能，只需要在 appsettigns.json 对应对应 true 就行。
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
                //          .EnableInterfaceInterceptors();//引用Autofac.Extras.DynamicProxy;
                //                                        // 如果你想注入两个，就这么写  InterceptedBy(typeof(BlogCacheAOP), typeof(BlogLogAOP));
                //                                        // 如果想使用Redis缓存，请必须开启 redis 服务，端口号我的是6319，如果不一样还是无效，否则请使用memory缓存 BlogCacheAOP
                //          .InterceptedBy(cacheType.ToArray());//允许将拦截器服务的列表分配给注册。 
                #endregion

                #region Repository.dll 注入，有对应接口
                var repositoryDllFile = Path.Combine(basePath, "Repository.dll");
                var assemblysRepository = Assembly.LoadFrom(repositoryDllFile);
                builder.RegisterAssemblyTypes(assemblysRepository).AsImplementedInterfaces();
                #endregion
            }
            catch //(Exception ex)
            {
                //log.Error("Repository.dll和service.dll 丢失，因为项目解耦了，所以需要先F6编译，再F5运行，请检查并拷贝。\n" + ex.Message);

                //throw new Exception("※※★※※ 如果你是第一次下载项目，请先对整个解决方案dotnet build（F6编译），然后再对api层 dotnet run（F5执行），\n因为解耦了，如果你是发布的模式，请检查bin文件夹是否存在Repository.dll和service.dll ※※★※※" + ex.Message + "\n" + ex.InnerException);
            }
            #region 没有接口层的服务层注入

            ////因为没有接口层，所以不能实现解耦，只能用 Load 方法。
            ////注意如果使用没有接口的服务，并想对其使用 AOP 拦截，就必须设置为虚方法
            ////var assemblysServicesNoInterfaces = Assembly.Load("Blog.Core.Services");
            ////builder.RegisterAssemblyTypes(assemblysServicesNoInterfaces);

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        //配置中间件，中间件组成管道
        //中间件就是处理http请i求和相应的
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //判断是否开发环境
            if (env.IsDevelopment())
            {
                // 在开发环境中，使用异常页面，这样可以暴露错误堆栈信息，所以不要放在生产环境。
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // 在非开发环境中，使用HTTP严格安全传输(or HSTS) 对于保护web安全是非常重要的。
                // 强制实施 HTTPS 在 ASP.NET Core，配合 app.UseHttpsRedirection
                //app.UseHsts();
            }

            #region Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiHelp V1");
                //c.RoutePrefix = "";//路径配置，设置为空，表示直接访问该文件，
                                   //路径配置，设置为空，表示直接在根域名（localhost:8001）访问该文件,注意localhost:8001/swagger是访问不到的，
                                   //这个时候去launchSettings.json中把"launchUrl": "swagger/index.html"去掉， 然后直接访问localhost:8001/index.html即可
            });
            #endregion

            //跨域
            app.UseCors("LimitRequests");
            //使用静态文件
            app.UseStaticFiles();
            // 使用cookie
            app.UseCookiePolicy();
            // 返回错误码
            app.UseStatusCodePages();//把错误码返回前台，比如是404
            //路由中间件
            app.UseRouting();
            // 先开启认证
            app.UseAuthentication();
            // 然后是授权中间件
            app.UseAuthorization();
            //
            //app.UseMiniProfiler();

            //终结点中间件和路由中间件配套使用
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
