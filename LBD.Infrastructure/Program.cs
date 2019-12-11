using StackExchange.Redis;
using System;

namespace LBD.Infrastructure
{
    class Program
    {
        static void Main(string[] args)
        {

            var data = RedisHelper.GetInstance().StringGet("RedisTest");
            RedisHelper.GetInstance().StringSet("RedisTest",DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            Console.ReadKey();
        }
    }
}
