using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// LIBRARY CONTAINING ALL ITEMS USED IN THE GAMES, SORTED BY VARIOUS LISTS
/// COMPARABLE TO UNITY'S PREFAB SYSTEM
/// </summary>
public class ItemLibrary
{
    private static ItemLibrary instance;
    public List<Item> items = new List<Item>();
    public List<Item> generics = new List<Item>();
    public List<Item> keyList = new List<Item>();
    public List<Weapon> weaponList = new List<Weapon>();
    public List<Weapon> enemyweaponList = new List<Weapon>();
    public List<Armor> armorList = new List<Armor>();
    public List<Armor> enemyarmorList = new List<Armor>();
    public List<Throwable> grenadeList = new List<Throwable>();
    public List<Item> usableList = new List<Item>();

    //public List<List<Item>> itemLists = new List<List<Item>>();

    public void Init()
    {
        //BIG WIP

        //Generics for the pickups
        generics.Add(new Item("med_kit", "med"));
        generics.Add(new Weapon("empty_weap", "weap"));
        generics.Add(new Armor("empty_armor", "armor"));
        generics.Add(new Item("ammo_box", "ammo"));
        generics.Add(new Item("keycard", "key"));
        generics.Add(new Throwable("grenade_shell", "grenade"));
        generics.Add(new Usable("modification", "use"));

        //Gonna be used for additional pickups, like Ammo Mods maybe or other usable that arent grenades (can still be coded like them) -So much for that...
        items.Add(new Throwable("terra_former", "grenade", new TerraformImpact()));
        items.Add(new Item("master_key", "key"));
        items.Add(AmmoFacade.Get().Create("wrath_of_the_gods", damage: 3, accuracy: 1, range: 1, penetration: 1, type: "true"));
        //Weapons - ALL Weapons in the game
        //PLAYER WEAPONS         
        //new Weapon(string n, string t, int d, float r, float a, int ammo, int maxammo, int clip, string ammotype, damagetype, pen)
        weaponList.Add(new Weapon("handgun", "weap", 3, 4, 0.7f, 24, 24, 12, "9mm", "bullet", 0));
        weaponList.Add(new Weapon("assault_rifle", "weap", 3, 6, 0.8f, 30, 90, 10, "9mm", "bullet", 0));
        weaponList.Add(new Weapon("combat_shotgun", "weap", 6, 2, 0.95f, 4, 16, 4, "12-gauge", "bullet", 0));
        weaponList.Add(new Weapon("sniper_rifle", "weap", 12, 9, 0.99f, 2, 6, 1, ".50", "bullet", 0));
        weaponList.Add(new Weapon("submachine_gun", "weap", 3, 4, 0.6f, 150, 300, 60, "5.7x28mm", "bullet", 0));
        weaponList.Add(new Weapon("EHF_osc_blade", "weap", 5, 1, 1.0f, -1, -1, -1, "mechanical", "sharp", 0));
        //ENEMY WEAPONS
        enemyweaponList.Add(new Weapon("claws", "weap", 3, 1, 0.95f, -1, -1, -1, "none", "sharp", 0.3f));
        enemyweaponList.Add(new Weapon("bolter", "weap", 2, 3, 0.7f, -1, -1, -1, "raw", "bullet", 0));
        enemyweaponList.Add(new Weapon("bearpaw", "weap", 6, 1, 0.6f, -1, -1, -1, "none", "blunt", 0.1f));
        //Armor - ALL Armor in the game
        armorList.Add(new Armor("uniform", "armor", 0, "fabric"));
        armorList.Add(new Armor("hardshell_suit", "armor", 20, "plate"));
        armorList.Add(new Armor("command_suit", "armor", 30, "aramid"));
        armorList.Add(new Armor("combat_armor", "armor", 40, "hybrid"));
        armorList.Add(new Armor("strike_suit", "armor", 80, "hybrid", new Trait(-1, "equip", new ActionTrait(-1))));
        armorList.Add(new Armor("phasing_armor", "armor", 10, "molecular", new Trait(-1, "equip", new ActionTrait(1))));
        //ENEMY ARMOR
        enemyarmorList.Add(new Armor("thin_plating", "armor", 10, "plate"));
        enemyarmorList.Add(new Armor("cyber_fur", "armor", 30, "fluffy"));
        //Throwable - ALL Grenades in the game
        grenadeList.Add(new Throwable("frag_grenade", "grenade", new DamageImpact(4.0f, 8.0f)));
        grenadeList.Add(new Throwable("flashbang", "grenade", new AccuracyImpact(4.0f)));
        //Usables - ALL Character Buff Items in the game (technically all usables, but a little difficult to use the system)
        //Hollow-tips, explosive, 
        usableList.Add(AmmoFacade.Get().Create("tracer_ammo", accuracy: 1));
        usableList.Add(AmmoFacade.Get().Create("slug_shells", damage: 1, accuracy: -0.2f, range: 1, penetration: 1, type: "blunt"));
        usableList.Add(AmmoFacade.Get().Create("flechet_shells", damage: 1, penetration: 0.5f, type: "flechet"));
        usableList.Add(AmmoFacade.Get().Create("hooking_device", type: "hook"));
        //Keys 
        keyList.Add(new Item("red_keycard", "key"));
        keyList.Add(new Item("blue_keycard", "key"));
        keyList.Add(new Item("green_keycard", "key"));
        keyList.Add(new Item("yellow_keycard", "key"));
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
