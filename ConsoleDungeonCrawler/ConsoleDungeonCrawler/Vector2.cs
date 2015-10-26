
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class Vector2
{
    public Vector2()
    {
        this.x = 0;
        this.y = 0;
    }

    public Vector2(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public float x;
    public float y;

    public Vector2 AddVectors(Vector2 a, Vector2 b)
    {
        Vector2 result = new Vector2();

        result.x = a.x + b.x;
        result.y = a.y + b.y;

        return result;
    }
}