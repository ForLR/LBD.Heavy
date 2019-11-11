using Heavy.Data.Context;
using Heavy.Data.EventSourcing;
using Heavy.Data.Repository.EvenSourcing;
using Heavy.Domain.Core.Bus;
using Heavy.Domain.Core.Events;
using Heavy.Identity.Auth;
using Heavy.Identity.Commands;
using MediatR;
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

            services.AddScoped<IMediatorHandler, InMemoryBus>();

            services.AddSingleton<IAuthorizationHandler, EmailHandler>();
            services.AddSingleton<IAuthorizationHandler, RoleHandler>();

            services.AddScoped<EventStoreContext>();
            services.AddScoped<IEventStoreRepository, EventStoreRepository>();


           // services.AddScoped<IRequestHandler<AddUserCommand, bool>, CustomerCommandHandler>();

        }
    }
}
