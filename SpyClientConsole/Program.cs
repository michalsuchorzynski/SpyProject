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
            /*ScreenShot newscreen = new ScreenShot();
            newscreen.generateCurrentScreen();
            */
            while (true)
            {
                TaskManager t1 = new TaskManager();
                t1.getOpenPrograms();
                
            }   
        }
    }
}
