
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Level
{

    public Level()
    {
    }

    public Tile[,] structure;
    public List<PickUp> pickUps = new List<PickUp>();
    public List<Actor> enemies = new List<Actor>();
    public List<Vector2> playerSpawnPoints = new List<Vector2>();
    public List<Vector2> pickupSpawnPoints = new List<Vector2>();
    public List<Vector2> enemySpawnPoints = new List<Vector2>();

    public Tile[,] Get()
    {
        // TODO implement here
        return null;
    }
}