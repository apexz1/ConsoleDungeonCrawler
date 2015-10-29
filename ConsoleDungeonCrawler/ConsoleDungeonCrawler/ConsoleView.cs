
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class ConsoleView : IBaseView, IGameDataChangeListener, IGameStateChangeListener
{

    public ConsoleView()
    {
        TILE_CHARS.Add("floor", 'X');
        Application.Add((IGameDataChangeListener)this);
    }

    public GameData data;
    public bool score;
    public bool hud;
    public IConsoleRenderer currentRenderer;
    private readonly Dictionary<string, char> TILE_CHARS = new Dictionary<string, char>();
    private readonly Dictionary<string, char> ITEM_CHARS = new Dictionary<string, char>();

    public void Execute()
    {
        //Console.Clear();
        Console.WriteLine(data.level.pickUps.Count);
        char repChar = ' ';

        for (int i = 0; i < data.level.structure.GetLength(0); i++)
        {
            for (int j = 0; j < data.level.structure.GetLength(1); j++)
            {
                TILE_CHARS.TryGetValue(data.level.structure[i, j].terrain, out repChar);
                for (int x = 0; x < data.level.pickUps.Count; x++)
                {
                    if ((i == data.level.pickUps[x].position.x) && (j == data.level.pickUps[x].position.y))
                    {
                        //Console.WriteLine("askjhbdfuisdjrpfejgz#PechConsole");
                        repChar = 'P';
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }
                }
                if ((i == data.player.position.x) && (j == data.player.position.y))
                {
                    //Console.WriteLine("Player Found");
                    repChar = 'O';
                    Console.ForegroundColor = ConsoleColor.Green;
                }

                Console.Write(repChar);
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            Console.WriteLine();
        }   

        if(data.inventory != null)
        {
            Console.WriteLine("inventory:\n");
            for (int i = 0; i < data.inventory.content.Count; i++)
            {
                Console.WriteLine(data.inventory.content[i].item.name + "   " + data.inventory.content[i].count);
            }
        }
        else { Console.WriteLine("no inventory"); }
    }

    public void OnGameDataChange(GameData data)
    {
        this.data = data;
        Execute();
    }

    public void OnGameStateChange()
    {
        throw new NotImplementedException();
    }
}