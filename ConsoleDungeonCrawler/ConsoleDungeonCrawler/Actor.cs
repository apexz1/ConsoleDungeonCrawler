
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Actor : GameObject
{

    public Actor()
    {
        this.name = "player";
        this.position = new Vector2();
    }
    public Actor(Vector2 position)
    {
        this.name = "player";
        this.position = position;
    }

    public int health;
    public int speed;
    public int actions;
    public float vision;
    private GameData data;

    public void Move(Direction dir)
    {
        data = Application.GetData();
        switch(dir)
        {
            case Direction.UP:

                if (position.x-1 < 0)
                {
                     return;
                }
                position.x -= 1;
                Console.WriteLine("move up? " + position.x + " " + position.y);

                break;

            case Direction.DOWN:

                if (position.x+1 > data.level.structure.GetLength(0)-1)
                {
                    return;
                }
                position.x += 1;
                Console.WriteLine("move down? " + position.x + " " + position.y);

                break;

            case Direction.LEFT:

                if (position.y - 1 < 0)
                {
                    return;
                }
                position.y -= 1;
                Console.WriteLine("move left? " + position.x + " " + position.y);

                break;

            case Direction.RIGHT:

                if (position.y + 1 > data.level.structure.GetLength(1) - 1)
                {
                    return;
                }
                position.y += 1;
                Console.WriteLine("move right? " + position.x + " " + position.y);

                break;
        }

        for (int i = 0; i < data.level.pickUps.Count; i++)
        {
            //Console.WriteLine("move to pickup debug: " + position.x + " " + data.level.pickUps[i].position.y);
            if (position.x == data.level.pickUps[i].position.x && position.y == data.level.pickUps[i].position.y)
            {
                data.level.pickUps[i].OnPickup();
            }
        }
    }

    public void TakeDamage(int value)
    {
        // TODO implement here
    }

}