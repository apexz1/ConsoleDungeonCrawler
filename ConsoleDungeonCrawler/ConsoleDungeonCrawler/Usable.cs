﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// Really bad name for what it is, Usable are items that modify your character in some way, usually weapon stats.
/// Little like Witcher Potion Prep, now that I think about it.
/// </summary>
public class Usable : Item
{
    List<Trait> behaviour = new List<Trait>();

    public Usable()
    {

    }
    public Usable(string name, string type)
    {
        this.name = name;
        this.type = type;
    }
    public Usable(string n, string t, Trait trait)
    {
        name = n;
        type = t;
        behaviour.Add(trait);
    }
    public Usable(string n, string t, List<Trait> traits)
    {
        name = n;
        type = t;
        behaviour = traits;
    }

    public void Use()
    {
        GameData data = Application.GetData();

        if (data.combat)
        {
            return;
        }

        for (int i = 0; i < behaviour.Count; i++)
        {
            data.player.AddTrait(behaviour[i]);
        }
    }

    public void AddBehaviour(Trait trait)
    {
        behaviour.Add(trait);
    }
}