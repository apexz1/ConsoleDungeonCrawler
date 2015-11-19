
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;

public class LevelFromImage : ILevelBuilder {

    public int pickUpCount = 5;
    public int enemyCount = 3;
    string path = "layout.bmp";
    Random rng = new Random();
    Bitmap btm;

    public LevelFromImage()
    {
        Init();
    }

    public LevelFromImage(string path)
    {
        this.path = path;
        Init();
    }
    
    public void Init()
    {
        btm = new Bitmap(new FileStream(path, FileMode.Open));
    }

    public Level Generate()
    {
        Level levelGen = new Level();
        Random rng = new Random();

        levelGen.structure = BuildStructure();
        levelGen.playerSpawnPoints = SetPlayerSpawnPoints();
        levelGen.pickupSpawnPoints = SetPickupSpawnPoints();
        levelGen.enemySpawnPoints = SetEnemySpawnPoints();

        /*
        for (int i = 0; i < pickUpCount; i++)
        {
            int current = rng.Next(0, levelGen.pickupSpawnPoints.Count);
            levelGen.pickUps.Add(SpawnPickup(levelGen.pickupSpawnPoints[current], rng.Next(0, ItemLibrary.Get().generics.Count)));
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
        levelGen.pickUps.Add(SpawnPickup(new Vector2(1, 2), 0));
        levelGen.pickUps.Add(SpawnPickup(new Vector2(1, 3), 1));
        levelGen.pickUps.Add(SpawnPickup(new Vector2(2, 3), 2));
        levelGen.pickUps.Add(SpawnPickup(new Vector2(3, 3), 3));
        return levelGen;
     
    }

    private Tile[,] BuildStructure()
    {
        Tile[,] levelGenStructure = new Tile[btm.Width, btm.Height];

        for (int i = 0; i < btm.Width; i++)
        {
            for (int j = 0; j < btm.Height; j++)
            {
                //Console.WriteLine(btm.GetPixel(i, j).ToArgb());
                if (btm.GetPixel(i, j).ToArgb() == Color.Black.ToArgb())
                {
                    levelGenStructure[i, j] = new Tile("wall", ClipType.WALL);
                }
                else
                {
                    levelGenStructure[i, j] = new Tile("floor", ClipType.FLOOR);
                }
            }
        }

        return levelGenStructure;
    }

    private PickUp SpawnPickup(Vector2 pos, int i)
    {
        Random rng = new Random();

        PickUp pickUp = new PickUp(ItemLibrary.Get().generics[i], 1); //rng.Next(1,3));
        pickUp.position = pos;

        return pickUp;
    }
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

        spawns.Add(new Vector2(15, 3));
        spawns.Add(new Vector2(15, 6));
        spawns.Add(new Vector2(15, 9));

        return spawns;
    }
}