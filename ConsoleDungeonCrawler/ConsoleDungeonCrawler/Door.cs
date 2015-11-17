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
        if (!CheckKeyCard())
        {
            Application.GetData().combatlog.Add("Authorization failed. Unable to access door control.");
            return;
        }
        open = !open;
        if (SetClipType())
        {
            if (open) Application.GetData().combatlog.Add("Door succesfully opened.");
            if (!open) Application.GetData().combatlog.Add("Door succesfully closed.");
        }
    }

    public bool SetClipType()
    {
        bool set = false;
        GameData data = Application.GetData();

        //Console.WriteLine("ATTEMPTING TO SETCLIPTYPE");

        for (int i = 0; i < data.level.structure.GetLength(0); i++)
        {
            for (int j = 0; j < data.level.structure.GetLength(1); j++)
            {
                //Console.WriteLine(i + " " + j);
                if (this.position.x == new Vector2(i, j).x && this.position.y == new Vector2(i, j).y)
                {
                    {
                        if (open == true) data.level.structure[i, j].substance = ClipType.FLOOR;
                        if (open == false) data.level.structure[i, j].substance = ClipType.WALL;

                        set = true;
                    }
                    //Console.WriteLine("SETTING CLIPTYPE TO:" + data.level.structure[i, j].substance);
                }
            }
        }

        return set;       
    }

    public bool CheckKeyCard()
    {
        bool hasCard = false;

        for (int i = 0; i < Application.GetData().inventory.content.Count; i++)
        {
            Item current = Application.GetData().inventory.content[i].item;
            Console.WriteLine(this.type + " " + current.name);

            //HIGHLY MODDABLE SYSTEM, YES YES
            if (this.type == "red" && (current.name == "Red Keycard" || current.name == "master_key")) hasCard = true;
            if (this.type == "blue" && (current.name == "Blue Keycard" || current.name == "master_key")) hasCard = true;
            if (this.type == "green" && (current.name == "Green Keycard" || current.name == "master_key")) hasCard = true;
            if (this.type == "yellow" && (current.name == "Yellow Keycard" || current.name == "master_key")) hasCard = true;
        }

        return hasCard;
    }
}

