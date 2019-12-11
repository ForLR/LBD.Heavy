using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace LBD.Infrastructure
{
    public class RedisHelper
    {
        private static ConnectionMultiplexer redis;
        private static readonly object _lock = new object();
        private static IDatabase db;
        static RedisHelper()
        {
            redis = ConnectionMultiplexer.Connect("47.101.221.220,password=123258lR.");
        }

        public static IDatabase GetInstance()
        {
            lock (_lock)
            {
                if (db==null)
                {
                    lock (_lock)
                    {
                        db=redis.GetDatabase();
                    }

                }
                return db;
            }
         
        }
       ~RedisHelper()
        {
            redis.Dispose();
        }


    }
}
