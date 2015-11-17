using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Door : GameObject
{
    public bool open;
    public string type;

    public Door()
    {
    }

    public Door(string name, string type, Vector2 position, bool open)
    {
        this.name = name;
        this.type = type;
        this.position = position;
        this.open = open;
    }

    public void Init()
    {
        open = false;
        SetClipType();
    }

    public void Switch()
    {
        open = !open;
        //Console.WriteLine("SWITCHED DOOR");
        SetClipType();
    }

    public void SetClipType()
    {
        GameData data = Application.GetData();

        //Console.WriteLine("ATTEMPTING TO SETCLIPTYPE");

        for (int i = 0; i < data.level.structure.GetLength(0); i++)
        {
            for (int j = 0; j < data.level.structure.GetLength(1); j++)
            {
                //Console.WriteLine(i + " " + j);
                if (this.position.x == new Vector2(i, j).x && this.position.y == new Vector2(i, j).y)
                {
                    if (open == true) data.level.structure[i, j].substance = ClipType.FLOOR;
                    if (open == false) data.level.structure[i, j].substance = ClipType.WALL;

                    //Console.WriteLine("SETTING CLIPTYPE TO:" + data.level.structure[i, j].substance);
                }
            }
        }       
    }
}

