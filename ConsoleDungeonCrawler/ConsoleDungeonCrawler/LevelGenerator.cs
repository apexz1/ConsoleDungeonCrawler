
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class LevelGenerator : ILevelBuilder
{

    int pickUpCount = 5;
    int enemyCount = 3;

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
        levelGen.enemySpawnPoints = SetEnemySpawnPoints();

        
        for (int i = 0; i < pickUpCount; i++)
        {         
            int current = rng.Next(0, levelGen.pickupSpawnPoints.Count);
            levelGen.pickUps.Add(SpawnPickup(levelGen.pickupSpawnPoints[current]));
            levelGen.pickupSpawnPoints.RemoveAt(current);
        }
        /**/

        for (int i = 0; i < enemyCount; i++)
        {
            int current = rng.Next(0, levelGen.enemySpawnPoints.Count);
            levelGen.enemies.Add(SpawnEnemy(levelGen.enemySpawnPoints[current], 1));
            levelGen.enemySpawnPoints.RemoveAt(current);
        }
        /**/

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

        levelGenStructure[15, 15] = new Tile("wall", ClipType.WALL);

        return levelGenStructure;
    }
    
    private PickUp SpawnPickup(Vector2 pos)
    {
        PickUp pickUp = new PickUp(ItemLibrary.Get().items[rng.Next(0, ItemLibrary.Get().items.Count)], 1); //rng.Next(1,3));
        pickUp.position = pos;

        return pickUp;
    }
    /**/
    
    private Actor SpawnEnemy(Vector2 pos, int h)
    {
        Actor enemy = new Actor();

        enemy.position = pos;
        enemy.health = h;
        enemy.Weapon.content = new Weapon();

        Application.GetData().collision.Add(enemy);

        return enemy;
    }
    /**/
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

        /*
        spawns.Add(new Vector2(5, 5));
        spawns.Add(new Vector2(5, 15));
        spawns.Add(new Vector2(15, 5));
        spawns.Add(new Vector2(15, 15));
        spawns.Add(new Vector2(5, 10));
        spawns.Add(new Vector2(10, 5));
        spawns.Add(new Vector2(10, 10));
        /**/

        spawns.Add(new Vector2(18, 2));
        spawns.Add(new Vector2(18, 1));
        spawns.Add(new Vector2(19, 1));
        spawns.Add(new Vector2(19, 2));
        spawns.Add(new Vector2(17, 2));
        spawns.Add(new Vector2(18, 3));
        spawns.Add(new Vector2(17, 3));

        return spawns;
    }

    private List<Vector2> SetEnemySpawnPoints()
    {
        List<Vector2> spawns = new List<Vector2>();

        spawns.Add(new Vector2(15, 3));
        spawns.Add(new Vector2(15, 6));
        spawns.Add(new Vector2(15, 9));

        return spawns;
    }
}