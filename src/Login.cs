using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using OTP_API.Common;

namespace OTP_API
{
    public class Login
    {
        //The main entry gor Login checking which is called from web server
        public static string Process(string userName, string password) {
            bool success = false;

            //fail fast if inputs are invalid
            if ( userName == null || password == null || userName.Length == 0 || password.Length == 0 )
            {
                success = false;
            } 
            else 
            {
                string storedPassword = Storage.Get(userName);

                if ( storedPassword != null ) 
                {
                    success = (storedPassword == password);
                }
            }

            if ( success )
            {
                //Upon successfull login, remove the password because it is one-time
                Storage.Remove(userName);

                if ( Data.LogSuccessfulLogin ) 
                {
                    Logger.LogResult(userName, password, true);
                }

                return "OK";
            }
            else
            {
                if ( Data.LogFailedLogin ) 
                {
                    Logger.LogResult(userName, password, false);
                }

                return "FAIL";
            }
        }
    }
}

