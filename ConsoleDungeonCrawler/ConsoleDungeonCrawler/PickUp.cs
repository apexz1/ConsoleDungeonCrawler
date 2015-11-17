
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class PickUp : GameObject
{

    public PickUp()
    {
        this.item = new Item();
        this.count = 1;
    }

    public PickUp(Item item, int count)
    {
        this.item = item;
        this.count = count;
        this.type = item.type;
    }

    public PickUp(Item item, string type, int count)
    {
        this.item = item;
        this.type = type;
        this.count = count;
    }

    public Item item;
    public string type;
    public int count;

    public void OnPickup()
    {
        Random rng = new Random();
        bool done = false;

        GameData data = Application.GetData();

        while(!done)
        {
            this.item = ItemLibrary.Get().items[2];

            //Sequence spawn conditions
            if (item.type == "ammo")
            {
                Console.WriteLine("AMMO FOUND");                
                for (int i = 0; i < data.inventory.content.Count; i++)
                {
                    if (data.inventory.content[i].item.type == "weap")
                    {
                        Console.WriteLine("WEAPON FOUND");
                        Weapon temp = (Weapon)data.inventory.content[i].item;

                        if (temp.ammo == temp.maxAmmo)
                            continue;

                        temp.ammo += (int)(temp.clipsize * 1.5);

                        if (temp.ammo > temp.maxAmmo)
                            temp.ammo = temp.maxAmmo;

                        data.inventory.content[i].item = temp;

                        data.combatlog.Add(/*DateTime.Now.Hour + ":" + DateTime.Now.Minute + */"Ammunition crate found. Weapon ammunition restored.");
                    }
                }
            }

            else if (item.type == "med")
            {
                //Console.WriteLine("MEDKIT FOUND");
                data.combatlog.Add(/*DateTime.Now.Hour + ":" + DateTime.Now.Minute + */"Medkit found. Player health restored.");
                data.player.health = data.player.maxHealth;
            }

            else if (item.type == "weap")
            {
                if (!data.inventory.Contains(this.item))
                {
                    data.inventory.Add(this.item, this.count);
                    data.combatlog.Add(/*DateTime.Now.Hour + ":" + DateTime.Now.Minute + */ "Weaponcase found. " + this.item.name + " was added to the inventory.");
                }
                else
                {
                    data.combatlog.Add(/*DateTime.Now.Hour + ":" + DateTime.Now.Minute + */ "Empty Weaponcase found. Proceeding...");
                }
                /*
                for (int i = 0; i < data.inventory.content.Count; i++)
                {
                    if (data.inventory.content[i].item.name == this.item.name)
                    {
                        Console.WriteLine(item.name + " " + data.inventory.content[i].item.name);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("FIRST");
                        data.inventory.Add(this.item, this.count);
                        //Console.WriteLine(item.name + data.inventory.content[i].item.name);
                        //Weapon temp = (Weapon)this.item;
                        //Console.WriteLine(temp.name + " " + temp.ammo + " " + temp.maxAmmo + " " + temp.range);
                    }
                }
                /**/
            }

            else
            {
                //Console.WriteLine("SECOND");

                data.inventory.Add(this.item, this.count);
            }

            done = true;
        }

        data.level.pickUps.Remove(this);
        //Console.WriteLine("item picked up " + item.name);
    }

}