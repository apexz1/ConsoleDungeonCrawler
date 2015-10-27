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
            ConsolePlayerController playerController = new ConsolePlayerController();
            ConsoleView view = new ConsoleView();

            //Console.SetWindowSize(240, 84);
            MCP.controller = playerController;
            Console.WriteLine(MCP.controller.ToString());
            MCP.view = view;
            MCP.Run();
            return;
        }
    }
}
