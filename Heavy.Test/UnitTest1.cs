using AutoMapper;
using Heavy.Application.AutoMappers;
using Heavy.Application.Interfaces;
using Heavy.Identity.Data;
using Heavy.Identity.Model;
using Heavy.Identity.Repositorys;
using Heavy.Ioc;
using Heavy.Repositorys;
using LBD.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

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

            services.AddIdentity<User, IdentityRole>(option =>
            {
                option.Password.RequiredLength = 1;
                option.Password.RequireNonAlphanumeric = false;
                option.Password.RequireLowercase = false;
                option.Password.RequireUppercase = false;
            }).AddEntityFrameworkStores<ApplicationDbContext>();


            NativeInjector.RegisterService(services);
            //services.AddIdentityCore<User>();
            services.AddMediatR(typeof(UnitTest1));
            ServiceProvider serviceProvider = services.BuildServiceProvider();
            //IUserAppService userApp = serviceProvider.GetService<IUserAppService>();
            //var user=userApp.AllViewModel().Result;

            ClaimTypeRepository claimTypeRepository= serviceProvider.GetService<ClaimTypeRepository>();
            var claim = new ClaimType { ApplicationType = ClaimTypeEnum.User, Name = "Edit" };
            claimTypeRepository.Add(claim);
            var result=claimTypeRepository.SaveChanges().Result;
            var claims = claimTypeRepository.GetById(claim.Id);
            var data = HttpHelper.Get("http://47.101.221.220:3000/top/list?idx=1");
            Assert.Equals(3,4);
        }
    } 


}
