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
    public List<Item> keyItems = new List<Item>();
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
        generics.Add(new Item("max_mustermann", "key"));
        generics.Add(new Throwable("grenade_shell", "grenade"));

        //Gonna be used for additional pickups, like Ammo Mods maybe or other usable that arent grenades (can still be coded like them)
        items.Add(new Item());
        //Weapons - ALL Weapons in the game
        //PLAYER WEAPONS         
        //new Weapon(string n, string t, int d, float r, float a, int ammo, int maxammo, int clip, string ammotype, damagetype, pen)
        weaponList.Add(new Weapon("handgun", "weap", 3, 4, 0.7f, 24, 24, 12, "9mm", "bullet", 0));
        weaponList.Add(new Weapon("assault_rifle", "weap", 3, 6, 0.8f, 30, 90, 10, "9mm", "bullet", 0));
        //ENEMY WEAPONS
        enemyweaponList.Add(new Weapon("claws", "weap", 3, 1, 0.95f, -1, -1, -1, "none", "sharp", 0.3f));
        enemyweaponList.Add(new Weapon("bolter", "weap", 2, 3, 0.7f, -1, -1, -1, "raw", "bullet", 0));
        enemyweaponList.Add(new Weapon("bearpaw", "weap", 6, 1, 0.6f, -1, -1, -1, "none", "blunt", 0.1f));
        //Armor - ALL Armor in the game
        armorList.Add(new Armor("uniform", "armor", 0, "fabric"));
        //ENEMY ARMOR
        enemyarmorList.Add(new Armor("thin_plating", "armor", 5, "plate"));
        enemyarmorList.Add(new Armor("cyber_fur", "armor", 5, "fluffy"));
        //Throwable - ALL Grenades in the game
        grenadeList.Add(new Throwable("frag_grenade", "grenade", new Damage(4.0f, 8.0f)));
        //Usables - ALL Character Buff Items in the game
        usableList.Add(new Usable("tracer_ammo", "use", new Trait("ammo_mod", new AccuracyTrait(1))));
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
