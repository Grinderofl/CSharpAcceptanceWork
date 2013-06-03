using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpAcceptanceWork
{
    class Program
    {
        static void Main(string[] args)
        {
            string filename = "AppSettings.config";
            if (args.Length > 0)
                filename = args[0];
            var threader = new FibonacciThreader(filename);
            threader.Execute();
        }
    }
}
