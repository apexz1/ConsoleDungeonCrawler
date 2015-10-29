
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
        GameData data = Application.GetData();

        data.inventory.Add(this.item, this.count);
        data.level.pickUps.Remove(this);

        Console.WriteLine("item picked up " + item.name);
    }

}