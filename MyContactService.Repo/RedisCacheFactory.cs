using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace MyContactService.Repo
{
    public static class RedisCacheFactory
    {
        private static Lazy<ConnectionMultiplexer> lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
        {
            return ConnectionMultiplexer.Connect(ConfigurationManager.AppSettings["RedisCacheName"]);
        });

        public static IDatabase Database
        {
            get
            {
                if (lazyConnection.IsValueCreated)
                {
                    return lazyConnection.Value.GetDatabase();
                }
                else
                {
                    var connection = lazyConnection.Value;
                    if (connection != null)
                        return connection.GetDatabase();
                    else
                        return null;
                }
            }
        }
    }
}