﻿using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace OTP_API
{
    public class Server
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }

    //The class which handles API handler registrations
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {            
            //Assign two handlers for two different API calls: register a new user and login an existing user
            app.Map("/register", ProcessRegistration);
            app.Map("/login", ProcessLogin);
        }

        private static void ProcessRegistration(IApplicationBuilder app)
        {
            app.Run(async context =>
                    {
                        string userName = context.Request.Query["user"]; 

                        await context.Response.WriteAsync(Registration.Process(userName));
                    });
        }

        private static void ProcessLogin(IApplicationBuilder app)
        {
            app.Run(async context =>
                    {
                        //fetch provided user and password
                        string userName = context.Request.Query["user"]; 
                        string password = context.Request.Query["pass"]; 

                        await context.Response.WriteAsync(Login.Process(userName, password));
                    });
        }
    }
}
