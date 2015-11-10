
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class ConsolePlayerController : IBaseController, IGameDataChangeListener, IGameStateChangeListener
{
    public GameData data;
    public static bool done = false;

    public ConsolePlayerController()
    {
        data = Application.GetData();
        done = false;
        Application.Add((IGameDataChangeListener)this);
    }

    public void Execute()
    {
        ConsoleKey input = Console.ReadKey().Key;

        switch (input)
        {
            case ConsoleKey.W:
                Console.WriteLine("\nw");
                data.player.Move(Direction.UP);
                break;
            case ConsoleKey.A:
                Console.WriteLine("\na");
                data.player.Move(Direction.LEFT);
                break;
            case ConsoleKey.S:
                Console.WriteLine("\ns");
                data.player.Move(Direction.DOWN);
                break;
            case ConsoleKey.D:
                Console.WriteLine("\nd");
                data.player.Move(Direction.RIGHT);
                break;
            case ConsoleKey.C:
                Console.WriteLine("\nc");
                if (data.player.EnterCombat())
                {
                    CombatSwitch();
                }
                break;
            case ConsoleKey.Enter:
                Console.WriteLine("\nenter");
                data.player.Weapon.content.Attack();
                break;
            case ConsoleKey.Backspace:
                Console.WriteLine("\nbackspace");
                data.player.Undo();
                break;
            case ConsoleKey.I:
                Console.WriteLine("\ni");
                InventorySwitch();
                break;
            case ConsoleKey.R:
                Console.WriteLine("\nr");
                End();
                break;
        }

        //Console.WriteLine("" + data.player.position.x + data.player.position.y + data.player.selector.position.x + data.player.selector.position.y);
    }

    private void CombatSwitch()
    {
        data.combat = !(data.combat);
    }
    private void InventorySwitch()
    {
        MasterControlProgram.SetController(new ConsoleInventoryController());
        data.currentItem = 0;
    }

    private void End()
    {
        data.player.actions = 0;
        data.player.path.Clear();
        Application.GetEnemyController().Execute();

        done = true;
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