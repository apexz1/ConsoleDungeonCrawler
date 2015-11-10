
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Throwable : Item
{
    List<Item> behaviour;
    float range;

    public void Use()
    {
        GameData data = Application.GetData();
        int index = 0;
        float rangeStorage = data.player.Weapon.content.range;

        if (!data.combat)
        {
            data.combat = true;
        }

        data.player.EnterCombat();

        Throwable t = (Throwable)behaviour[index];
        data.player.Weapon.content.range = t.range;
        data.player.Weapon.content.range = rangeStorage;

    }
}