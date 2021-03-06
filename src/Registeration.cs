using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using OTP_API.Common;

namespace OTP_API
{
    public class Registration
    {
        //This is the entry gateway of the Registration logic which will be called from
        //Web server. We try to make this code independent of the external interface
        //So it receives user-name and returns a response which can be used by any entity 
        //outside
        public static string Process(string userName) {
            if ( userName == null || userName.Length == 0 )
            {
                return null;
            }

            string otp = GenerateOneTimePassword(userName);

            Storage.Set(userName, otp);

            return otp;
        }

        //Generate OTP based on given user name
        private static string GenerateOneTimePassword(string user) 
        {
            //Hash a combination of userName and a unique number (epoch seconds)
            TimeSpan t = DateTime.UtcNow - new DateTime(1970, 1, 1);
            int secondsSinceEpoch = (int)t.TotalSeconds;

            return Hash(user + secondsSinceEpoch.ToString());
        }

        //Compute SHA-256 hash of the given string and convert it to a URL safe string
        private static string Hash(string password)
        {
            using (var sha = SHA256.Create())
            {
                var computedHash = sha.ComputeHash(Encoding.ASCII.GetBytes(password));
                return BitConverter.ToString(computedHash).Replace("-","");
            }
        }
    }
}
