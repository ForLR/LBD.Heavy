using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Heavy.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Heavy.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Heavy.Identity.Model;
using Heavy.Identity.Data;
using Heavy.Identity.Auth;
using Heavy.Ioc;
using Heavy.Repository;
using MediatR;

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

            //数据库
            services.AddDbContext<ApplicationDbContext>(options => options.UseMySql(Configuration.GetConnectionString("DefaultConnection")));


            services.AddIdentity<User, IdentityRole>(option=> 
            {
                option.Password.RequiredLength = 1;
                option.Password.RequireNonAlphanumeric = false;
                option.Password.RequireLowercase = false;
                option.Password.RequireUppercase = false;
            }).AddEntityFrameworkStores<ApplicationDbContext>();
           

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
            });
            #endregion

            //内存缓存
            services.AddMemoryCache();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddMediatR(typeof(Startup));

            // Inject 注入
            RegisterService(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
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
            
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
              
            });
        }

        public static void RegisterService(IServiceCollection services)
        {
            services.AddScoped<UserRepository>();
            NativeInjector.RegisterService(services);

        }
    }
}
