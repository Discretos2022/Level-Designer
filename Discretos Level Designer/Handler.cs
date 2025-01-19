using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Discretos_Level_Designer.NetCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Text;

namespace Discretos_Level_Designer
{
    public class Handler
    {

        /// <summary>
        ///  Base Game
        /// </summary>
        public static List<Actor> actors = new List<Actor>();
        public static List<TileV2> solids = new List<TileV2>();
        //public static List<Wall> walls = new List<Wall>();
        public static List<Actor> ladder = new List<Actor>();
        public static Dictionary<int, PlayerV2> playersV2 = new Dictionary<int, PlayerV2>();
        public static TileV2[,] Level;
        public static List<MovingBlock> blocks = new List<MovingBlock>();


        /// <summary>
        /// Editor
        /// </summary>
        public static EditorTile[,] tiles = new EditorTile[100, 50];
        public static Wall[,] walls = new Wall[100, 50];
        private static EditorTile[,] tilesCopie = new EditorTile[tiles.GetLength(0), tiles.GetLength(1)];
        private static Wall[,] wallsCopie = new Wall[walls.GetLength(0), walls.GetLength(1)];

        public static void Initialize()
        {
            solids = new List<TileV2>();
            //walls = new List<Wall>();
            actors = new List<Actor>();
            ladder = new List<Actor>();
            blocks = new List<MovingBlock>();

        }

