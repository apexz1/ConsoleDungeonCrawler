
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
        this.Armor = new Slot<Armor>();
        Weapon.content = new Weapon();
        Armor.content = new Armor();

        XselecRange = Weapon.content.range;
    }
    /// <summary>
    /// Constructor for enemies
    /// </summary>
    /// <param name="name"></param>
    /// <param name="health"></param>
    /// <param name="weapon"></param>
    /// <param name="armor"></param>
    public Actor(string name, int health, Weapon weapon, Armor armor)
    {
        this.name = name;
        this.health = health;
        this.position = new Vector2();
        this.selector = new GameObject();
        this.selector.position = new Vector2(0, 0);

        this.Weapon = new Slot<Weapon>();
        this.Armor = new Slot<Armor>();
        Weapon.content = weapon;
        Armor.content = armor;
    }

    public string type;
    public int health;
    public int maxHealth;
    public int actions;
    public int maxActions = 10000;
    public Slot<Weapon> Weapon;
    public Slot<Armor> Armor;
    private GameData data;
    public GameObject selector;

    public bool usingItem = false;
    public float XselecRange;

    public List<Vector2> path = new List<Vector2>();

    public bool Move(Direction dir)
    {
        Vector2 pos = new Vector2();
        data = Application.GetData();
        bool moved = false;
        XselecRange = Weapon.content.range;

        if (actions <= 0)
        {
            return false;
        }

        switch (dir)
        {
            case Direction.VOID:
                return true;

            case Direction.UP:
                //Console.WriteLine(XselecRange);
                //SMARTGIT DEMONSTRATION COMMENT
                //change -5/+5 to temporary range, based on what is being used for weapon ranges and stuff

                //------------------------------------------------------------
                if (!(data.combat) && position.x - 1 < 0)
                {
                    return false;
                }
                {
                    //------------------------------------------------------------
                }
                if (data.combat && (selector.position.x - 1 < position.x - XselecRange || selector.position.x - 1 < 0))
                {
                    return false;
                }
                //------------------------------------------------------------
                for (int i = 0; i < data.collision.Count; i++)
                {
                    if (!(data.combat) && (data.collision[i].position.x == position.x - 1 && data.collision[i].position.y == position.y))
                    {
                        return false;
                    }
                }
                //------------------------------------------------------------              
                if (!(data.combat) && (data.level.structure[(int)position.x - 1, (int)position.y].substance == ClipType.WALL))
                {
                    return false;
                }

                //------------------------------------------------------------
                pos.x -= 1;
                Console.WriteLine("move up? " + position.x + " " + position.y);

                break;
            //------------------------------------------------------------


            case Direction.DOWN:

                if (!(data.combat) && position.x + 1 > data.level.structure.GetLength(0) - 1)
                {
                    return false;
                }
                if (data.combat && (selector.position.x + 1 > position.x + XselecRange || selector.position.x + 1 > data.level.structure.GetLength(0) - 1))
                {
                    return false;
                }
                for (int i = 0; i < data.level.enemies.Count; i++)
                {
                    if (!(data.combat) && (data.collision[i].position.x == position.x + 1 && data.collision[i].position.y == position.y))
                    {
                        return false;
                    }
                }
                if (!(data.combat) && (data.level.structure[(int)position.x + 1, (int)position.y].substance == ClipType.WALL))
                {
                    return false;
                }
                pos.x += 1;
                Console.WriteLine("move down? " + position.x + " " + position.y);

                break;

            case Direction.LEFT:

                if (!(data.combat) && position.y - 1 < 0)
                {
                    return false;
                }
                if (data.combat && (selector.position.y - 1 < position.y - XselecRange || selector.position.y - 1 < 0))
                {
                    return false;
                }
                for (int i = 0; i < data.level.enemies.Count; i++)
                {
                    if (!(data.combat) && (data.collision[i].position.y == position.y - 1 && data.collision[i].position.x == position.x))
                    {
                        return false;
                    }
                }
                if (!(data.combat) && (data.level.structure[(int)position.x, (int)position.y - 1].substance == ClipType.WALL))
                {
                    return false;
                }
                pos.y -= 1;
                Console.WriteLine("move left? " + position.x + " " + position.y);

                break;

            case Direction.RIGHT:

                if (!(data.combat) && position.y + 1 > data.level.structure.GetLength(1) - 1)
                {
                    return false;
                }
                if (data.combat && (selector.position.y + 1 > position.y + XselecRange || selector.position.y + 1 > data.level.structure.GetLength(1) - 1))
                {
                    return false;
                }
                for (int i = 0; i < data.level.enemies.Count; i++)
                {
                    if (!(data.combat) && (data.collision[i].position.y == position.y + 1 && data.collision[i].position.x == position.x))
                    {
                        return false;
                    }
                }
                if (!(data.combat) && (data.level.structure[(int)position.x, (int)position.y + 1].substance == ClipType.WALL))
                {
                    return false;
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
            moved = true;
            //Console.WriteLine("PLAYER POSITION = " + position.x + "," + position.y);
        }

        if (data.combat)
        {
            selector.position = new Vector2((int)(selector.position.x + pos.x), (int)(selector.position.y + pos.y));
            moved = true;
        }

        for (int i = 0; i < data.level.pickUps.Count; i++)
        {
            //Console.WriteLine("move to pickup debug: " + position.x + " " + data.level.pickUps[i].position.y);
            if (position.x == data.level.pickUps[i].position.x && position.y == data.level.pickUps[i].position.y)
            {
                if (this == data.player)
                {
                    data.level.pickUps[i].OnPickup();
                }
            }
        }

        return moved;
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

    public void TakeDamage(int value, string dmgtype)
    {
        data = Application.GetData();

        //DAMAGE CALC WITH DAMAGE TYPE AND ARMOR TYPE AND STUFF

        health -= value;

        if (this != data.player)
        {
            data.combatlog.Add("...Enemy took" + value + " damage");
        }
        if (this == data.player)
        {
            data.combatlog.Add("Hit." + value + " damage taken");
        }


        if (this != data.player && health <= 0)
        {
            data.level.enemies.Remove(this);
        }
        if (this == data.player && health <= 0)
        {
            data.collision.Remove(data.player);
            data.player = new Actor();
            data.SpawnPlayer();

            Console.WriteLine("YOU DIED");
        }
    }

    public void EquipWeapon(Weapon r)
    {
        if (data.combat)
        {
            data.combat = false;
        }

        Weapon.content = r;
    }


    public void Undo()
    {
        if (actions == maxActions)
        {
            return;
        }
        if (data.combat)
        {
            return;
        }

        position = path[path.Count - 1];
        path.RemoveAt(path.Count - 1);
        actions += 1;
    }

}