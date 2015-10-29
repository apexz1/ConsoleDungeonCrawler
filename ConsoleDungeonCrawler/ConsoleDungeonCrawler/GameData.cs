using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class GameData
{
    public Actor player;
    public Inventory inventory;
    public Score score;
    public Level level;

    public GameData()
    {
        this.player = new Actor();
        this.inventory = new Inventory();
        this.score = new Score();
        this.level = new Level();
    }

    public void SpawnPlayer()
    {
        this.player = new Actor();
        Random rng = new Random();
        this.player.position = level.playerSpawnPoints[2];
    }




}