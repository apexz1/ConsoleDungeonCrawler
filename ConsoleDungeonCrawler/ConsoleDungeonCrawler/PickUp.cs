
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
            this.item = ItemLibrary.Get().items[rng.Next(0, ItemLibrary.Get().items.Count)];

            //Sequence spawn conditions
            if (item.type == "ammo")
            {
                Console.WriteLine("AMMO FOUND");
                //Check if inventory has weapon, if not re-roll
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
                    }
                }
            }

            else if (item.type == "med")
            {
                //Console.WriteLine("MEDKIT FOUND");
                data.combatlog.Add(DateTime.Now.Hour + ":" + DateTime.Now.Minute + " Medkit found. Player health restored");
                data.player.health = data.player.maxHealth;
            }

            else if (item.type == "weap")
            {
                for (int i = 0; i < data.inventory.content.Count; i++)
                {
                    if (data.inventory.content[i].item.name == this.item.name)
                    {
                        //
                    }
                }
            }

            else
            {
                data.inventory.Add(this.item, this.count);
            }

            done = true;
        }

        data.level.pickUps.Remove(this);
        Console.WriteLine("item picked up " + item.name);
    }

}