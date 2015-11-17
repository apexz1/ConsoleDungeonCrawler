
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Weapon : Item
{
    public Weapon()
    {
        this.name = "base_weap";
        this.type = "none";
        this.damage = 3;
        this.range = 20;
        this.accuracy = 0.8f;
        this.ammo = 2;
        this.clipsize = -1;
        this.ammotype = "none";
        this.damagetype = "none";
        this.penetration = 0;
    }

    public Weapon(string name, string type)
    {
        this.name = name;
        this.type = type;
        this.damage = 0;
        this.range = 0;
        this.accuracy = 0;
        this.ammo = 0;
        this.clipsize = 0;
        this.ammotype = "none";
        this.damagetype = "none";
        this.penetration = 0;
    }

    public Weapon(string n, string t, int d, float r, float a, int ammo, int maxAmmo, int clip, string ammotype, string damagetype, float pen)
    {
        this.name = n;
        this.type = t;
        this.damage = d;
        this.range = r;

        if (a > 1)  a = 1;
        this.accuracy = a;

        this.ammo = ammo;
        this.currentammo = 0;
        this.maxAmmo = maxAmmo;
        this.clipsize = clip;
        this.ammotype = ammotype;
        this.damagetype = damagetype;
        this.penetration = pen;
    }

    public int damage;
    public float range;
    public float accuracy;
    public int ammo;
    public int currentammo;
    public int maxAmmo;
    public int clipsize;
    public string ammotype;
    public string damagetype;
    public float penetration;

    public void Attack()
    {
        GameData data = Application.GetData();

        if (data.player.actions <= 0)
        {
            return;
        }

        if (currentammo <= 0)
        {
            Console.WriteLine("no ammo");
            return;
        }

        for (int i = 0; i < data.level.structure.GetLength(0); i++)
        {
            for (int j = 0; j < data.level.structure.GetLength(1); j++)
            {
                if ((i == data.player.selector.position.x) && (j == data.player.selector.position.y))
                {
                    if (CheckTarget(new Vector2(i, j)) != null)
                    {
                        if (CheckAccuracy())
                        {
                            data.combatlog.Add(/*DateTime.Now.Hour + ":" + DateTime.Now.Minute + */" Target found. Dealing damage...");
                            CheckTarget(new Vector2(i, j)).TakeDamage(damage, damagetype);
                        }
                        else
                        {
                            data.combatlog.Add(/*DateTime.Now.Hour + ":" + DateTime.Now.Minute + */" Target missed. No damage dealt.");
                        }

                        data.player.actions -= 1;               
                        if (data.player.actions <= 0)
                        {
                            data.combat = false;
                        }

                        currentammo -= 1;
                    }
                }
            }
        }
    }

    public void Attack(Vector2 target)
    {
        GameData data = Application.GetData();

        if (CheckTarget(target) != null)
        {
            if (CheckAccuracy())
            {
                CheckTarget(target).TakeDamage(damage, damagetype);
            }
            else
            {
                data.combatlog.Add(/*DateTime.Now.Hour + ":" + DateTime.Now.Minute + */" enemy missed. No damage taken.");
            }

            data.player.actions -= 1;
        }
    }

    public void Reload()
    {
        if (Application.GetData().combat)
        {
            Application.GetData().combat = false;
        }

        if (ammo <= 0)
        {
            Application.GetData().combatlog.Add("No ammunition left in system. Unable to reload weapon.");
            return;
        }

        if (currentammo == clipsize)
        {
            Application.GetData().combatlog.Add("Weapon system fully loaded. Unable to reload weapon.");
            return;
        }

        if (ammo >= clipsize)
        {
            currentammo = clipsize;
            ammo -= clipsize;

            Application.GetData().combatlog.Add("Weapon system reloaded.");
        }

        if (ammo > 0 && ammo < clipsize)
        {
            currentammo = ammo;
            ammo = 0;

            Application.GetData().combatlog.Add("Weapon system reloaded. Please refill ammunition.");
        }

        Application.GetData().player.actions -= 1;
    }

    public Actor CheckTarget(Vector2 target)
    {
        GameData data = Application.GetData();
        Actor actor = new Actor();
        bool targetExists = false;

        for (int i = 0; i < data.collision.Count; i++)
        {
            if (data.collision[i].position.x == target.x && data.collision[i].position.y == target.y)
            {              
                if ((ConsolePseudoRaycast.CastRay(new Vector2(data.player.position.x, data.player.position.y), new Vector2(target.x, target.y))))
                {
                    //ConsoleView.errorMessage = "target obscured (RayCast)";
                    Console.WriteLine("target obscured (RayCast)");
                    return null;
                }
                /**/
                targetExists = true;
                actor = data.collision[i];
            }
        }

        if (targetExists)
        {
            return actor;
        }

        return null;
    }

    public bool CheckAccuracy()
    {
        Random rng = new Random();

        float rngF = (rng.Next(-50, 100));
        float acc = rngF / 100.0f;
        if (acc < 0.01f) acc = 0.01f;

        Console.WriteLine("ACCURACY CHECK " + acc + Application.GetData().player.Weapon.content.accuracy);

        if (acc <= Application.GetData().player.Weapon.content.accuracy) return true;
        else return false;
    }

}