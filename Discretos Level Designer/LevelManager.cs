using Discretos_Level_Designer.GUI;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Enumeration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Discretos_Level_Designer
{
    public static class LevelManager
    {


        public static string path = "Levels/";
        public static string ext = ".lvl";

        public static List<LevelDataSet> Levels = new List<LevelDataSet>();

        public static float completion = 0;
        public static bool isLoadedLevel = false;

        private static string name = "";

        public static void SaveAsLevel(string _name)
        {
            name = _name;
            ThreadPool.QueueUserWorkItem(new WaitCallback(SaveAsLevel), 1);
        }

        private static void SaveAsLevel(object threadContext)
        {

            completion = 0;
            LoadingGUI.GUI.SetValue(completion);
            LoadingGUI.GUI.isActive = true;

            Directory.CreateDirectory(path);

            name = CheckIfFileExist(name);
            string fileName = path + name + ext;

            FileStream stream = File.OpenWrite(fileName);
            StreamWriter file = new StreamWriter(stream);

            file.WriteLine("version : 2.0");
            file.WriteLine("");

            file.WriteLine("-----------------");
            file.WriteLine("");

            int width = Handler.tiles.GetLength(0);
            int height = Handler.tiles.GetLength(1);

            for (int j = 0; j < height; j++)
            {
                for (int i = 0; i < width; i++)
                {
                    file.Write((int)Handler.tiles[i, j].ID + ",");
                    completion += 1.0f * 33.3f / (width * height);
                    LoadingGUI.GUI.SetValue(completion);
                }

                file.Write("\n");
            }

            file.WriteLine("");
            file.WriteLine("-----------------");
            file.WriteLine("");

            for (int j = 0; j < height; j++)
            {
                for (int i = 0; i < width; i++)
                {
                    file.Write((int)Handler.walls[i, j].ID + ",");
                    completion += 1.0f * 33.3f / (width * height);
                    LoadingGUI.GUI.SetValue(completion);
                }

                file.Write("\n");
            }

            file.WriteLine("");
            file.WriteLine("-----------------");
            file.WriteLine("");

            for (int j = 0; j < height; j++)
            {
                for (int i = 0; i < width; i++)
                {
                    file.Write((int)Handler.walls[i, j].variante + ",");
                    completion += 1.0f * 33.3f / (width * height);
                    LoadingGUI.GUI.SetValue(completion);
                }

                file.Write("\n");
            }

            file.WriteLine("");
            file.WriteLine("-----------------");
            file.WriteLine("");


            file.Close();
            stream.Close();

            Console.WriteLine("Level saved : " + name);

            completion = 100;
            LoadingGUI.GUI.SetValue(completion);
            LoadingGUI.GUI.isActive = false;

            isLoadedLevel = true;

        }

        public static void SaveLevel(string _name)
        {
            name = _name;
            ThreadPool.QueueUserWorkItem(new WaitCallback(SaveLevel), 1);
        }

        public static void SaveLevel(object threadContext)
        {

            completion = 0;
            LoadingGUI.GUI.SetValue(completion);
            LoadingGUI.GUI.isActive = true;

            Directory.CreateDirectory(path);

            string fileName = path + name + ext;

            FileStream stream = File.OpenWrite(fileName);
            StreamWriter file = new StreamWriter(stream);

            file.WriteLine("version : 2.0");
            file.WriteLine("");

            file.WriteLine("-----------------");
            file.WriteLine("");

            int width = Handler.tiles.GetLength(0);
            int height = Handler.tiles.GetLength(1);

            for (int j = 0; j < height; j++)
            {
                for (int i = 0; i < width; i++)
                {
                    file.Write((int)Handler.tiles[i, j].ID + ",");
                    completion += 1.0f * 33.3f / (width * height);
                    LoadingGUI.GUI.SetValue(completion);
                }

                file.Write("\n");
            }

            file.WriteLine("");
            file.WriteLine("-----------------");
            file.WriteLine("");

            for (int j = 0; j < height; j++)
            {
                for (int i = 0; i < width; i++)
                {
                    file.Write((int)Handler.walls[i, j].ID + ",");
                    completion += 1.0f * 33.3f / (width * height);
                    LoadingGUI.GUI.SetValue(completion);
                }

                file.Write("\n");
            }

            file.WriteLine("");
            file.WriteLine("-----------------");
            file.WriteLine("");

            for (int j = 0; j < height; j++)
            {
                for (int i = 0; i < width; i++)
                {
                    file.Write((int)Handler.walls[i, j].variante + ",");
                    completion += 1.0f * 33.3f / (width * height);
                    LoadingGUI.GUI.SetValue(completion);
                }

                file.Write("\n");
            }

            file.WriteLine("");
            file.WriteLine("-----------------");
            file.WriteLine("");


            file.Close();
            stream.Close();

            Console.WriteLine("Level saved : " + name);

            completion = 100;
            LoadingGUI.GUI.SetValue(completion);
            LoadingGUI.GUI.isActive = false;


        }

        public static void LoadLevel(string _name)
        {
            name = _name;
            ThreadPool.QueueUserWorkItem(new WaitCallback(LoadLevel), 1);
        }


        private static void LoadLevel(object threadContext)
        {
            string fileName = path + name + ext;
            completion = 0;
            LoadingGUI.GUI.SetValue(completion);
            LoadingGUI.GUI.isActive = true;

            if (!File.Exists(fileName))
            {
                Directory.CreateDirectory(path);
                Handler.tiles = new EditorTile[100, 50];
                Handler.InitEditorGrid();
                Console.WriteLine("Level \"" + name + "\" not exist !");
                goto L_end;
            }


            FileStream stream = File.Open(fileName, FileMode.Open);
            StreamReader file = new StreamReader(stream);

            string version = file.ReadLine();
            Console.WriteLine(version);

            file.ReadLine(); file.ReadLine(); file.ReadLine();


            string line;
            List<string> lines = new List<string>();

            while (true)
            {
                line = file.ReadLine();
                if (line == "") break;
                lines.Add(line);
            }

            int width = lines[0].Split(',').Length - 1;
            int height = lines.Count;

            EditorTile[,] level = new EditorTile[width, height];
            Wall[,] walls = new Wall[width, height];

            for (int l = 0; l < height; l++)
            {

                string[] tiles = lines[l].Split(',');

                for (int i = 0; i < tiles.Length - 1; i++)
                {

                    level[i, l] = new EditorTile(new Vector2(i * 16, l * 16), (EditorTile.BlockID)int.Parse(tiles[i]));
                    completion += 1.0f * 50.0f / (width * height);
                    LoadingGUI.GUI.SetValue(completion);
                }

            }

            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                {
                    level[x, y].InitImg(level);
                    completion += 1.0f * 50.0f / (width * height);
                    LoadingGUI.GUI.SetValue(completion);
                }

            if (version == "version : 1.0")
            {
                for (int i = 0; i < walls.GetLength(0); i++)
                    for (int j = 0; j < walls.GetLength(1); j++)
                        walls[i, j] = new Wall(new Vector2(i * 16, j * 16), Wall.WallID.none, 0);

                goto L_endStream;
            }


            file.ReadLine(); // 2e -----------
            file.ReadLine(); // space




            string line1;
            List<string> lines1 = new List<string>();

            while (true)
            {
                line1 = file.ReadLine();
                if (line1 == "") break;
                lines1.Add(line1);
            }

            file.ReadLine(); // 3e -----------
            file.ReadLine(); // space

            string line2;
            List<string> lines2 = new List<string>();

            while (true)
            {
                line2 = file.ReadLine();
                if (line2 == "") break;
                lines2.Add(line2);
            }

            for (int l = 0; l < height; l++)
            {

                string[] wallIDs = lines1[l].Split(',');
                string[] wallVariantes = lines2[l].Split(',');

                for (int i = 0; i < wallIDs.Length - 1; i++)
                {

                    walls[i, l] = new Wall(new Vector2(i * 16, l * 16), (Wall.WallID)int.Parse(wallIDs[i]), int.Parse(wallVariantes[i]));
                    completion += 1.0f * 50.0f / (width * height);
                    LoadingGUI.GUI.SetValue(completion);
                }

            }


        L_endStream:;


            file.Close();
            stream.Close();

            Handler.tiles = level;
            Handler.walls = walls;

            Console.WriteLine("Level loaded : " + name);

        L_end:;

            completion = 100;
            LoadingGUI.GUI.SetValue(completion);
            LoadingGUI.GUI.isActive = false;

            isLoadedLevel = true;

        }

        public static void LoadLevelDataSets()
        {

            Levels.Clear();

            if (Directory.Exists(path))
            {
                string[] levelList = Directory.GetFiles(path, "*.lvl");

                for (int i = 0;i < levelList.Length;i++)
                {
                    //Level level = new Level();

                    string name = levelList[i].Substring(path.Length, levelList[i].Length - (path.Length + ext.Length));

                    FileInfo info = new FileInfo(levelList[i]);
                    string date = info.LastAccessTime.Day.ToString("00") + "." + info.LastAccessTime.Month.ToString("00") + "." + info.LastAccessTime.Year.ToString("0000");

                    double dataLength = (double)info.Length;
                    string unit = "o";

                    if (dataLength > 1024)
                    { dataLength /= 1000; unit = "ko"; }

                    if (dataLength > 1024)
                    { dataLength /= 1000; unit = "mo"; }

                    string size = (dataLength).ToString("0.00", CultureInfo.InvariantCulture) + unit;

                    Levels.Add(new LevelDataSet(name, date, size, levelList[i]));

                }

            }

        }



        public static string CheckIfFileExist(string name, int num = 0)
        {

            if(num > 0)
            {
                if (File.Exists(path + name + " (" + num + ")" + ext))
                    return CheckIfFileExist(name, num + 1);

                return name + " (" + num + ")";
            }

            if (File.Exists(path + name + ext))
                return CheckIfFileExist(name, num + 1);

            return name;

        }


    }
}
