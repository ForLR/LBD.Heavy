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
using Heavy.Repositorys;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Heavy.Ioc
{
    public class NativeInjector
    {
        public static void RegisterService(IServiceCollection services)
        {
          
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<IEventStore, SqlEventStore>();


            services.AddScoped<IEventStoreRepository, EventStoreRepository>();
            services.AddScoped<IUserAppService, UserAppService>();
         
            services.AddScoped<IMediatorHandler, InMemoryBus>();

            services.AddSingleton<IAuthorizationHandler, EmailHandler>();
            services.AddSingleton<IAuthorizationHandler, RoleHandler>();
            services.AddScoped<EventStoreContext>();
            services.AddDbContext<ApplicationDbContext>(option => option.UseMySql("Server=47.101.221.220;port=3306;uid=lanbudai;pwd=123258lR.;Database=Heavy"));


            services.AddScoped<IRequestHandler<RegisterUserCommand, bool>, UserCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateUserCommand, bool>, UserCommandHandler>();
            services.AddScoped<IRequestHandler<DeleteUserCommand, bool>, UserCommandHandler>();



            services.AddScoped<INotificationHandler<DomainNotificationEvent>, DomainNotificationEventHandler>();

            services.AddScoped<UserRepository>();
            services.AddScoped<IUser, AspNetUser>();


            // services.AddScoped<IRequestHandler<AddUserCommand, bool>, CustomerCommandHandler>();

        }
    }
}
