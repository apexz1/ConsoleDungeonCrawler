using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDungeonCrawler
{
    class Program
    {
        static void Main(string[] args)
        {
            MasterControlProgram MCP = new MasterControlProgram();
            ConsoleMenuController menuController = new ConsoleMenuController();
            ConsoleView view = new ConsoleView();

            Console.WriteLine("\n" + MCP.ToString());
            MCP.Run();
            Console.WriteLine("\n" + menuController.ToString());
            Console.WriteLine("\n" + view.ToString());

            Console.WriteLine("\n\n\n" + "Testing build, press any key to quit");
            Console.ReadKey();
            return;
        }
    }
}
