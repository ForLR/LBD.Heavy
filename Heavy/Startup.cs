using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Heavy.Identity.Model;
using Heavy.Identity.Data;
using Heavy.Identity.Auth;
using Heavy.Ioc;
using MediatR;
using AutoMapper;
using System.Reflection;
using Heavy.Application.AutoMappers;
using Microsoft.Extensions.Hosting;
using FluentValidation.AspNetCore;

namespace Heavy
{
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
           
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;


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

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0).AddFluentValidation();

            //MediatR
            services.AddMediatR(typeof(Startup));
            //services.AddControllers().AddNewtonsoftJson();
            // Inject 注入
            RegisterService(services, Configuration);
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
            app.UseCookiePolicy();


            app.UseRouting();
            app.UseAuthentication();

            app.UseAuthorization();
            app.UseEndpoints(routes =>
            {
             

                routes.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
              
            });
        }

        public static void RegisterService(IServiceCollection services, IConfiguration configuration)
        {
            NativeInjector.RegisterService(services, configuration);

        }
    }
}
