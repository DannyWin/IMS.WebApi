using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using IMS.IRepository;
using IMS.IService;
using IMS.Model;
using IMS.Repository;
using IMS.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace IMS.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            BaseDBConfig.ConnectionString = Configuration.GetSection("AppSettings:SqlServerConnection").Value;
            //BaseRepository<User>.connection= Configuration.GetSection("ConnectionStrings:MySqlConnection").Value;
            //services.AddDbContext<DataContext>(options => options.UseSqlServer(Configuration.GetConnectionString("MySqlConnection")));
            //services.AddSingleton(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            //services.AddScoped(typeof(IBaseService<>), typeof(BaseService<>));
            //services.AddScoped<IOrgService, OrgService>();
            //services.AddScoped<IOrgRepository, OrgRepository>();
            //services.AddScoped<IUserService, UserService>();
            //services.AddScoped<IUserRepository, UserRepository>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            #region Automapper
            services.AddAutoMapper(typeof(Startup));
            #endregion

            #region Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v0.1.0",
                    Title = "WebApi API",
                    Description = "框架说明文档",
                    TermsOfService = "None",
                    Contact = new Swashbuckle.AspNetCore.Swagger.Contact { Name = "IMS.WebApi", Email = "1@1.com", Url = "https://127.0.0.1" }
                });
                #region 读取xml信息
                var basePath = Microsoft.DotNet.PlatformAbstractions.ApplicationEnvironment.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, "IMS.WebApi.xml");//这个就是刚刚配置的xml文件名
                c.IncludeXmlComments(xmlPath, true);//默认的第二个参数是false，这个是controller的注释，记得修改
                var xmlModelPath = Path.Combine(basePath, "IMS.Model.xml");//这个就是Model层的xml文件名
                c.IncludeXmlComments(xmlModelPath);
                #endregion

                //#region Token绑定到ConfigureServices
                ////添加header验证信息
                ////c.OperationFilter<SwaggerHeader>();
                //var security = new Dictionary<string, IEnumerable<string>> { { "WebApi", new string[] { } }, };
                //c.AddSecurityRequirement(security);
                ////方案名称“WebApi”可自定义，上下一致即可
                //c.AddSecurityDefinition("WebApi", new ApiKeyScheme
                //{
                //    Description = "JWT授权(数据将在请求头中进行传输) 直接在下框中输入Bearer {token}（注意两者之间是一个空格）\"",
                //    Name = "Authorization",//jwt默认的参数名称
                //    In = "header",//jwt默认存放Authorization信息的位置(请求头中)
                //    Type = "apiKey"
                //});
                //#endregion

                //#region Token服务注册
                //services.AddSingleton<IMemoryCache>(factory =>
                //{
                //    var cache = new MemoryCache(new MemoryCacheOptions());
                //    return cache;
                //});
                //services.AddAuthorization(options =>
                //{
                //    options.AddPolicy("Client", policy => policy.RequireRole("Client").Build());
                //    options.AddPolicy("Admin", policy => policy.RequireRole("Admin").Build());
                //    options.AddPolicy("AdminOrClient", policy => policy.RequireRole("Admin", "Client").Build());
                //});
                //#endregion

            });

            #endregion

            #region AutoFac

            //实例化 AutoFac  容器   
            var builder = new ContainerBuilder();

            //注册要通过反射创建的组件
            //builder.RegisterType<UserService>().As<IUserService>();
            //builder.RegisterType<UserRepository>().As<IUserRepository>();
            var assemblysServices = Assembly.Load("IMS.Service");//要记得!!!这个注入的是实现类层，不是接口层！不是 IServices
            builder.RegisterAssemblyTypes(assemblysServices).AsImplementedInterfaces();//指定已扫描程序集中的类型注册为提供所有其实现的接口。
            var assemblysRepository = Assembly.Load("IMS.Repository");//要记得!!!这个注入的是实现类层，不是接口层！不是 IRepository
            builder.RegisterAssemblyTypes(assemblysRepository).AsImplementedInterfaces();
            //将services填充到Autofac容器生成器中
            builder.Populate(services);

            //使用已进行的组件登记创建新容器
            var ApplicationContainer = builder.Build();

            #endregion

            return new AutofacServiceProvider(ApplicationContainer);//第三方IOC接管 core内置DI容器
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            #region Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiHelp V1");
            });
            #endregion

            app.UseMvc();
        }
    }
}
