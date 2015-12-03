using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class GameData
{
    public Actor player;
    public Inventory inventory;
    public bool inv = false;
    public int currentItem;
    public int currentMenu;
    public bool combat;
    public Score score;
    public Level level;
    public List<Actor> collision = new List<Actor>();
    public List<string> combatlog = new List<string>()
    {
        "|============|",
        "|////////////|",
        "|//PR0T0L0G//|",
        "|////////////|",
        "|============|"
    };

    public GameData()
    {
        this.player = new Actor();
        this.inventory = new Inventory();
        this.score = new Score();
        this.level = new Level();
        this.combat = new bool();
        this.score = new Score();
        this.currentItem = -1;
    }

    public void SpawnPlayer()
    {
        player = new Actor();
        player.health = 10;
        player.maxHealth = 10;
        Application.GetData().inventory.Add(player.Weapon.content, 1);
        Application.GetData().inventory.Add(player.Armor.content, 1);

        /*
        Application.GetData().inventory.Add(ItemLibrary.Get().grenadeList[1], 1);
        Application.GetData().inventory.Add(ItemLibrary.Get().usableList[2], 1);
        Application.GetData().inventory.Add(ItemLibrary.Get().grenadeList[1], 1);
        Application.GetData().inventory.Add(ItemLibrary.Get().usableList[2], 1);
        Application.GetData().inventory.Add(ItemLibrary.Get().grenadeList[0], 1);
        Application.GetData().inventory.Add(ItemLibrary.Get().usableList[0], 1);
        Application.GetData().inventory.Add(ItemLibrary.Get().usableList[1], 1);
        Application.GetData().inventory.Add(ItemLibrary.Get().weaponList[1], 1);
        Application.GetData().inventory.Add(ItemLibrary.Get().weaponList[2], 1);
        //player.AddTrait(2, "temp", new HeavyInjuryTrait(5));
        /**/

        Random rng = new Random();
        player.position = level.playerSpawnPoints[rng.Next(0, 4)];


        Application.GetData().collision.Add(player);
    }
}