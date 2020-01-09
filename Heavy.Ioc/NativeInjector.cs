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
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Heavy.Ioc
{
    public class NativeInjector
    {
        public static void RegisterService(IServiceCollection services, IConfiguration configuration)
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


            services.ConfigureApplicationCookie(option=> 
            {
                //如果缓存时间段内有使用 则缓存的保存时间则再次滑动
                option.SlidingExpiration = false;
              
            });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<IEventStore, SqlEventStore>();


            services.AddScoped<IEventStoreRepository, EventStoreRepository>();
            services.AddScoped<IUserAppService, UserAppService>();
         
            services.AddScoped<IMediatorHandler, InMemoryBus>();

            services.AddSingleton<IAuthorizationHandler, EmailHandler>();
            services.AddSingleton<IAuthorizationHandler, RoleHandler>();
            services.AddSingleton<IAuthorizationHandler, ReadHandler>();
            services.AddDbContext<EventStoreContext>(option => option.UseMySql(configuration.GetConnectionString("DefaultConnection")));
            
            services.AddDbContext<ApplicationDbContext>(option => option.UseMySql(configuration.GetConnectionString("DefaultConnection")));


            services.AddScoped<IRequestHandler<RegisterUserCommand, bool>, UserCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateUserCommand, bool>, UserCommandHandler>();
            services.AddScoped<IRequestHandler<DeleteUserCommand, bool>, UserCommandHandler>();



            services.AddScoped<INotificationHandler<DomainNotificationEvent>, DomainNotificationEventHandler>();

            services.AddScoped<UserRepository>();
            services.AddScoped<ClaimTypeRepository>();
            
            services.AddScoped<IUser, AspNetUser>();


        }
    }
}
