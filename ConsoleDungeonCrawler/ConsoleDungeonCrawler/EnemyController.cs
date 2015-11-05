
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class EnemyController : IBaseController
{
    public static bool done = false;
    private Random rng = new Random();
    private readonly Dictionary<int, Direction> DIR_LIB = new Dictionary<int, Direction>(); 

    public EnemyController()
    {
        DIR_LIB.Add(0, Direction.UP);
        DIR_LIB.Add(1, Direction.DOWN);
        DIR_LIB.Add(2, Direction.LEFT);
        DIR_LIB.Add(3, Direction.RIGHT);
        DIR_LIB.Add(4, Direction.VOID);
    }

public void Execute()
    {
        GameData data = Application.GetData();
        List<Actor> enemies = data.level.enemies;
        Direction dir;

        for (int i = 0; i < enemies.Count; i++)
        {
            DIR_LIB.TryGetValue(rng.Next(0, 5), out dir);
            enemies[i].Move(dir);
        }

        End();
    }

    public void End()
    {
        done = true;
    }
}