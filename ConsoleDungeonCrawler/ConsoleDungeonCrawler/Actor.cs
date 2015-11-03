
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Actor : GameObject
{

    public Actor()
    {
        this.name = "player";
        this.position = new Vector2();
        this.selector = new GameObject();
        this.selector.position = new Vector2(0, 0);

        this.actions = maxActions;
        this.Weapon = new Slot<Weapon>();
        Weapon.content = new Weapon();
    }

    public int health;
    public int speed;
    public int maxActions = 50;
    public int actions;
    public float vision;
    public Slot<Weapon> Weapon;
    public Slot<Armor> Armour;
    private GameData data;
    public GameObject selector;

    public List<Vector2> path = new List<Vector2>();

    public void Move(Direction dir)
    {
        Vector2 pos = new Vector2();
        data = Application.GetData();

        if (actions <= 0)
        {
            return;
        }


        switch(dir)
        {
            case Direction.UP:

                //change -5/+5 to temporary range, based on what is being used for weapon ranges and stuff
                if (!(data.combat) && position.x-1 < 0)
                {
                     return;
                }
                if (data.combat && (selector.position.x - 1 < position.x - 5 || selector.position.x-1 < 0))
                {
                    return;
                }
                pos.x -= 1;
                Console.WriteLine("move up? " + position.x + " " + position.y);

                break;

            case Direction.DOWN:

                if (!(data.combat) && position.x+1 > data.level.structure.GetLength(0)-1)
                {
                    return;
                }
                if (data.combat && (selector.position.x + 1 > position.x + 5 || selector.position.x + 1 > data.level.structure.GetLength(0) - 1))
                {
                    return;
                }
                pos.x += 1;
                Console.WriteLine("move down? " + position.x + " " + position.y);

                break;

            case Direction.LEFT:

                if (!(data.combat) && position.y - 1 < 0)
                {
                    return;
                }
                if (data.combat && (selector.position.y - 1 < position.y - 5 || selector.position.y - 1 < 0))
                {
                    return;
                }
                pos.y -= 1;
                Console.WriteLine("move left? " + position.x + " " + position.y);

                break;

            case Direction.RIGHT:

                if (!(data.combat) && position.y + 1 > data.level.structure.GetLength(1) - 1)
                {
                    return;
                }
                if (data.combat && (selector.position.y + 1 > position.y + 5 || selector.position.y + 1 > data.level.structure.GetLength(1) - 1))
                {
                    return;
                }
                pos.y += 1;
                Console.WriteLine("move right? " + position.x + " " + position.y);

                break;
        }

        if (!data.combat)
        {
            path.Add(position); 
            position = new Vector2((int)(position.x + pos.x), (int)(position.y + pos.y));
            actions -= 1;
        }

        if (data.combat)
        {
            selector.position = new Vector2((int)(selector.position.x + pos.x), (int)(selector.position.y + pos.y));
        }

        for (int i = 0; i < data.level.pickUps.Count; i++)
        {
            //Console.WriteLine("move to pickup debug: " + position.x + " " + data.level.pickUps[i].position.y);
            if (position.x == data.level.pickUps[i].position.x && position.y == data.level.pickUps[i].position.y)
            {
                data.level.pickUps[i].OnPickup();
            }
        }
    }

    public bool EnterCombat()
    {
        if (actions <= 0)
        {
            return false;
        }

        selector = new GameObject();
        selector.position = new Vector2();

        selector.position = this.position;

        return true;
    }

    public void TakeDamage(int value)
    {
        data = Application.GetData();
        Console.WriteLine("DAMAGE TAKEN!");
        health -= value;

        if (this != data.player && health <= 0)
        {
            data.level.enemies.Remove(this);
        }
    }

}