using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace OTP_API.Common
{
    public class Storage
    {
        private static Dictionary<string,string> cache = new Dictionary<string, string>();

        public static void Set(string key, string data)
        {
            cache[key] = data;
        }

        public static void Remove(string key)
        {
            cache.remove(key);
        }

        public static string Get(string key) 
        {
            if ( !cache.ContainsKey(key) )
            {
                return null;
            }

            return cache[key];
        }

        public static void Clear() 
        {
            cache.Clear();
        }
    }
}

