
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

    public void Attack()
    {
        GameData data = Application.GetData();

        for (int i = 0; i < data.level.structure.GetLength(0); i++)
        {
            for (int j = 0; j < data.level.structure.GetLength(1); j++)
            {
                if ((i == data.player.selector.position.x) && (j == data.player.selector.position.y))
                {
                    if (CheckTarget(new Vector2(i, j)) != null)
                    {
                        CheckTarget(new Vector2(i, j)).TakeDamage(damage);
                    }
                }
            }
        }
    }

    public void Reload()
    {
        // TODO implement here
    }

    public Actor CheckTarget(Vector2 target)
    {
        GameData data = Application.GetData();
        Actor enemy = new Actor();
        bool targetExists = false;

        for (int i = 0; i < data.level.enemies.Count; i++)
        {
            if (data.level.enemies[i].position.x == target.x && data.level.enemies[i].position.y == target.y)
            {
                targetExists = true;
                enemy = data.level.enemies[i];
            }
        }

        if (targetExists)
        {
            return enemy;
        }
        return null;       
    }

}