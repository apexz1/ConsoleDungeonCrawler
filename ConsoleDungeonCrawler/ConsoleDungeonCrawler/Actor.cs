
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
        Weapon.content = new Weapon(ItemLibrary.Get().weaponList[0]);
        Armor.content = new Armor(ItemLibrary.Get().armorList[0]);

        XselecRange = Weapon.content.range;
    }
    /// <summary>
    /// Constructor for enemies
    /// </summary>
    /// <param name="name"></param>
    /// <param name="health"></param>
    /// <param name="weapon"></param>
    /// <param name="armor"></param>
    public Actor(string name, string type,  int health, Weapon weapon, Armor armor)
    {
        this.name = name;
        this.actions = 20000;
        this.type = type;
        this.health = health;
        this.position = new Vector2();
        this.selector = new GameObject();
        this.selector.position = new Vector2(0, 0);

        this.Weapon = new Slot<Weapon>();
        this.Armor = new Slot<Armor>();
        Weapon.content = weapon;
        Armor.content = armor;
    }
    public Actor(Actor enemy)
    {
        this.name = enemy.name;
        this.actions = 20000;
        this.type = enemy.type;
        this.health = enemy.health;
        this.position = new Vector2();
        this.selector = new GameObject();
        this.selector.position = new Vector2(0, 0);

        this.Weapon = new Slot<Weapon>();
        this.Armor = new Slot<Armor>();
        Weapon.content = enemy.Weapon.content;
        Armor.content = enemy.Armor.content;
    }

    public string type;
    public int health;
    public int maxHealth;
    public int actions;
    public int maxActions = 10000;
    public Slot<Weapon> Weapon;
    public Slot<Armor> Armor;
    public List<Trait> traits = new List<Trait>();
    private GameData data;
    public GameObject selector;
    public bool info = false;

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
            Console.WriteLine(18);
            return false;
        }

        switch (dir)
        {
            case Direction.VOID:
                Console.WriteLine(0);

                return true;

            case Direction.UP:
                //Console.WriteLine(XselecRange);
                //SMARTGIT DEMONSTRATION COMMENT
                //change -5/+5 to temporary range, based on what is being used for weapon ranges and stuff

                //------------------------------------------------------------
                if (!(data.combat) && position.x - 1 < 0)
                {
                    Console.WriteLine(1);
                    return false;
                }
                {
                    //------------------------------------------------------------
                }
                if (data.combat && (selector.position.x - 1 < position.x - XselecRange || selector.position.x - 1 < 0))
                {
                    Console.WriteLine(2);
                    return false;
                }
                //------------------------------------------------------------
                for (int i = 0; i < data.collision.Count; i++)
                {
                    if (!(data.combat) && (data.collision[i].position.x == position.x - 1 && data.collision[i].position.y == position.y))
                    {
                        Console.WriteLine(3);
                        return false;
                    }
                }
                //------------------------------------------------------------              
                if (!(data.combat) && (data.level.structure[(int)position.x - 1, (int)position.y].substance == ClipType.WALL))
                {
                    Console.WriteLine(4);
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
                    Console.WriteLine(5);
                    return false;
                }
                if (data.combat && (selector.position.x + 1 > position.x + XselecRange || selector.position.x + 1 > data.level.structure.GetLength(0) - 1))
                {
                    Console.WriteLine(6);
                    return false;
                }
                for (int i = 0; i < data.level.enemies.Count; i++)
                {
                    if (!(data.combat) && (data.collision[i].position.x == position.x + 1 && data.collision[i].position.y == position.y))
                    {
                        Console.WriteLine(7);
                        return false;
                    }
                }
                if (!(data.combat) && (data.level.structure[(int)position.x + 1, (int)position.y].substance == ClipType.WALL))
                {
                    Console.WriteLine(8);
                    return false;
                }
                pos.x += 1;
                //Console.WriteLine("move down? " + position.x + " " + position.y);

                break;

            case Direction.LEFT:

                if (!(data.combat) && position.y - 1 < 0)
                {
                    Console.WriteLine(9);
                    return false;
                }
                if (data.combat && (selector.position.y - 1 < position.y - XselecRange || selector.position.y - 1 < 0))
                {
                    Console.WriteLine(10);
                    return false;
                }
                for (int i = 0; i < data.level.enemies.Count; i++)
                {
                    if (!(data.combat) && (data.collision[i].position.y == position.y - 1 && data.collision[i].position.x == position.x))
                    {
                        Console.WriteLine(11);
                        return false;
                    }
                }
                if (!(data.combat) && (data.level.structure[(int)position.x, (int)position.y - 1].substance == ClipType.WALL))
                {
                    Console.WriteLine(12);
                    return false;
                }
                pos.y -= 1;
                //Console.WriteLine("move left? " + position.x + " " + position.y);

                break;

            case Direction.RIGHT:

                if (!(data.combat) && position.y + 1 > data.level.structure.GetLength(1) - 1)
                {
                    Console.WriteLine(13);
                    return false;
                }
                if (data.combat && (selector.position.y + 1 > position.y + XselecRange || selector.position.y + 1 > data.level.structure.GetLength(1) - 1))
                {
                    Console.WriteLine(14);
                    return false;
                }
                for (int i = 0; i < data.level.enemies.Count; i++)
                {
                    if (!(data.combat) && (data.collision[i].position.y == position.y + 1 && data.collision[i].position.x == position.x))
                    {
                        Console.WriteLine(15);
                        return false;
                    }
                }
                if (!(data.combat) && (data.level.structure[(int)position.x, (int)position.y + 1].substance == ClipType.WALL))
                {
                    Console.WriteLine(17);
                    return false;
                }
                pos.y += 1;
                //Console.WriteLine("move right? " + position.x + " " + position.y);

                break;
        }

        if (!data.combat)
        {
            path.Add(position);
            position = new Vector2((int)(position.x + pos.x), (int)(position.y + pos.y));
            actions -= 1;
            moved = true;
            Console.WriteLine("NEW POSITION = " + position.x + "," + position.y);
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
    /// <summary>
    /// Move position A towards position B
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public Direction[] DirectionTowards(Vector2 b)
    {
        Direction[] dir = new Direction[] { Direction.VOID, Direction.VOID };

        if (position.x < b.x) dir[0] = Direction.DOWN;
        if (position.x > b.x) dir[0] = Direction.UP;
        if (position.y < b.y) dir[1] = Direction.RIGHT;
        if (position.y > b.y) dir[1] = Direction.LEFT;

        return dir;
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

    public void TakeDamage(float value, string dmgtype, float pen)
    {
        data = Application.GetData();
        Armor armor = this.Armor.content;

        //DAMAGE CALC WITH DAMAGE TYPE AND ARMOR TYPE AND STUFF
        value = ApplyArmor(value, dmgtype, armor, pen);
        health -= (int)value;

        if (this != data.player)
        {
            data.combatlog.Add("...Enemy took " + (int)value + " damage");
        }
        if (this == data.player)
        {
            data.combatlog.Add("Hit. " + (int)value + " damage taken");
        }


        if (this != data.player && health <= 0)
        {
            data.level.enemies.Remove(this);
            data.collision.Remove(this);
            data.score.AddScore(10);
            Console.WriteLine(data.score.GetScore());
        }
        if (this == data.player && health <= 0)
        {
            data.collision.Remove(data.player);
            data.player = new Actor();
            data.SpawnPlayer();

            Console.WriteLine("YOU DIED");
        }
    }

    private float ApplyArmor(float value, string dmgtype, Armor armor, float pen)
    {
        float result = value;

        if (armor.armortype == "none") return value;
        if (armor.armortype == "plate")
        {
            if (dmgtype == "sharp") result = (value * 0.3f) - ((armor.value/10));
            if (dmgtype == "bullet") result = (value * 0.9f) - ((armor.value/10) * (1 - pen/2));
            if (dmgtype == "flechet")
            {
                result = (value * 0.9f) - ((armor.value / 10) * (1 - pen));
                AddTrait(2, "temp", new HeavyInjuryTrait(1));
            }

            if (dmgtype == "blunt") result = (value * 1.5f) + ((armor.value/20) * (1 - pen));
        }
        if (armor.armortype == "fluffy")
        {
            if (dmgtype == "sharp" || dmgtype == "flechet") result = (value * 3.0f) + ((armor.value) * (1 - pen));
            if (dmgtype == "bullet") result = (value) - ((armor.value/10) * (1 - pen*0.8f));
            if (dmgtype == "blunt") result = (value) + ((armor.value));
        }

        //Console.WriteLine(value + " " + dmgtype + " " + armor.armortype);
        //Console.WriteLine("ARMOR CALC DONE " + result);
        return result;
    }

    private void ApplyTraits()
    {
        //Reset();
        for (int i = 0; i < traits.Count; i++)
        {
            for (int j = 0; j < traits[i].behaviour.Count; j++)
            {
                traits[i].behaviour[j].Execute(this);
            }
        }
    }
    public void AddTrait(Trait trait)
    {
        traits.Add(trait);
        ApplyTraits();
    }
    public void AddTrait(int duration, string name, ITraitBehaviour trait)
    {
        traits.Add(new Trait(duration, name, trait));
        ApplyTraits();
    }
    public void RemoveTrait(string name)
    {
        Trait trait = null;

        for (int i = 0; i < traits.Count; i++)
        {
            if (name == traits[i].name)
            {
                trait = traits[i];
            }
        }

        if (trait == null) return;

        trait.Remove(this);
        traits.Remove(trait);
    }
    public void RemoveTrait(Trait trait)
    {
        trait.Remove(this);
        traits.Remove(trait);
    }

    public void EquipWeapon(Weapon r)
    {
        if (Application.GetData().combat)
        {
            data.combat = false;
        }

        Weapon.content = r;
    }

    public void EquipArmor(Armor a)
    {
        if (Application.GetData().combat)
        {
            data.combat = false;
        }

        Armor.content = a;
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

    /*
    public void Reset()
    {
        for (int i = 0; i < ItemLibrary.Get().weaponList.Count; i++)
        {
            if (ItemLibrary.Get().weaponList[i].name == this.Weapon.content.name)
            {
                this.Weapon.content = new Weapon(ItemLibrary.Get().weaponList[i]);
            }
        }
    }
    */

}