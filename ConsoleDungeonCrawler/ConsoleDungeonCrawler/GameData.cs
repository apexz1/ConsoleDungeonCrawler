using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class GameData
{
    public Actor player;
    public Inventory inventory;
    public int currentItem;
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
        this.currentItem = -1;
    }

    public void SpawnPlayer()
    {
        this.player = new Actor();
        player.health = 10;
        player.maxHealth = 10;
        player.Weapon.content = new Weapon("new_weap", "weap", 10, 5, -1, 5, 20, 5, "none", "none", -1);
        Application.GetData().inventory.Add(player.Weapon.content, 1);

        Random rng = new Random();
        this.player.position = new Vector2(16,16);


        Application.GetData().collision.Add(player);
    }
}