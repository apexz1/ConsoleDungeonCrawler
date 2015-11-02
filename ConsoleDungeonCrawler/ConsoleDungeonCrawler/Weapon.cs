
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Weapon : Item
{
    public Weapon()
    {
        damage = 3;
    }

    public int damage;
    public float range;
    public float accuracy;
    public int ammo;
    public int clipsize;
    public string ammotype;
    public string damagetype;
    public float penetration;

    public void Attack(Vector2 position)
    {
        // TODO implement here
    }

    public void Reload()
    {
        // TODO implement here
    }

}