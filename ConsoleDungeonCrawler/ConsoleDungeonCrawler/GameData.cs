
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class GameData
{
    public Actor player;
    public Score score;
    public Level level;

    public GameData()
    {
    }

    public void SpawnPlayer()
    {
        this.player = new Actor();
        Random rng = new Random();
        this.player.position = level.playerSpawnPoints[2];
    }




}