using AutoMapper;
using Heavy.Application.AutoMappers;
using Heavy.Application.Interfaces;
using Heavy.Application.Services;
using Heavy.Domain.Core.Events;
using Heavy.Identity.Data;
using Heavy.Identity.Model;
using Heavy.Ioc;
using LBD.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Heavy.Test
{
    [TestClass]
    public class UnitTest1
    {

        public UnitTest1()
        {
          
        }
        [TestMethod]
        public void TestMethod1()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddMemoryCache();
            services.AddAutoMapper(typeof(AutoMappingConfig));

            services.AddIdentityCore<User>();
            services.AddMediatR(typeof(UnitTest1));

            services.AddDbContext<ApplicationDbContext>(option => option.UseMySql("Server=47.101.221.220;port=3306;uid=lanbudai;pwd=123258lR.;Database=Heavy"));

            services.AddIdentity<User, IdentityRole>(option =>
            {
                option.Password.RequiredLength = 1;
                option.Password.RequireNonAlphanumeric = false;
                option.Password.RequireLowercase = false;
                option.Password.RequireUppercase = false;
            }).AddEntityFrameworkStores<ApplicationDbContext>();
            NativeInjector.RegisterService(services);



            ServiceProvider serviceProvider = services.BuildServiceProvider();
            IEventStore userApp = serviceProvider.GetService<IEventStore>();
            
            //var user=userApp.AllViewModel().Result;
            var data = HttpHelper.Get("http://47.101.221.220:3000/top/list?idx=1");
            Assert.Equals(3,4);
        }
    } 


}
