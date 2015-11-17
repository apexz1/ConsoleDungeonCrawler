
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Armor : Item
{

    public Armor()
    {
        value = 5;
        type = "none";
    }

    public Armor(string name, string type)
    {
        this.name = name;
        this.type = type;
        value = 5;
        armortype = "none";
    }

    public Armor(string name, string type, int value, string armortype)
    {
        this.name = name;
        this.type = type;
        this.value = value;
        this.armortype = armortype;
    }

    public int value;
    public string armortype;

}