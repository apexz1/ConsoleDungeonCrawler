using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ConsolePseudoRaycast
{
    //THIS RAYCAST ONLY WORKS FOR THIS PROJECT SPECIFICALLY; DONT PORT. UNITY'S IS BETTER ANYWAYS
    //MAYBE MAKE USUABLE BY OUTPUT STRUCT?!?!?!?!??!?!?
    public ConsolePseudoRaycast()
    {
    }

    public static bool CastRay(Vector2 a, Vector2 b)
    {
        bool hit = false;
        Vector2 distance = GetDistanceVector(a, b);
        Vector2 current = a;

        float x = distance.x;
        float y = distance.y;

        while (x != 0 || y != 0)
        {
            int count = 0;
            current = Vector2.AddVectors(a, new Vector2(Math.Abs(x), Math.Abs(y)));

            //PLEASE FIND BETTER WAY
            if (x > 0) x -= 1;
            if (x < 0) x += 1;
            if (y > 0) y -= 1;
            if (y < 0) y += 1;

            Console.WriteLine("" + distance.x + " " + distance.y + " " + current.x + " " + current.y);
            Console.ReadKey();

            if (Application.GetData().level.structure[(int)(current.x), (int)(current.y)].substance == ClipType.WALL)
            {
                if (a.x != current.x && a.y != current.y)
                {
                    hit = true;
                }
                if (a.x == current.x && Vector2.Distance(a, current) <= 1)
                {
                    hit = true;
                }
                if (a.y == current.y && Vector2.Distance(a, current) <= 1)
                {
                    hit = true;
                }
                //Console.WriteLine("WALL DETECTED!!!!; (RayCast)");
                ConsoleView.errorMessage = "Wall detected";
            }

            count++;
        }

        return hit;
    }

    private static Vector2 GetDistanceVector(Vector2 a, Vector2 b)
    {
        Vector2 dist_vec = new Vector2();

        dist_vec.x = a.x - b.x;
        dist_vec.y = a.y - b.y;

        return dist_vec;
    }
}