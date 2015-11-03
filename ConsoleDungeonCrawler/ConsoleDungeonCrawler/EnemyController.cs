
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class EnemyController : IBaseController
{
    public static bool done = false;
    public EnemyController()
    {
    }

    public void Execute()
    {
        GameData data = Application.GetData();
        for (int i = 0; i < data.level.enemies.Count; i++)
        {
            data.level.enemies[i].position = Vector2.AddVectors(data.level.enemies[i].position, new Vector2(0, 1));
        }

        End();
    }

    public void End()
    {
        done = true;
    }
}