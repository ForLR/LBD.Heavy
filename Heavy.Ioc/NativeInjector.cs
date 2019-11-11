using Heavy.Data.Context;
using Heavy.Data.EventSourcing;
using Heavy.Data.Repository;
using Heavy.Data.Repository.EvenSourcing;
using Heavy.Domain.Core.Bus;
using Heavy.Domain.Core.Events;
using Heavy.Domain.Interfaces;
using Heavy.Identity.Auth;
using Heavy.Identity.Data;
using Heavy.Identity.Model;
using Heavy.Repositorys;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Heavy.Ioc
{
    public class NativeInjector
    {
        public static void RegisterService(IServiceCollection services)
        {
            //services.AddScoped<UserRepository>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<IEventStore, SqlEventStore>();


            services.AddScoped<IEventStoreRepository, EventStoreRepository>();

         
            services.AddScoped<IMediatorHandler, InMemoryBus>();

            services.AddSingleton<IAuthorizationHandler, EmailHandler>();
            services.AddSingleton<IAuthorizationHandler, RoleHandler>();
            services.AddScoped<EventStoreContext>();
            services.AddScoped<ApplicationDbContext>();


            services.AddScoped<UserRepository>();
            services.AddScoped<IUser, AspNetUser>();


            // services.AddScoped<IRequestHandler<AddUserCommand, bool>, CustomerCommandHandler>();

        }
    }
}
