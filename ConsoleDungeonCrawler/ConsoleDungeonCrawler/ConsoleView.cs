
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class ConsoleView : IBaseView, IGameDataChangeListener, IGameStateChangeListener
{

    public ConsoleView()
    {
        REP_CHARS.Add("floor", 'X');
        Application.Add((IGameDataChangeListener)this);
    }

    public GameData data;
    public bool score;
    public bool hud;
    public IConsoleRenderer currentRenderer;
    private readonly Dictionary<string, char> REP_CHARS = new Dictionary<string, char>();

    public void Build()
    {
        // TODO implement here
    }

    public void BuildInvetory()
    {
        // TODO implement here
    }

    public void Execute()
    {
        //Console.Clear();
        Console.WriteLine(data.level.pickUps.Count);
        char repChar = ' ';

        for (int i = 0; i < data.level.structure.GetLength(0); i++)
        {
            for (int j = 0; j < data.level.structure.GetLength(1); j++)
            {
                REP_CHARS.TryGetValue(data.level.structure[i, j].terrain, out repChar);
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