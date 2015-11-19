
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class ConsoleView : IBaseView, IGameDataChangeListener, IGameStateChangeListener
{
    public static string errorMessage = "";

    public ConsoleView()
    {
        TILE_CHARS.Add("floor", '▒');
        TILE_CHARS.Add("wall", '▓');
        Application.Add((IGameDataChangeListener)this);
    }

    public GameData data;
    public bool score;
    public bool hud;
    public IConsoleRenderer currentRenderer;
    private readonly Dictionary<string, char> TILE_CHARS = new Dictionary<string, char>();
    private readonly Dictionary<string, char> ITEM_CHARS = new Dictionary<string, char>();

    public ConsolePixel[,] uiContent = new ConsolePixel[44, 79];

    public void Execute()
    {
        //Console.Clear();
        char symbol = ' ';
        ConsoleColor f = ConsoleColor.Gray;
        ConsoleColor b = ConsoleColor.Black;

        for (int i = 0; i < uiContent.GetLength(0); i++)
        {
            for (int j = 0; j < uiContent.GetLength(1); j++)
            {
                uiContent[i, j] = new ConsolePixel();
            }
        }


        //Player Health stuff
        for (int i = 0; i < data.player.maxHealth; i++)
        {

            if (i < data.player.health)
                f = ConsoleColor.Red;
            else
                f = ConsoleColor.DarkGray;

            uiContent[0, 0 + i] = new ConsolePixel('♥', f, b);
        }
        f = ConsoleColor.Gray;
        b = ConsoleColor.Black;

        //Ammo Stuff
        if (true)
        {
            string content = data.player.Weapon.content.currentammo.ToString() + "/" + data.player.Weapon.content.ammo;
            char[] label = content.ToCharArray();

            for (int i = 0; i < label.Length; i++)
            {
                //Console.WriteLine(label[i]);
                if (data.player.Weapon.content.currentammo == 0 && label[i].ToString().Equals("0"))
                {
                    Console.WriteLine(label[i]);
                    f = ConsoleColor.Red;
                }
                else f = ConsoleColor.Gray;
                uiContent[0, data.level.structure.GetLength(1) - content.Length + i] = new ConsolePixel(label[i], f, b);
            }
        }
        /*
        if (true)
        {
            //Console.WriteLine(data.player.Weapon.content.name);
            char[] label;
            string content = data.player.Weapon.content.name;
            label = content.ToCharArray();
            for (int i = 0; i < label.Length; i++)
            {
                uiContent[1, 0 + i] = new ConsolePixel(label[i], ConsoleColor.Red, ConsoleColor.Black);
            }
        }
        /**/
        //Level Stuff
        for (int i = 0; i < data.level.structure.GetLength(0); i++)
        {
            for (int j = 0; j < data.level.structure.GetLength(1); j++)
            {
                TILE_CHARS.TryGetValue(data.level.structure[i, j].terrain, out symbol);
                for (int x = 0; x < data.level.pickUps.Count; x++)
                {
                    if ((i == data.level.pickUps[x].position.x) && (j == data.level.pickUps[x].position.y))
                    {
                        symbol = 'P';
                        f = ConsoleColor.Yellow;

                        if (data.level.pickUps[x].item.type == "med")
                        {
                            symbol = '+';
                            f = ConsoleColor.Green;
                        }
                        if (data.level.pickUps[x].item.type == "ammo")
                        {
                            symbol = '‼';
                            f = ConsoleColor.White;
                        }
                        if (data.level.pickUps[x].item.type == "weap")
                        {
                            symbol = '¬';
                            f = ConsoleColor.Yellow;
                        }
                        if (data.level.pickUps[x].item.type == "armor")
                        {
                            symbol = 'A';
                            f = ConsoleColor.Blue;
                        }
                        if (data.level.pickUps[x].item.type == "grenade")
                        {
                            symbol = 'ó';
                            f = ConsoleColor.Cyan;
                        }
                        if (data.level.pickUps[x].item.type == "key")
                        {
                            symbol = '¶';
                            f = ConsoleColor.Magenta;
                        }
                    }
                }
                for (int x = 0; x < data.level.enemies.Count; x++)
                {
                    if ((i == data.level.enemies[x].position.x) && (j == data.level.enemies[x].position.y))
                    {
                        symbol = 'O';
                        f = ConsoleColor.Red;
                    }
                }
                for (int x = 0; x < data.level.trigger.Count; x++)
                {
                    if ((i == data.level.trigger[x].position.x) && (j == data.level.trigger[x].position.y))
                    {
                        symbol = '♦';
                        f = ConsoleColor.Green;
                    }
                }
                for (int x = 0; x < data.level.doors.Count; x++)
                {
                    if ((i == data.level.doors[x].position.x) && (j == data.level.doors[x].position.y))
                    {
                        if (data.level.doors[x].open == true)
                        {
                            symbol = '▀';
                        }
                        if (data.level.doors[x].open == false)
                        {
                            symbol = '■';
                        }
                        if (data.level.doors[x].type == "red") f = ConsoleColor.Red;
                        if (data.level.doors[x].type == "green") f = ConsoleColor.Green;
                        if (data.level.doors[x].type == "blue") f = ConsoleColor.Blue;
                        if (data.level.doors[x].type == "yellow") f = ConsoleColor.Yellow;
                    }
                }
                if ((i == data.player.position.x) && (j == data.player.position.y))
                {
                    //Console.WriteLine("Player Found");
                    symbol = 'O';
                    f = ConsoleColor.Black;
                    b = ConsoleColor.DarkGray;

                    if (data.player.actions <= 0)
                    {
                        f = ConsoleColor.DarkGreen;
                    }
                }
                if (data.combat)
                {
                    if ((i == data.player.selector.position.x) && (j == data.player.selector.position.y))
                    {
                        //repChar = ' ';
                        //Console.Write("selector position = " + data.player.selector.position.x + data.player.selector.position.y);
                        b = ConsoleColor.Magenta;
                    }

                }

                //?+i/?+j for the level position offset
                uiContent[1 + i, j] = new ConsolePixel(symbol, f, b);

                f = ConsoleColor.Gray;
                b = ConsoleColor.Black;
            }
        }

        #region inventory stuff
        //Inventory Stuff
        if (data.inventory != null)
        {
            char[] label;
            string content = "Inventory:";
            label = content.ToCharArray();
            for (int i = 0; i < label.Length; i++)
            {
                uiContent[1, (data.level.structure.GetLength(1) + 1) + i] = new ConsolePixel(label[i]);
            }

            if (data.inventory.content.Count > 0)
            {
                for (int i = 0; i < data.inventory.content.Count; i++)
                {
                    content = data.inventory.content[i].item.name + " " + data.inventory.content[i].count.ToString();
                    label = content.ToCharArray();
                    for (int j = 0; j < label.Length; j++)
                    {
                        if (i == data.currentItem)
                        {
                            f = ConsoleColor.Gray;
                            b = ConsoleColor.Magenta;
                        }
                        if (data.inventory.content[i].item == data.player.Weapon.content)
                        {
                            f = ConsoleColor.White;
                            b = b;
                        }
                        uiContent[i + 2, (data.level.structure.GetLength(1) + 1) + j] = new ConsolePixel(label[j], f, b);
                        f = ConsoleColor.Gray;
                        b = ConsoleColor.Black;
                    }
                    //Console.WriteLine(data.inventory.content[i].item.name + "   " + data.inventory.content[i].count);
                }
            }
            else
            {
                content = "empty inventory";
                label = content.ToCharArray();

                for (int i = 0; i < label.Length; i++)
                {
                    uiContent[1, (data.level.structure.GetLength(1) + 1) + i] = new ConsolePixel(label[i]);
                }
            }
        }
        /**/
        #endregion

        //ProtoLog
        if (true)
        {
            char[] label;
            string content = "";
            label = content.ToCharArray();

            if (data.combatlog.Count > 0)
            {
                Console.WriteLine(data.combatlog.Count);
                for (int i = 0; i < 5; i++)
                {
                    content = data.combatlog[(data.combatlog.Count - 1) - i];
                    label = content.ToCharArray();
                    for (int j = 0; j < label.Length; j++)
                    {
                        uiContent[(data.level.structure.GetLength(0) + 2) + i, 0 + j] = new ConsolePixel(label[j], f, b);
                    }
                }
            }
        }

        //RENDERS THE CURRENT UICONTENT
        for (int i = 0; i < uiContent.GetLength(0); i++)
        {
            for (int j = 0; j < uiContent.GetLength(1); j++)
            {
                Console.ForegroundColor = uiContent[i, j].foreground;
                Console.BackgroundColor = uiContent[i, j].background;
                Console.Write(uiContent[i, j].symbol);
            }
            Console.WriteLine();
        }
    }

    public void OnGameDataChange(GameData data)
    {
        this.data = data;
        Execute();
    }

    public void OnGameStateChange()
    {
        throw new NotImplementedException();
    }
}