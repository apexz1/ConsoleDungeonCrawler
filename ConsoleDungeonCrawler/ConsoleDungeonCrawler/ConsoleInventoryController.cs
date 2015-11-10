using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class ConsoleInventoryController : IBaseController
{
    GameData data = Application.GetData();


    public void Execute()
    {
        int current = data.currentItem;
        char input = Console.ReadKey().KeyChar;

        switch (input)
        {
            case 'w':
                Console.WriteLine("\nw");
                if (current <= 0)
                {
                    current = data.inventory.content.Count() - 1;
                }
                else
                {
                    current = current - 1;
                }
                break;

            case 's':
                Console.WriteLine("\ns");
                if (current >= data.inventory.content.Count() - 1)
                {
                    current = 0;
                }
                else
                {
                    current = current + 1;
                }
                break;
            case 'e':
                Console.WriteLine("\ne");
                UseItem();
                break;

            case 'i':
                Console.WriteLine("\ni");
                MasterControlProgram.SetController(new ConsolePlayerController());
                current = -1;
                break;
        }

        data.currentItem = current;
    }

    private void UseItem()
    {
        Item item = data.inventory.content[data.currentItem].item;

        if (item is Weapon)
        {
            data.player.EquipWeapon((Weapon)item);
        }
        if (item is Throwable)
        {
            Throwable t = (Throwable)item;
            t.Use();
        }
    }
}
