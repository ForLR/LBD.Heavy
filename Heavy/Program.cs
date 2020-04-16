using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Autofac.Extensions.DependencyInjection;
using Heavy.Identity.Data;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;



namespace Heavy
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()//等级
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)///日志输出的最小级别
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.File(Path.Combine("logs/LogDatas", "log.txt"), rollingInterval: RollingInterval.Day)
                .CreateLogger();



            var host = CreateHostBuilder(args).Build();
            //using (var scope = host.Services.CreateScope())
            //{
            //    var services = scope.ServiceProvider;
            //    try
            //    {

            //        var context = services.GetRequiredService<ApplicationDbContext>();


            //    }
            //    catch (Exception e)
            //    {
            //        throw new Exception(e.Message);
            //    }
            //}
            host.Run();
        }
        //Microsoft.Extensions.Hosting.IHostBuilder.

        /// <summary>
        /// 泛型，通用主机    完成配置--完成Kestrel
        /// https://docs.microsoft.com/zh-cn/aspnet/core/fundamentals/host/generic-host?view=aspnetcore-3.1
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)//完成基础配置
          .ConfigureWebHostDefaults(webBuilder => //自定义主机配置
          {
             
              webBuilder.UseUrls("http://*:88").UseStartup<Startup>();
             
          })
            .UseSerilog()
            .UseServiceProviderFactory(new AutofacServiceProviderFactory());//设置工厂来替换实例  替换core默认主机


        /// <summary>
        /// web主机
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.
            CreateDefaultBuilder(args).
            UseUrls("http://*:88").
            ConfigureLogging((hostingContext, logging) =>
            {
                //logging.AddEventSourceLogger();
                logging.AddConsole();

            }).
            UseSerilog().
            UseStartup<Startup>();
    }
}
