using Autofac;
using AutoMapper;
using Heavy.Application.AutoMappers;
using Heavy.Application.Interfaces;
using Heavy.Application.Services;
using Heavy.Data.Context;
using Heavy.Data.EventSourcing;
using Heavy.Data.Repository;
using Heavy.Data.Repository.EvenSourcing;
using Heavy.Domain.Core.Bus;
using Heavy.Domain.Core.Events;
using Heavy.Domain.Core.Notifications;
using Heavy.Domain.Interfaces;
using Heavy.Identity.Auth;
using Heavy.Identity.CommandHandler;
using Heavy.Identity.Commands;
using Heavy.Identity.Data;
using Heavy.Identity.Model;
using Heavy.Identity.Repositorys;
using Heavy.Repositorys;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;

namespace Heavy.Ioc
{
    public static class NativeInjector
    {
        /// <summary>
        /// 各种注册
        /// </summary>
        /// <param name="services"></param>
        public static void RegisterSynthesize(this IServiceCollection services, IConfiguration configuration)
        {
            //内存缓存
            services.AddMemoryCache();
            //分布式缓存 redis 
            services.AddDistributedMemoryCache();

            services.AddStackExchangeRedisCache(option =>
            {
                option.InstanceName = configuration.GetSection("Redis")["InstanceName"];
                option.Configuration = configuration.GetSection("Redis")["Connect"];
            });
            //AutoMapper
            services.AddAutoMapper(typeof(AutoMappingConfig));

            services.AddIdentity<User, IdentityRole>(option =>
            {
                option.Password.RequiredLength = 1;
                option.Password.RequireNonAlphanumeric = false;
                option.Password.RequireLowercase = false;
                option.Password.RequireUppercase = false;
                option.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<ApplicationDbContext>();


            services.ConfigureApplicationCookie(option =>
            {
                //如果缓存时间段内有使用 则缓存的保存时间则再次滑动
                option.SlidingExpiration = false;

            });
            var conn = configuration.GetConnectionString("DefaultConnection");


            services.AddDbContext<EventStoreContext>(option => option.UseMySql(conn));

            services.AddDbContext<ApplicationDbContext>(option => option.UseMySql(conn));
            //services.RegisterService(configuration);
        }
        /// <summary>
        /// 默认ioc容器
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void RegisterService(this IServiceCollection services, IConfiguration configuration)
        {
          
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<IEventStore, SqlEventStore>();


            services.AddScoped<IEventStoreRepository, EventStoreRepository>();
            services.AddScoped<IUserAppService, UserAppService>();
         
            services.AddScoped<IMediatorHandler, InMemoryBus>();

            services.AddSingleton<IAuthorizationHandler, EmailHandler>();
            services.AddSingleton<IAuthorizationHandler, RoleHandler>();
            services.AddSingleton<IAuthorizationHandler, ReadHandler>();
           


            services.AddScoped<IRequestHandler<RegisterUserCommand, bool>, UserCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateUserCommand, bool>, UserCommandHandler>();
            services.AddScoped<IRequestHandler<DeleteUserCommand, bool>, UserCommandHandler>();

            services.AddScoped<INotificationHandler<DomainNotificationEvent>, DomainNotificationEventHandler>();

            services.AddScoped<UserRepository>();
            services.AddScoped<ClaimTypeRepository>();
            
            services.AddScoped<IUser, AspNetUser>();
        }


        /// <summary>
        /// 使用autofac 容器
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void RegisterServiceForAutofac(ContainerBuilder containerBuilder,Assembly assembly)
        {

            //#region 指定控制器也由autofac 来进行实例获取 
           // var assembly = this.GetType().GetTypeInfo().Assembly;
            var builder = new ContainerBuilder();
            var manager = new ApplicationPartManager();
            manager.ApplicationParts.Add(new AssemblyPart(assembly));
            manager.FeatureProviders.Add(new ControllerFeatureProvider());
            var feature = new ControllerFeature();
            manager.PopulateFeature(feature);
            builder.RegisterType<ApplicationPartManager>().AsSelf().SingleInstance();
            builder.RegisterTypes(feature.Controllers.Select(ti => ti.AsType()).ToArray()).PropertiesAutowired();
            //#endregion


            containerBuilder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>().SingleInstance();
            containerBuilder.RegisterType<EmailHandler>().As<IAuthorizationHandler>().SingleInstance();
            containerBuilder.RegisterType<RoleHandler>().As<IAuthorizationHandler>().SingleInstance();
            containerBuilder.RegisterType<ReadHandler>().As<IAuthorizationHandler>().SingleInstance();


            containerBuilder.RegisterType<SqlEventStore>().As<IEventStore>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<EventStoreRepository>().As<IEventStoreRepository>().InstancePerLifetimeScope();

            containerBuilder.RegisterType<UserAppService>().As<IUserAppService>().InstancePerLifetimeScope();

            containerBuilder.RegisterType<InMemoryBus>().As<IMediatorHandler>().InstancePerLifetimeScope();



            containerBuilder.RegisterType<UserCommandHandler>().As<IRequestHandler<RegisterUserCommand, bool>>().InstancePerLifetimeScope();

            containerBuilder.RegisterType<UserCommandHandler>().As<IRequestHandler<UpdateUserCommand, bool>>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<UserCommandHandler>().As<IRequestHandler<DeleteUserCommand, bool>>().InstancePerLifetimeScope();

            containerBuilder.RegisterType<DomainNotificationEventHandler>().As<INotificationHandler<DomainNotificationEvent>>().InstancePerLifetimeScope();

            containerBuilder.RegisterType<InMemoryBus>().InstancePerLifetimeScope();

            containerBuilder.RegisterType<ClaimTypeRepository>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<AspNetUser>().As<IUser>().InstancePerLifetimeScope();


          
            //containerBuilder

        }
    }
}
