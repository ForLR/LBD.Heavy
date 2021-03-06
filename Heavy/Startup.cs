﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Heavy.Identity.Auth;
using Heavy.Ioc;
using MediatR;
using Microsoft.Extensions.Hosting;
using FluentValidation.AspNetCore;
using Autofac;
using Serilog;
using System.Reflection;
using Heavy.Utility;

namespace Heavy
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
           // LoggerFactory = loggerFactory;
        }

        public IConfiguration Configuration { get; }

        //public ILoggerFactory LoggerFactory { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.Strict;

            });


            #region 授权
            services.AddAuthorization(option =>
            {
               
                option.AddPolicy("仅限lurui", policy => 
                {
                    policy.RequireRole("lurui");
                });
                //option.AddPolicy("仅限Claims",policy=>policy.RequireClaim("Edit Albums"));
                //option.AddPolicy("仅限Claims", policy => policy.RequireAssertion(context =>
                //   {
                //       if (context.User.HasClaim(x => x.Type == "Edit Albums"))
                //           return true;
                //       return false;
                //   }));
                
            option.AddPolicy("仅限qq邮箱", policy => policy.AddRequirements
             (
                 new EmailRequirement("@qq.com")
                // new QualifiedUserRequirement("lurui")
            ));

            option.AddPolicy("ReadAuth", policy => policy.AddRequirements(new ReadAuthRequirement()));

            });
            #endregion

            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0).AddFluentValidation();

            //MediatR
            services.AddMediatR(typeof(Startup));
            //services.AddControllers().AddNewtonsoftJson();
            // Inject 注入
            //services.RegisterService(Configuration);
            services.RegisterSynthesize(Configuration);

            services.AddControllersWithViews(options=> 
            {
               // options.Filters.Add<>(); 添加全局过滤器
            })
                .AddRazorRuntimeCompilation() ////修改cshtml后能自动编译
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                .AddFluentValidation();
        }


        public void ConfigureContainer(ContainerBuilder containerBuilder)
        {
           containerBuilder.RegisterModule<HeavyAutofacModule>();
           // NativeInjector.RegisterServiceForAutofac(containerBuilder, this.GetType().GetTypeInfo().Assembly);

        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,ILoggerFactory loggerFactory)
        {
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // app.UseHsts();
            }
            app.UseSerilogRequestLogging();

            //自定义日志配置
            loggerFactory.AddProvider(new ColoredConsoleLoggerProvider(new ColoredConsoleLoggerConfiguration()
            {
                LogLevel = LogLevel.Information,
                Color=System.ConsoleColor.Blue
            }));
            loggerFactory.AddProvider(new ColoredConsoleLoggerProvider(new ColoredConsoleLoggerConfiguration()
            {
                LogLevel = LogLevel.Debug,
                Color = System.ConsoleColor.Yellow
            }));

           

            // app.UseHttpsRedirection();
            app.UseStaticFiles();
            //app.UseCookiePolicy();
         


            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                   name: "default",
                   pattern: "{controller=Home}/{action=Index}/{id?}");//中心化路由--默认支持特性路由

                //区域路由
                endpoints.MapAreaControllerRoute(
                    name: "areas", "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                endpoints.UseMapGetDefault();

            });
        }

     
    }
}
