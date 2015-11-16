
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
        this.accuracy = 1;
        this.ammo = 2;
        this.clipsize = -1;
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
                        CheckTarget(new Vector2(i, j)).TakeDamage(damage);
                        data.player.actions -= 1;

                        if(data.player.actions <= 0)
                        {
                            data.combat = false;
                        }

                        ammo -= 1;
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
            CheckTarget(target).TakeDamage(damage);
            data.player.actions -= 1;
        }
    }

    public void Reload()
    {
        if (ammo <= 0) return;
        if (ammo >= clipsize)
        {
            currentammo = clipsize;
            ammo -= clipsize;
        }

        if (ammo > 0 && ammo < clipsize)
        {
            currentammo = ammo;
            ammo = 0;
        }
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

}