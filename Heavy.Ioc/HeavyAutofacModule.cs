using Autofac;
using Heavy.Application.Interfaces;
using Heavy.Application.Services;
using Heavy.Data.EventSourcing;
using Heavy.Data.Repository.EvenSourcing;
using Heavy.Domain.Core.Bus;
using Heavy.Domain.Core.Events;
using Heavy.Domain.Core.Notifications;
using Heavy.Domain.Interfaces;
using Heavy.Identity.Auth;
using Heavy.Identity.CommandHandler;
using Heavy.Identity.Commands;
using Heavy.Identity.Model;
using Heavy.Identity.Repositorys;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using System.Reflection;
namespace Heavy.Ioc
{
    public  class HeavyAutofacModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder containerBuilder)
        {
            var assembly = this.GetType().GetTypeInfo().Assembly;
            var builder = new ContainerBuilder();
            var manager = new ApplicationPartManager();
            manager.ApplicationParts.Add(new AssemblyPart(assembly));
            manager.FeatureProviders.Add(new ControllerFeatureProvider());
            var feature = new ControllerFeature();
            manager.PopulateFeature(feature);

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

        }
    }
}