        public void Update(GameTime gameTime)
        {

            for (int i = 0; i < solids.Count; i++)
            {
                solids[i].Update(gameTime);
            }

            for (int i = 0; i < actors.Count; i++)
            {
                actors[i].Update(gameTime);
            }

            for (int i = 0; i < blocks.Count; i++)
            {
                blocks[i].Update(gameTime);
            }

            for (int i = 1; i <= playersV2.Count; i++)
            {
                playersV2[i].Update(gameTime);
            }




            #region Left Collision

            if (!playersV2[NetPlay.MyPlayerID()].isDead) playersV2[NetPlay.MyPlayerID()].LeftDisplacement(gameTime);
            for (int i = 0; i < actors.Count; i++) actors[i].LeftDisplacement(gameTime);

            for (int i = 0; i < blocks.Count; i++) blocks[i].RightDisplacement(gameTime);


            if (!playersV2[NetPlay.MyPlayerID()].isDead) playersV2[NetPlay.MyPlayerID()].LeftDynamicCollision();
            if (!playersV2[NetPlay.MyPlayerID()].isDead) playersV2[NetPlay.MyPlayerID()].LeftStaticCollision();

            for (int i = 0; i < actors.Count; i++) actors[i].LeftDynamicCollision();
            for (int i = 0; i < actors.Count; i++) actors[i].LeftStaticCollision();

            #endregion


            #region Right Collision

            if (!playersV2[NetPlay.MyPlayerID()].isDead) playersV2[NetPlay.MyPlayerID()].RightDisplacement(gameTime);
            for (int i = 0; i < actors.Count; i++) actors[i].RightDisplacement(gameTime);


            for (int i = 0; i < blocks.Count; i++) blocks[i].LeftDisplacement(gameTime);
            
            


            if (!playersV2[NetPlay.MyPlayerID()].isDead) playersV2[NetPlay.MyPlayerID()].RightDynamicCollision();
            if (!playersV2[NetPlay.MyPlayerID()].isDead) playersV2[NetPlay.MyPlayerID()].RightStaticCollision();

            /*Bug*/ //if (!playersV2[1].isDead) playersV2[1].LeftDynamicCollision();
            /*Bug*/ //if (!playersV2[1].isDead) playersV2[1].LeftStaticCollision();

            for (int i = 0; i < actors.Count; i++) actors[i].RightDynamicCollision();
            for (int i = 0; i < actors.Count; i++) actors[i].RightStaticCollision();

            #endregion


            #region Down Collision

            if (!playersV2[NetPlay.MyPlayerID()].isDead) playersV2[NetPlay.MyPlayerID()].DownDisplacement(gameTime);
            for (int i = 0; i < actors.Count; i++) actors[i].DownDisplacement(gameTime);


            for (int i = 0; i < blocks.Count; i++) blocks[i].UpDisplacement(gameTime);


            if (!playersV2[NetPlay.MyPlayerID()].isDead) playersV2[NetPlay.MyPlayerID()].DownDynamicCollision();
            if (!playersV2[NetPlay.MyPlayerID()].isDead) playersV2[NetPlay.MyPlayerID()].DownStaticCollision();
            for (int i = 0; i < actors.Count; i++) actors[i].DownDynamicCollision();
            for (int i = 0; i < actors.Count; i++) actors[i].DownStaticCollision();

            #endregion


            #region Up Collision

            if (!playersV2[NetPlay.MyPlayerID()].isDead) playersV2[NetPlay.MyPlayerID()].UpDisplacement(gameTime);
            for (int i = 0; i < actors.Count; i++) actors[i].UpDisplacement(gameTime);


            for (int i = 0; i < blocks.Count; i++) blocks[i].DownDisplacement(gameTime);


            if (!playersV2[NetPlay.MyPlayerID()].isDead) playersV2[NetPlay.MyPlayerID()].UpDynamicCollision();
            if (!playersV2[NetPlay.MyPlayerID()].isDead) playersV2[NetPlay.MyPlayerID()].UpStaticCollision();
            for (int i = 0; i < actors.Count; i++) actors[i].UpDynamicCollision();
            for (int i = 0; i < actors.Count; i++) actors[i].UpStaticCollision();

            #endregion



        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {

            #region Tiles and Walls Optimization

            /*int xMin = ((int)Main.camera.Position.X - 1920 / 8) / 16 - 2;
            int xMax = ((int)Main.camera.Position.X + 1920 / 8) / 16 + 2;

            int yMin = ((int)Main.camera.Position.Y - 1080 / 8) / 16 - 2;
            int yMax = ((int)Main.camera.Position.Y + 1080 / 8) / 16 + 2;

            if (xMin < 0) xMin = 0;
            if (xMax > Level.GetLength(0)) xMax = Level.GetLength(0);
            if (yMin < 0) yMin = 0;
            if (yMax > Level.GetLength(1)) yMax = Level.GetLength(1);*/

            int minX = (int)(Main.camera.Position.X - 1920 / (2 * Camera.Zoom)) / 16;
            int minY = (int)(Main.camera.Position.Y - 1080 / (2 * Camera.Zoom)) / 16;
            int maxX = (int)(Main.camera.Position.X + 1920 / (2 * Camera.Zoom)) / 16 + 1;
            int maxY = (int)(Main.camera.Position.Y + 1080 / (2 * Camera.Zoom)) / 16 + 1;

            if (minX < 0) minX = 0;
            if (minY < 0) minY = 0;
            if (maxX > tiles.GetLength(0)) maxX = tiles.GetLength(0);
            if (maxY > tiles.GetLength(1)) maxY = tiles.GetLength(1);


            #endregion


            /*for (int j = yMin; j < yMax; j++)
                for (int i = xMin; i < xMax; i++)
                    if (Handler.Walls[i, j].getType() > 0)
                        Handler.Walls[i, j].Draw(spriteBatch, gameTime);



            for (int j = yMin; j < yMax; j++)
                for (int i = xMin; i < xMax; i++)
                    if (Handler.Level[i, j].ID > 0)
                        Handler.Level[i, j].Draw(spriteBatch, gameTime);*/


            for (int i = 0; i < blocks.Count; i++)
            {
                blocks[i].Draw(spriteBatch, gameTime);
            }



            for (int i = 0; i < solids.Count; i++)
            {
                solids[i].Draw(spriteBatch, gameTime);
            }

            for (int i = 0; i < actors.Count; i++)
            {
                actors[i].Draw(spriteBatch, gameTime);
            }

            for (int i = 1; i <= playersV2.Count; i++)
            {
                playersV2[i].Draw(spriteBatch, gameTime);
            }

            for (int i = minX; i < maxX; i++)
                for (int j = minY; j < maxY; j++)
                    if (walls[i, j].ID != Wall.WallID.none)
                        walls[i, j].Draw(spriteBatch, gameTime);

            for (int i = minX; i < maxX; i++)
                for (int j = minY; j < maxY; j++)
                    if (tiles[i, j].ID != EditorTile.BlockID.none)
                        tiles[i, j].Draw(spriteBatch, gameTime);


        }

        public static void InitPlayersList()
        {
            playersV2.Clear();
        }

        public static void AddPlayerV2(int ID)
        {
            if(!playersV2.ContainsKey(ID))
                playersV2.Add(ID, new PlayerV2(Vector2.Zero, ID));
        }

        /// <summary>
        /// For multiplayer
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="_myPlayerID"></param>
        /// <param name="_clientID"></param>
        public static void AddPlayerV2(int ID, int _myPlayerID)
        {
            if (!playersV2.ContainsKey(ID))
                playersV2.Add(ID, new PlayerV2(Vector2.Zero, ID, _myPlayerID));
        }


        public static void RemoveActor(Actor actor)
        {

            if (NetPlay.IsMultiplaying)
            {
                if (NetPlay.MyPlayerID() == 1)
                {
                    if (actor.actorType == Actor.ActorType.Object && actor.GetID() == 5)
                        NetworkEngine_5._0.Server.ServerSender.SendCollectedKey(actors.IndexOf(actor), NetPlay.MyPlayerID());
                    else
                        NetworkEngine_5._0.Server.ServerSender.SendDistroyedObject(actors.IndexOf(actor));
                }
                else
                {
                    if (actor.actorType == Actor.ActorType.Object && actor.GetID() == 5)
                        NetworkEngine_5._0.Client.ClientSender.SendCollectedKey(actors.IndexOf(actor), NetPlay.MyPlayerID());
                    else
                        NetworkEngine_5._0.Client.ClientSender.SendDistroyedObject(actors.IndexOf(actor));
                }
            }

            actors.Remove(actor);

        }

        public static void InitEditorGrid()
        {

            //Level = new TileV2[50, 50];

            for (int i = 0; i < tiles.GetLength(0); i++)
                for (int j = 0; j < tiles.GetLength(1); j++)
                    tiles[i, j] = new EditorTile(new Vector2(i * 16, j * 16), 0, false, true);

            for (int i = 0; i < walls.GetLength(0); i++)
                for (int j = 0; j < walls.GetLength(1); j++)
                    walls[i, j] = new Wall(new Vector2(i * 16, j * 16), Wall.WallID.none, 0);

            tiles[5, 45] = new EditorTile(new Vector2(5 * 16, 45 * 16), EditorTile.BlockID.start_pos, false, true);
            Editor.StartPosTilePos = new Vector2(5 * 16, 45 * 16);

        }


        public static void AddBlockLineLeft()
        {
            tilesCopie = tiles;
            wallsCopie = walls;

            tiles = new EditorTile[tilesCopie.GetLength(0) + 1, tilesCopie.GetLength(1)];
            walls = new Wall[wallsCopie.GetLength(0) + 1, wallsCopie.GetLength(1)];


            for (int i = 0; i < tiles.GetLength(0); i++)
                for (int j = 0; j < tiles.GetLength(1); j++)
                    tiles[i, j] = new EditorTile(new Vector2(16 * i, 16 * j), 0);

            for (int i = 0; i < tilesCopie.GetLength(0); i++)
                for (int j = 0; j < tilesCopie.GetLength(1); j++)
                    tiles[i + 1, j] = tilesCopie[i, j];

            for (int i = 0; i < tiles.GetLength(0); i++)
                for (int j = 0; j < tiles.GetLength(1); j++)
                    tiles[i, j] = new EditorTile(new Vector2(16 * i, 16 * j), tiles[i, j].ID, tiles[i, j].isSlope);


            for (int i = 0; i < walls.GetLength(0); i++)
                for (int j = 0; j < walls.GetLength(1); j++)
                    walls[i, j] = new Wall(new Vector2(16 * i, 16 * j), 0, 0);

            for (int i = 0; i < wallsCopie.GetLength(0); i++)
                for (int j = 0; j < wallsCopie.GetLength(1); j++)
                    walls[i + 1, j] = wallsCopie[i, j];

            for (int i = 0; i < walls.GetLength(0); i++)
                for (int j = 0; j < walls.GetLength(1); j++)
                    walls[i, j] = new Wall(new Vector2(16 * i, 16 * j), walls[i, j].ID, walls[i, j].variante);

            UpdateAllBlock();

        }
        public static void RemoveBlockLineLeft()
        {
            tilesCopie = tiles;
            wallsCopie = walls;

            tiles = new EditorTile[tilesCopie.GetLength(0) - 1, tilesCopie.GetLength(1)];
            walls = new Wall[wallsCopie.GetLength(0) - 1, wallsCopie.GetLength(1)];


            for (int i = 0; i < tiles.GetLength(0); i++)
                for (int j = 0; j < tiles.GetLength(1); j++)
                    tiles[i, j] = new EditorTile(new Vector2(16 * i, 16 * j), 0);

            for (int i = 1; i < tilesCopie.GetLength(0); i++)
                for (int j = 0; j < tilesCopie.GetLength(1); j++)
                    tiles[i - 1, j] = tilesCopie[i, j];

            for (int i = 0; i < tiles.GetLength(0); i++)
                for (int j = 0; j < tiles.GetLength(1); j++)
                    tiles[i, j] = new EditorTile(new Vector2(16 * i, 16 * j), tiles[i, j].ID, tiles[i, j].isSlope);


            for (int i = 0; i < walls.GetLength(0); i++)
                for (int j = 0; j < walls.GetLength(1); j++)
                    walls[i, j] = new Wall(new Vector2(16 * i, 16 * j), 0, 0);

            for (int i = 1; i < tilesCopie.GetLength(0); i++)
                for (int j = 0; j < tilesCopie.GetLength(1); j++)
                    walls[i - 1, j] = wallsCopie[i, j];

            for (int i = 0; i < walls.GetLength(0); i++)
                for (int j = 0; j < walls.GetLength(1); j++)
                    walls[i, j] = new Wall(new Vector2(16 * i, 16 * j), walls[i, j].ID, walls[i, j].variante);

            UpdateAllBlock();

        }


        public static void AddBlockLineRight()
        {
            tilesCopie = tiles;
            wallsCopie = walls;

            tiles = new EditorTile[tilesCopie.GetLength(0) + 1, tilesCopie.GetLength(1)];
            walls = new Wall[wallsCopie.GetLength(0) + 1, wallsCopie.GetLength(1)];


            for (int i = 0; i < tiles.GetLength(0); i++)
                for (int j = 0; j < tiles.GetLength(1); j++)
                    tiles[i, j] = new EditorTile(new Vector2(16 * i, 16 * j), 0);

            for (int i = 0; i < tilesCopie.GetLength(0); i++)
                for (int j = 0; j < tilesCopie.GetLength(1); j++)
                    tiles[i, j] = tilesCopie[i, j];


            for (int i = 0; i < walls.GetLength(0); i++)
                for (int j = 0; j < walls.GetLength(1); j++)
                    walls[i, j] = new Wall(new Vector2(16 * i, 16 * j), 0, 0);

            for (int i = 0; i < tilesCopie.GetLength(0); i++)
                for (int j = 0; j < tilesCopie.GetLength(1); j++)
                    walls[i, j] = wallsCopie[i, j];

            UpdateAllBlock();

        }
        public static void RemoveBlockLineRight()
        {
            tilesCopie = tiles;
            wallsCopie = walls;

            tiles = new EditorTile[tilesCopie.GetLength(0) - 1, tilesCopie.GetLength(1)];
            walls = new Wall[wallsCopie.GetLength(0) - 1, wallsCopie.GetLength(1)];


            for (int i = 0; i < tiles.GetLength(0); i++)
                for (int j = 0; j < tiles.GetLength(1); j++)
                    tiles[i, j] = new EditorTile(new Vector2(16 * i, 16 * j), 0);

            for (int i = 0; i < tilesCopie.GetLength(0) - 1; i++)
                for (int j = 0; j < tilesCopie.GetLength(1); j++)
                    tiles[i, j] = tilesCopie[i, j];


            for (int i = 0; i < walls.GetLength(0); i++)
                for (int j = 0; j < walls.GetLength(1); j++)
                    walls[i, j] = new Wall(new Vector2(16 * i, 16 * j), 0, 0);

            for (int i = 0; i < tilesCopie.GetLength(0) - 1; i++)
                for (int j = 0; j < tilesCopie.GetLength(1); j++)
                    walls[i, j] = wallsCopie[i, j];

            UpdateAllBlock();

        }


        public static void AddBlockLineDown()
        {
            tilesCopie = tiles;
            wallsCopie = walls;

            tiles = new EditorTile[tilesCopie.GetLength(0), tilesCopie.GetLength(1) + 1];
            walls = new Wall[wallsCopie.GetLength(0), wallsCopie.GetLength(1) + 1];


            for (int i = 0; i < tiles.GetLength(0); i++)
                for (int j = 0; j < tiles.GetLength(1); j++)
                    tiles[i, j] = new EditorTile(new Vector2(16 * i, 16 * j), 0);

            for (int i = 0; i < tilesCopie.GetLength(0); i++)
                for (int j = 0; j < tilesCopie.GetLength(1); j++)
                    tiles[i, j] = tilesCopie[i, j];


            for (int i = 0; i < walls.GetLength(0); i++)
                for (int j = 0; j < walls.GetLength(1); j++)
                    walls[i, j] = new Wall(new Vector2(16 * i, 16 * j), 0, 0);

            for (int i = 0; i < tilesCopie.GetLength(0); i++)
                for (int j = 0; j < tilesCopie.GetLength(1); j++)
                    walls[i, j] = wallsCopie[i, j];

            UpdateAllBlock();

        }
        public static void RemoveBlockLineDown()
        {
            tilesCopie = tiles;
            wallsCopie = walls;

            tiles = new EditorTile[tilesCopie.GetLength(0), tilesCopie.GetLength(1) - 1];
            walls = new Wall[wallsCopie.GetLength(0), wallsCopie.GetLength(1) - 1];


            for (int i = 0; i < tiles.GetLength(0); i++)
                for (int j = 0; j < tiles.GetLength(1); j++)
                    tiles[i, j] = new EditorTile(new Vector2(16 * i, 16 * j), 0);

            for (int i = 0; i < tilesCopie.GetLength(0); i++)
                for (int j = 0; j < tilesCopie.GetLength(1) - 1; j++)
                    tiles[i, j] = tilesCopie[i, j];


            for (int i = 0; i < walls.GetLength(0); i++)
                for (int j = 0; j < walls.GetLength(1); j++)
                    walls[i, j] = new Wall(new Vector2(16 * i, 16 * j), 0, 0);

            for (int i = 0; i < tilesCopie.GetLength(0); i++)
                for (int j = 0; j < tilesCopie.GetLength(1) - 1; j++)
                    walls[i, j] = wallsCopie[i, j];

            UpdateAllBlock();

        }


        public static void AddBlockLineUp()
        {
            tilesCopie = tiles;
            wallsCopie = walls;

            tiles = new EditorTile[tilesCopie.GetLength(0), tilesCopie.GetLength(1) + 1];
            walls = new Wall[wallsCopie.GetLength(0), wallsCopie.GetLength(1) + 1];


            for (int i = 0; i < tiles.GetLength(0); i++)
                for (int j = 0; j < tiles.GetLength(1); j++)
                    tiles[i, j] = new EditorTile(new Vector2(16 * i, 16 * j), 0);

            for (int i = 0; i < tilesCopie.GetLength(0); i++)
                for (int j = 0; j < tilesCopie.GetLength(1); j++)
                    tiles[i, j + 1] = tilesCopie[i, j];

            for (int i = 0; i < tiles.GetLength(0); i++)
                for (int j = 0; j < tiles.GetLength(1); j++)
                    tiles[i, j] = new EditorTile(new Vector2(16 * i, 16 * j), tiles[i, j].ID, tiles[i, j].isSlope);


            for (int i = 0; i < walls.GetLength(0); i++)
                for (int j = 0; j < walls.GetLength(1); j++)
                    walls[i, j] = new Wall(new Vector2(16 * i, 16 * j), 0, 0);

            for (int i = 0; i < tilesCopie.GetLength(0); i++)
                for (int j = 0; j < tilesCopie.GetLength(1); j++)
                    walls[i, j + 1] = wallsCopie[i, j];

            for (int i = 0; i < walls.GetLength(0); i++)
                for (int j = 0; j < walls.GetLength(1); j++)
                    walls[i, j] = new Wall(new Vector2(16 * i, 16 * j), walls[i, j].ID, walls[i, j].variante);

            UpdateAllBlock();

        }
        public static void RemoveBlockLineUp()
        {
            tilesCopie = tiles;
            wallsCopie = walls;

            tiles = new EditorTile[tilesCopie.GetLength(0), tilesCopie.GetLength(1) - 1];
            walls = new Wall[wallsCopie.GetLength(0), wallsCopie.GetLength(1) - 1];


            for (int i = 0; i < tiles.GetLength(0); i++)
                for (int j = 0; j < tiles.GetLength(1); j++)
                    tiles[i, j] = new EditorTile(new Vector2(16 * i, 16 * j), 0);

            for (int i = 0; i < tilesCopie.GetLength(0); i++)
                for (int j = 1; j < tilesCopie.GetLength(1); j++)
                    tiles[i, j - 1] = tilesCopie[i, j];

            for (int i = 0; i < tiles.GetLength(0); i++)
                for (int j = 0; j < tiles.GetLength(1); j++)
                    tiles[i, j] = new EditorTile(new Vector2(16 * i, 16 * j), tiles[i, j].ID, tiles[i, j].isSlope);


            for (int i = 0; i < walls.GetLength(0); i++)
                for (int j = 0; j < walls.GetLength(1); j++)
                    walls[i, j] = new Wall(new Vector2(16 * i, 16 * j), 0, 0);

            for (int i = 0; i < tilesCopie.GetLength(0); i++)
                for (int j = 1; j < tilesCopie.GetLength(1); j++)
                    walls[i, j - 1] = wallsCopie[i, j];

            for (int i = 0; i < walls.GetLength(0); i++)
                for (int j = 0; j < walls.GetLength(1); j++)
                    walls[i, j] = new Wall(new Vector2(16 * i, 16 * j), walls[i, j].ID, walls[i, j].variante);

            UpdateAllBlock();

        }


        private static void UpdateAllBlock()
        {

            for (int y = 0; y < tiles.GetLength(1); y++)
            {
                for (int x = 0; x < tiles.GetLength(0); x++)
                {
                    tiles[x, y].InitImg(tiles);
                }
            }

        }



    }
}
