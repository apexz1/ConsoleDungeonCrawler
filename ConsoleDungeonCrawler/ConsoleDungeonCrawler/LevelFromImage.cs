
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;

public class LevelFromImage : ILevelBuilder {

    public LevelFromImage() {
    }

    public Level Generate()
    {
        Bitmap btm;
        Level levelGen = new Level();
        string path = "layout.bmp";

        btm = new Bitmap(new FileStream(path, FileMode.Open));

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

        //Console.ReadKey();
        levelGen.structure = levelGenStructure;
        levelGen.pickUps.Add(SpawnPickup(new Vector2(1, 2)));
        return levelGen;
     
    }

    private PickUp SpawnPickup(Vector2 pos)
    {
        PickUp pickUp = new PickUp(ItemLibrary.Get().items[0], 1); //rng.Next(1,3));
        pickUp.position = pos;

        return pickUp;
    }
}