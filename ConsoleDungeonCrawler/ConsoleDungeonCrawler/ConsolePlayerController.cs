
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
            case 'c':
                Console.WriteLine("\nc");
                if (data.player.EnterCombat())
                {
                    CombatSwitch();
                }
                break;
            case 'q':
                Console.WriteLine("\nq");
                data.player.Weapon.content.Attack();
                break;
            case 'u':
                Console.WriteLine("\nu");
                data.player.Undo();
                break;
            case 'i':
                Console.WriteLine("\ni");
                InventorySwitch();
                break;
            case 'r':
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