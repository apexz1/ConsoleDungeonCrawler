using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class GameData
{
    public Actor player;
    public Inventory inventory;
    public bool combat;
    public Score score;
    public Level level;
    public List<Actor> collision = new List<Actor>();

    public GameData()
    {
        this.player = new Actor();
        this.inventory = new Inventory();
        this.score = new Score();
        this.level = new Level();
        this.combat = new bool();
    }

    public void SpawnPlayer()
    {
        this.player = new Actor();
        player.health = 10;
        player.maxHealth = 10;

        Random rng = new Random();
        this.player.position = level.playerSpawnPoints[2];


        Application.GetData().collision.Add(player);
    }
}