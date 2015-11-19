using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ItemLibrary
{
    private static ItemLibrary instance;
    //public List<Item> items = new List<Item>();
    public List<Item> generics = new List<Item>();
    public List<Item> keyItems = new List<Item>();
    public List<Weapon> weaponList = new List<Weapon>();
    public List<Armor> armorList = new List<Armor>();
    public List<Throwable> grenadeList = new List<Throwable>();
    public List<Item> uses = new List<Item>();
    //public List<List<Item>> itemLists = new List<List<Item>>();

    public void Init()
    {
        //BIG WIP

        //Generics for the pickups
        generics.Add(new Item("med_kit", "med"));
        generics.Add(new Weapon("empty_weap", "weap"));
        generics.Add(new Armor("empty_armor", "armor"));
        generics.Add(new Item("ammo_box", "ammo"));
        generics.Add(new Item("max_mustermann", "key"));
        generics.Add(new Throwable("grenade_shell", "grenade"));

        //Weapons - ALL Weapons in the game
        weaponList.Add(new Weapon("new_weap", "weap", 10, 5, 1, 5, 20, 5, "none", "none", -1));
        weaponList.Add(new Weapon("other_weap", "weap", 7, 7, 0, 3, 15, 3, "none", "none", -1));
        //Armor - ALL Armor in the game
        armorList.Add(new Armor("new_armor", "armor", 10, "none"));
        //Throwable - ALL Grenades in the game
        grenadeList.Add(new Throwable("frag_grenade", "frag", new Damage(4.0f, 8.0f)));
        //Keys 
        keyItems.Add(new Item("master_key", "key"));
        //Uses - ALL items that are used instantly when picked up and are not ammo? Maybe we should drop this one
    }

    public static ItemLibrary Get()
    {
        if (instance == null)
        {
            instance = new ItemLibrary();
            instance.Init();
        }

        return instance;
    }
}
