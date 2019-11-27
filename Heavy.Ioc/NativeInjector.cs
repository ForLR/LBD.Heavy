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
using Microsoft.Extensions.DependencyInjection;

namespace Heavy.Ioc
{
    public class NativeInjector
    {
        public static void RegisterService(IServiceCollection services)
        {
            //内存缓存
            services.AddMemoryCache();

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
                option.SlidingExpiration = true;
              
            });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<IEventStore, SqlEventStore>();


            services.AddScoped<IEventStoreRepository, EventStoreRepository>();
            services.AddScoped<IUserAppService, UserAppService>();
         
            services.AddScoped<IMediatorHandler, InMemoryBus>();

            services.AddSingleton<IAuthorizationHandler, EmailHandler>();
            services.AddSingleton<IAuthorizationHandler, RoleHandler>();
            services.AddSingleton<IAuthorizationHandler, ReadHandler>();
            services.AddScoped<EventStoreContext>();
            services.AddDbContext<ApplicationDbContext>(option => option.UseMySql("Server=47.101.221.220;port=3306;uid=lanbudai;pwd=123258lR.;Database=Heavy"));


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
