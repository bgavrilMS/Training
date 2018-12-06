using System;
using System.Threading.Tasks;
using Microsoft.Identity.Client;

namespace NetStandard
{
    public class AuthProvider
    {
        private const string ClientID = "0615b6ca-88d4-4884-8729-b178178f7c27";
        public async Task<string> DoAuthAndGetToken()
        {
            PublicClientApplication pca = new PublicClientApplication(ClientID);
            Logger.LogCallback = Log;
            Logger.Level = LogLevel.Verbose;
            Logger.PiiLoggingEnabled = true;


            AuthenticationResult result = await pca.AcquireTokenAsync(new [] {"user.read"});
          

            return result.AccessToken;
        }


        #region Logger
        //  //await pca.AcquireTokenWithDeviceCodeAsync(new [] {"user.read"},   deviceCodeResult =>
        //    //{
        //    //    Console.WriteLine(deviceCodeResult.Message);
        //    //    return Task.FromResult(0);
        //    //});
        private static void Log(LogLevel level, string message, bool containsPii)
        {
            if (!containsPii)
            {
                Console.BackgroundColor = ConsoleColor.DarkBlue;
            }

            switch (level)
            {
                case LogLevel.Error:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case LogLevel.Warning:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case LogLevel.Verbose:
                    Console.ForegroundColor = ConsoleColor.Gray;
                    break;
                default:
                    break;
            }

            Console.WriteLine($"{level} {message}");
            Console.ResetColor();
        }
        #endregion
    }
}
