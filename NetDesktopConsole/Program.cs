using NetStandard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetDesktopConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            AuthProvider authProvider = new AuthProvider();
            string token = authProvider.DoAuthAndGetToken().GetAwaiter().GetResult();

            Console.Read();
        }
    }
}
