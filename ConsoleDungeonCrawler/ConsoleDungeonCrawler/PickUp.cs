
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class PickUp : GameObject
{

    public PickUp()
    {
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
        // TODO implement here
    }

}