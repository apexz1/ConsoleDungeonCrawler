
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class LevelGenerator : ILevelBuilder
{

    int pickUpCount = 5;
    Random rng = new Random();

    public LevelGenerator()
    {
    }

    public Level Generate()
    {
        Level levelGen = new Level();
        levelGen.structure = BuildStructure();
        levelGen.playerSpawnPoints = SetPlayerSpawnPoints();
        levelGen.pickupSpawnPoints = SetPickupSpawnPoints();

        for (int i = 0; i < pickUpCount; i++)
        {         
            int current = rng.Next(0, levelGen.pickupSpawnPoints.Count);
            levelGen.pickUps.Add(SpawnPickup(levelGen.pickupSpawnPoints[current]));
            levelGen.pickupSpawnPoints.RemoveAt(current);
        }

        return levelGen;
    }

    private Tile[,] BuildStructure()
    {
        Tile[,] levelGenStructure = new Tile[20, 20];

        for (int i = 0; i < levelGenStructure.GetLength(0); i++)
        {
            for (int j = 0; j < levelGenStructure.GetLength(1); j++)
            {
                levelGenStructure[i, j] = new Tile("floor", ClipType.FLOOR);
            }
        }

        return levelGenStructure;
    }

    private PickUp SpawnPickup(Vector2 pos)
    {
        PickUp pickUp = new PickUp(ItemLibrary.Get().items[rng.Next(0, ItemLibrary.Get().items.Count)], rng.Next(1,3));
        pickUp.position = pos;

        return pickUp;
    }
    
    private Actor SpawnEnemy(Vector2 pos)
    {
        Actor enemy = new Actor();
        return enemy;
    }

    private List<Vector2> SetPlayerSpawnPoints()
    {
        List<Vector2> spawns = new List<Vector2>();

        spawns.Add(new Vector2(0, 0));
        spawns.Add(new Vector2(0, 19));
        spawns.Add(new Vector2(19, 0));
        spawns.Add(new Vector2(19, 19));

        return spawns;
    }

    private List<Vector2> SetPickupSpawnPoints()
    {
        List<Vector2> spawns = new List<Vector2>();

        spawns.Add(new Vector2(5, 5));
        spawns.Add(new Vector2(5, 15));
        spawns.Add(new Vector2(15, 5));
        spawns.Add(new Vector2(15, 15));
        spawns.Add(new Vector2(5, 10));
        spawns.Add(new Vector2(10, 5));
        spawns.Add(new Vector2(10, 10));

        return spawns;
    }

    private List<Vector2> SetEnemySpawnPoints()
    {
        List<Vector2> spawns = new List<Vector2>();
        return spawns;
    }
}