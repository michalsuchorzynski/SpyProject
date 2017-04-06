using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpyClientLibrary;


namespace SpyClientConsole
{
    class Program
    {
        static void Main(string[] args)
        {

            /*Control controlmanager = new Control();
            // controlmanager.SendScreenTest();
            controlmanager.GetScreenTest();*/
            TaskManager t = new TaskManager();
            t.GetAcceptablePagesFromDB();
        }
    }
}
