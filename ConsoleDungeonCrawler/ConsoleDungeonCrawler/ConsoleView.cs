
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
    private Vector2 loff = new Vector2(1, 0);

    public ConsolePixel[,] uiContent = new ConsolePixel[44, 55];

    public void Execute()
    {
        Console.WriteLine(Application.GetData().level.enemies.Count);
        Console.WriteLine(Application.GetData().level.pickUps.Count);
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
        /**/

        //Player Actions stuff
        int debug = 5;  // player.maxActions
        int debug2 = 2; // player.Actions
        for (int i = 0; i < debug; i++)
        {
            if (i < debug2)
                f = ConsoleColor.White;
            else
                f = ConsoleColor.DarkGray;

            uiContent[0, data.player.maxHealth + 1 + i] = new ConsolePixel('>', f, b);
        }
        f = ConsoleColor.Gray;
        b = ConsoleColor.Black;
        /**/
               
        //Score stuff
        if (true)
        {
            string content = data.score.GetScore().ToString();
            char[] label = content.ToCharArray();

            for (int i = 0; i < label.Length; i++)
            {
                f = ConsoleColor.White;
                uiContent[0, uiContent.GetLength(1) - content.Length + i] = new ConsolePixel(label[i], f, b);
            }
        }
        f = ConsoleColor.Gray;
        b = ConsoleColor.Black;
        /**/
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
                    //Console.WriteLine(label[i]);
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

        //Geography Render
        for (int i = 0; i < data.level.structure.GetLength(0); i++)
        {
            for (int j = 0; j < data.level.structure.GetLength(1); j++)
            {
                //REALLY SIMPLE METHOD, CAN PROBABLY DO BETTER
                /*
                if (Vector2.Distance(new Vector2(i, j), data.player.position) > 5)
                    continue;
                    */
                TILE_CHARS.TryGetValue(data.level.structure[i, j].terrain, out symbol);
                //?+i/?+j for the level position offset
                uiContent[i + (int)loff.x, j] = new ConsolePixel(symbol, f, b);

                f = ConsoleColor.Gray;
                b = ConsoleColor.Black;
            }
        }
        /**/

        //Doors Render
        for (int x = 0; x < data.level.doors.Count; x++)
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
            uiContent[(int)data.level.doors[x].position.x + (int)loff.x, (int)data.level.doors[x].position.y] = new ConsolePixel(symbol, f, b);
        }
        f = ConsoleColor.Gray;
        b = ConsoleColor.Black;
        /**/

        //Pickup Render
        for (int x = 0; x < data.level.pickUps.Count; x++)
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
            uiContent[(int)data.level.pickUps[x].position.x + (int)loff.x, (int)data.level.pickUps[x].position.y] = new ConsolePixel(symbol, f, b);
        }
        f = ConsoleColor.Gray;
        b = ConsoleColor.Black;
        /**/

        //Level End Trigger Render
        for (int x = 0; x < data.level.trigger.Count; x++)
        {
            symbol = '♦';
            f = ConsoleColor.Green;
            uiContent[(int)data.level.trigger[x].position.x + (int)loff.x, (int)data.level.trigger[x].position.y] = new ConsolePixel(symbol, f, b);
        }
        f = ConsoleColor.Gray;
        b = ConsoleColor.Black;
        /**/

        //Enemies Render
        for (int i = 0; i < data.level.enemies.Count; i++)
        {
            Actor enemy = data.level.enemies[i];
            symbol = 'O';
            f = ConsoleColor.Red;
            //Console.WriteLine(data.level.enemies[i].name);
            if (data.level.enemies[i].name == "e_basemelee")
            {
                //Console.WriteLine(i);
                symbol = 'M';
                f = ConsoleColor.Red;
            }
            if (data.level.enemies[i].name == "e_baseranged")
            {
                symbol = 'R';
                f = ConsoleColor.Red;
            }
            if (data.level.enemies[i].name == "e_cyberbear")
            {
                symbol = 'B';
                f = ConsoleColor.Red;
            }

            uiContent[(int)enemy.position.x + (int)loff.x, (int)enemy.position.y] = new ConsolePixel(symbol, f, b);
        }
        f = ConsoleColor.Gray;
        b = ConsoleColor.Black;
        /**/

        //Player Render
        Actor player = data.player;
        symbol = 'O';
        f = ConsoleColor.Black;
        b = ConsoleColor.DarkGray;

        if (data.player.actions <= 0)
        {
            f = ConsoleColor.DarkGreen;
        }

        uiContent[(int)player.position.x + (int)loff.x, (int)player.position.y] = new ConsolePixel(symbol, f, b);
        f = ConsoleColor.Gray;
        b = ConsoleColor.Black;
        /**/

        //Selector Render - (Not anymore) Bugged
        if (data.combat)
        {
            GameObject selector = data.player.selector;
            symbol = uiContent[(int)selector.position.x + (int)loff.x, (int)selector.position.y].symbol;
            f = ConsoleColor.White;
            b = ConsoleColor.Magenta;
            uiContent[(int)selector.position.x + (int)loff.x, (int)selector.position.y] = new ConsolePixel(symbol, f, b);
            f = ConsoleColor.Gray;
            b = ConsoleColor.Black;
        }
        /**/

        #region inventory stuff
        //Inventory Stuff
        if (!data.inv)
        {
            List<string> charactersheet = new List<string>()
            {
                "//////////////////////",
                "////SYSTEM STATUS/////",
                "//",
                "//W: " + data.player.Weapon.content.name,
                "//A: " + data.player.Armor.content.name,
                "//",
                "//DMG " + data.player.Weapon.content.damage,
                "//ACC " + data.player.Weapon.content.accuracy,
                "//RANGE " + data.player.Weapon.content.range,
                "//",
                "//ARMOR: " + data.player.Armor.content.value + " points of ",
                "//" + data.player.Armor.content.armortype + " armor",
                "//",
                "//////////////////////",
                "//////////////////////",
            };

            if (true)
            {
                char[] label;
                string content = "";
                label = content.ToCharArray();

                for (int i = 0; i < charactersheet.Count; i++)
                {
                    content = charactersheet[i];
                    label = content.ToCharArray();
                    for (int j = 0; j < label.Length; j++)
                    {
                        uiContent[i + 1, (data.level.structure.GetLength(1) + 1) + j] = new ConsolePixel(label[j], f, b);
                    }
                }
            }
        }

        if (data.inventory != null && data.inv)
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
                    content = data.inventory.content[i].item.name;

                    if (data.inventory.content[i].count > 1)
                    {
                        content = data.inventory.content[i].item.name + " " + data.inventory.content[i].count.ToString();
                    }

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
                        if (data.inventory.content[i].item == data.player.Armor.content)
                        {
                            f = ConsoleColor.White;
                            b = b;
                        }
                        uiContent[i + 3, (data.level.structure.GetLength(1) + 1) + j] = new ConsolePixel(label[j], f, b);
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

        //InfoLog
        if (true)
        {
            List<string> infoLog = new List<string>();

            if (data.inv && data.currentItem < data.inventory.content.Count)
            {
                Console.WriteLine("item: " + data.currentItem);

                //data.currentItem = 0;
                ItemWrapper current = data.inventory.content[data.currentItem];

                infoLog.Add("//////////////////////");
                infoLog.Add("///ITEM INFORMATION///");
                infoLog.Add("");
                infoLog.Add(current.item.name);

                if (current.item.type == "weap")
                {
                    Weapon temp = (Weapon)current.item;
                    infoLog.Add("DAMAGE:    " + temp.damage.ToString());
                    infoLog.Add("DMGTYPE:   " + temp.damagetype.ToString());
                    infoLog.Add("AMMO:      " + temp.ammotype.ToString());
                    infoLog.Add("RANGE      " + temp.range.ToString());
                    infoLog.Add("ACCURACY   " + temp.accuracy.ToString());
                }

                if (current.item.type == "armor")
                {
                    Armor temp = (Armor)current.item;
                    infoLog.Add("ARMOR:     " + temp.value.ToString());
                    infoLog.Add("ARMTYPE:   " + temp.armortype.ToString());
                }
            }

            if (data.combat)
            {
                for (int i = 0; i < data.level.enemies.Count; i++)
                {
                    if (data.level.enemies[i].position.x == data.player.selector.position.x && data.level.enemies[i].position.y == data.player.selector.position.y)
                    {
                        if (data.level.enemies[i].info == true)
                        {
                            infoLog.Add("//////////////////////");
                            infoLog.Add("//COMBAT INFORMATION//");
                            infoLog.Add("");
                            infoLog.Add("TARGET: " + data.level.enemies[i].name);
                            infoLog.Add("");
                            infoLog.Add("WEAPON:    " + data.level.enemies[i].Weapon.content.name);
                            infoLog.Add("DAMAGE:    " + data.level.enemies[i].Weapon.content.damage);
                            infoLog.Add("DMGTYPE:   " + data.level.enemies[i].Weapon.content.damagetype);
                            infoLog.Add("RANGE:     " + data.level.enemies[i].Weapon.content.range);
                            infoLog.Add("ACCURACY:  " + data.level.enemies[i].Weapon.content.accuracy);
                            infoLog.Add("ARMOR:     " + data.level.enemies[i].Armor.content.value);
                            infoLog.Add("ARMTYPE:   " + data.level.enemies[i].Armor.content.armortype);
                        }
                        else
                        {
                            infoLog.Add("//////////////////////");
                            infoLog.Add("//COMBAT INFORMATION//");
                            infoLog.Add("");
                            infoLog.Add("UNKNOWN SPECIMEN");
                        }
                    }
                }
            }

            char[] label;
            string content = "";
            label = content.ToCharArray();

            if (infoLog.Count > 0)
            {
                for (int i = 0; i < infoLog.Count; i++)
                {
                    content = infoLog[i];
                    label = content.ToCharArray();
                    for (int j = 0; j < label.Length; j++)
                    {
                        uiContent[i + 18, (data.level.structure.GetLength(1) + 1) + j] = new ConsolePixel(label[j], f, b);
                    }
                }
            }
        }

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