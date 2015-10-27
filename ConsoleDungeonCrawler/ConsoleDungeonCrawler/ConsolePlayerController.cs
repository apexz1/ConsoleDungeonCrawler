
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class ConsolePlayerController : IBaseController, IGameDataChangeListener, IGameStateChangeListener
{
    public GameData data;

    public ConsolePlayerController()
    {
        Application.Add((IGameDataChangeListener)this);
    }

    public void Execute()
    {
        char input = Console.ReadKey().KeyChar;
        switch (input)
        {
            case 'w':
                Console.WriteLine("\nw");
                data.player.Move(Direction.UP);
                break;
            case 'a':
                Console.WriteLine("\na");
                data.player.Move(Direction.LEFT);
                break;
            case 's':
                Console.WriteLine("\ns");
                data.player.Move(Direction.DOWN);
                break;
            case 'd':
                Console.WriteLine("\nd");               
                data.player.Move(Direction.RIGHT);
                break;
        }

    }

    public void OnGameDataChange(GameData data)
    {
        this.data = data;
    }

    public void OnGameStateChange()
    {
        throw new NotImplementedException();
    }
}