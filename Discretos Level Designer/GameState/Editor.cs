using Discretos_Level_Designer;
using Discretos_Level_Designer.GUI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Discretos_Level_Designer
{
    public class Editor
    {

        public Vector2 cameraPosition;


        private Main main;
        private Handler handler;

        private int mouseX = 0;
        private int mouseY = 0;

        private int normalizedX = 0;
        private int normalizedY = 0;

        private bool gridEnabled = true;

        private bool isToolsBarOpen = true;
        private Vector2 toolsPosition = Vector2.Zero;

        private ButtonV3 Info;
        private ButtonV3 TileButton;
        private ButtonV3 WallButton;

        private Vector2 GridPosition;

        public bool oneReleaseMouse = false;

        public static string currentLevel = "no name";

        public static EditorTile.BlockID currentBlock = EditorTile.BlockID.none;
        public static Wall.WallID currentWall = Wall.WallID.brick_gray;
        public static int currentVarianteWall = 5;

        public static Brush brush = Brush.wall;

        public static Vector2 StartPosTilePos;


        public Editor(Handler handler, Main main)
        {
            
            this.main = main;
            this.handler = handler;

            //InfoBox = new MessageBox(new Rectangle(0, 0, 1000, 800));
            //InfoBox.SetPositionInCenter(1920, 1080);

            Info = new ButtonV3();
            TileButton = new ButtonV3();
            WallButton = new ButtonV3();

            InitButton();

        }

        public void Update(GameState state, GameTime gameTime, Screen screen, Camera camera)
        {

            mouseX = (int)Math.Round(MouseInput.GetPositionInGridWithZoomAndTranslation(screen, camera).X);
            mouseY = (int)Math.Round(MouseInput.GetPositionInGridWithZoomAndTranslation(screen, camera).Y);

            normalizedX = (mouseX / 16) * 16;
            normalizedY = (mouseY / 16) * 16;


            if (KeyInput.isSimpleClick(Keys.F4) && gridEnabled) gridEnabled = false;
            else if (KeyInput.isSimpleClick(Keys.F4) && !gridEnabled) gridEnabled = true;

            if (MouseInput.getMouseState().LeftButton == ButtonState.Released)
                oneReleaseMouse = true;

            if(MouseInput.GetScreenPosition(screen).Y > toolsPosition.Y + 100 && MessageBoxV2.HaveNotActiveBox() && oneReleaseMouse && Main.isActiveGame && brush == Brush.tile)
                TileTracer();

            else if (MouseInput.GetScreenPosition(screen).Y > toolsPosition.Y + 100 && MessageBoxV2.HaveNotActiveBox() && oneReleaseMouse && Main.isActiveGame && brush == Brush.wall)
                WallTracer();


            if (KeyInput.getKeyState().IsKeyDown(Keys.Down) || GamePad.GetState(PlayerIndex.One).DPad.Down == ButtonState.Pressed) //&& (!KeyInput.getOldKeyState().IsKeyDown(Keys.Down) && !GamePadInput.GetOldPadState().IsButtonDown(Buttons.DPadDown)))
                cameraPosition += new Vector2(0, KeyInput.getKeyState().IsKeyDown(Keys.LeftControl) ? 8 : 2);

            if (KeyInput.getKeyState().IsKeyDown(Keys.Up) || GamePad.GetState(PlayerIndex.One).DPad.Up == ButtonState.Pressed) // && (!KeyInput.getOldKeyState().IsKeyDown(Keys.Up) && !GamePadInput.GetOldPadState().IsButtonDown(Buttons.DPadUp)))
                cameraPosition += new Vector2(0, KeyInput.getKeyState().IsKeyDown(Keys.LeftControl) ? -8 : -2);

            if (KeyInput.getKeyState().IsKeyDown(Keys.Left) || GamePad.GetState(PlayerIndex.One).DPad.Left == ButtonState.Pressed) //&& (!KeyInput.getOldKeyState().IsKeyDown(Keys.Left) && !GamePadInput.GetOldPadState().IsButtonDown(Buttons.DPadLeft)))
                cameraPosition += new Vector2(KeyInput.getKeyState().IsKeyDown(Keys.LeftControl) ? -8 : -2, 0);

            if (KeyInput.getKeyState().IsKeyDown(Keys.Right) || GamePad.GetState(PlayerIndex.One).DPad.Right == ButtonState.Pressed) //&& (!KeyInput.getOldKeyState().IsKeyDown(Keys.Right) && !GamePadInput.GetOldPadState().IsButtonDown(Buttons.DPadRight)))
                cameraPosition += new Vector2(KeyInput.getKeyState().IsKeyDown(Keys.LeftControl) ? 8 : 2, 0);

            if (KeyInput.getKeyState().IsKeyDown(Keys.Space))
                InitCameraPosition();

            Main.camera.FollowObjectInWorldMap(cameraPosition + new Vector2(1920 / (2 * Camera.Zoom), 1080 / (2 * Camera.Zoom)));


            if (KeyInput.isSimpleClick(Keys.Tab) && isToolsBarOpen) isToolsBarOpen = false;
            else if (KeyInput.isSimpleClick(Keys.Tab) && !isToolsBarOpen) isToolsBarOpen = true;

            if (isToolsBarOpen && toolsPosition.Y < -5)
                toolsPosition.Y += 5;
            else if (!isToolsBarOpen && toolsPosition.Y > -100)
                toolsPosition.Y -= 10;


            #region Buttons

            Info.SetPosition(toolsPosition.X + 1740, toolsPosition.Y + 15);
            Info.Update(gameTime, screen);

            if (Info.IsSelected() && MessageBoxV2.HaveNotActiveBox())
                Info.SetColor(Color.Gray, Color.Gray);
            else
                Info.SetColor(Color.White, Color.Black);

            if (Info.IsCliqued() && MessageBoxV2.HaveNotActiveBox())
            { /*InfoBox.isActive = true;*/ InfoGUI.GUI.isActive = true; }

            TileButton.SetPosition(toolsPosition.X + 1440, toolsPosition.Y + 15);
            TileButton.Update(gameTime, screen);

            if (TileButton.IsSelected())
                TileButton.SetColor(Color.Gray, Color.Gray);
            else
                TileButton.SetColor(Color.White, Color.Black);

            if (TileButton.IsCliqued())
            { BlockGUI.GUI.isActive = true; }


            WallButton.SetPosition(toolsPosition.X + 1520, toolsPosition.Y + 15);
            WallButton.Update(gameTime, screen);

            if (WallButton.IsSelected())
                WallButton.SetColor(Color.Gray, Color.Gray);
            else
                WallButton.SetColor(Color.White, Color.Black);

            if (WallButton.IsCliqued())
            { WallGUI.GUI.isActive = true; }

            #endregion


            if (KeyInput.getKeyState().IsKeyDown(Keys.F3) && !KeyInput.getOldKeyState().IsKeyDown(Keys.F3))
            {
                LevelGUI.GUI.isActive = true;
                LevelManager.LoadLevelDataSets();
                LevelGUI.GUI.InitLevelsButtons();
            }

            if (KeyInput.getKeyState().IsKeyDown(Keys.F5) && !KeyInput.getOldKeyState().IsKeyDown(Keys.F5))
            {
                if (BlockGUI.GUI.isActive)
                    BlockGUI.GUI.isActive = false;
                else
                    BlockGUI.GUI.isActive = true;
            }

            MessageBoxV2.UpdateBoxSystem(gameTime, screen, this);
                

        }

        public void DrawInCamera(SpriteBatch spriteBatch, GameTime gameTime)
        {
            DrawEditor(spriteBatch, gameTime);
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            DrawUI(spriteBatch, gameTime);

            if (KeyInput.isSimpleClick(Keys.Q) && Camera.Zoom > 1)
                Camera.Zoom -= 1;
            else if (KeyInput.isSimpleClick(Keys.Q) && Camera.Zoom == 1)
                Camera.Zoom = 0.5f;

            if (KeyInput.isSimpleClick(Keys.E) && Camera.Zoom < 4 && Camera.Zoom >= 1)
                Camera.Zoom += 1;
            else if(KeyInput.isSimpleClick(Keys.E) && Camera.Zoom < 1)
                Camera.Zoom = 1;

            DrawMessageBox(spriteBatch);

        }


        public void DrawUI(SpriteBatch spriteBatch, GameTime gameTime)
        {


            spriteBatch.Draw(Main.BlackBar, toolsPosition, Color.White);

            Writer.DrawText(Main.UltimateFont, "zoom : x" + Camera.Zoom / 4.0f, new Vector2(10, 1000), Color.Black, Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f, 2f, spriteBatch);

            Writer.DrawText(Main.UltimateFont, "x : " + normalizedX / 16, new Vector2(10, 1040), Color.Black, Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f, 2f, spriteBatch);
            Writer.DrawText(Main.UltimateFont, "y : " + normalizedY / 16, new Vector2(150, 1040), Color.Black, Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f, 2f, spriteBatch);

            Writer.DrawText(Main.UltimateFont, "level x : " + Handler.tiles.GetLength(0), new Vector2(1680, 1000), Color.Black, Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f, 2f, spriteBatch);
            Writer.DrawText(Main.UltimateFont, "level y : " + Handler.tiles.GetLength(1), new Vector2(1680, 1040), Color.Black, Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f, 2f, spriteBatch);

            Info.Draw(spriteBatch);
            TileButton.Draw(spriteBatch);
            WallButton.Draw(spriteBatch);

        }


        public void DrawMessageBox(SpriteBatch spriteBatch)
        {

            MessageBoxV2.DrawBoxSystem(spriteBatch);

        }


        public void UpdateEditor(GameTime gameTime, Screen screen)
        {
            

        }


        public void DrawEditor(SpriteBatch spriteBatch, GameTime gameTime)
        {

            if (mouseX < 0) normalizedX -= 16;
            if (mouseY < 0) normalizedY -= 16;

            handler.Draw(spriteBatch, gameTime);


            int minX = (int)(cameraPosition.X / 16) * 16;
            int minY = (int)(cameraPosition.Y / 16) * 16;
            int maxX = (int)(cameraPosition.X + (1920 / (2 * Camera.Zoom)) * 2);
            int maxY = (int)(cameraPosition.Y + (1080 / (2 * Camera.Zoom)) * 2);

            if (cameraPosition.X < 0)
                minX -= 16;
            if (cameraPosition.Y < 0)
                minY -= 16;

            for (int i = minX; i < maxX; i += 16)
            {
                for (int j = minY; j < maxY; j += 16)
                {

                    if (i < 0 || i >= 16 * Handler.tiles.GetLength(0) || j < 0 || j >= 16 * Handler.tiles.GetLength(1))
                        spriteBatch.Draw(Main.Case, new Vector2(i, j), Color.Red * 0.5f);
                    else if (gridEnabled)
                        spriteBatch.Draw(Main.Case, new Vector2(i, j), Color.White * 0.5f);

                }

            }

            if (MessageBoxV2.HaveNotActiveBox())
            {
                spriteBatch.Draw(Main.Case, new Rectangle(normalizedX - 1, normalizedY - 1, 17, 17), Color.Blue * 1f);
                spriteBatch.Draw(Main.Case, new Rectangle(normalizedX - 1, normalizedY, 17, 17), Color.Blue * 1f);
                spriteBatch.Draw(Main.Case, new Rectangle(normalizedX, normalizedY - 1, 17, 17), Color.Blue * 1f);
                spriteBatch.Draw(Main.Case, new Rectangle(normalizedX, normalizedY, 17, 17), Color.Blue * 1f);
            }

        }


        public void TileTracer()
        {

            if (MouseInput.getMouseState().LeftButton == ButtonState.Pressed)
            {
                if (normalizedX >= 0 && normalizedY >= 0 && normalizedX / 16 < Handler.tiles.GetLength(0) && normalizedY / 16 < Handler.tiles.GetLength(1))
                {

                    int x = normalizedX / 16;
                    int y = normalizedY / 16;
                    bool isSlope = false;

                    if (KeyInput.getKeyState().IsKeyDown(Keys.LeftControl)) isSlope = true;


                    if (currentBlock == EditorTile.BlockID.start_pos)
                    {
                        Handler.tiles[(int)StartPosTilePos.X / 16, (int)StartPosTilePos.Y / 16] = new EditorTile(StartPosTilePos, EditorTile.BlockID.none);
                        StartPosTilePos = new Vector2(normalizedX, normalizedY);
                    }

                    Handler.tiles[x, y] = new EditorTile(new Vector2(normalizedX, normalizedY), currentBlock, isSlope);

                    UpdateBlock(x, y);

                }


            }

            if (MouseInput.getMouseState().RightButton == ButtonState.Pressed)
            {
                if (normalizedX >= 0 && normalizedY >= 0 && normalizedX / 16 < Handler.tiles.GetLength(0) && normalizedY / 16 < Handler.tiles.GetLength(1))
                {

                    int x = normalizedX / 16;
                    int y = normalizedY / 16;

                    Handler.tiles[x, y] = new EditorTile(new Vector2(normalizedX, normalizedY), EditorTile.BlockID.none);

                    UpdateBlock(x, y);

                }

            }

            

        }

        public void WallTracer()
        {

            if (MouseInput.getMouseState().LeftButton == ButtonState.Pressed)
            {
                if (normalizedX >= 0 && normalizedY >= 0 && normalizedX / 16 < Handler.tiles.GetLength(0) && normalizedY / 16 < Handler.tiles.GetLength(1))
                {

                    int x = normalizedX / 16;
                    int y = normalizedY / 16;

                    Handler.walls[x, y] = new Wall(new Vector2(normalizedX, normalizedY), currentWall, currentVarianteWall);

                    UpdateBlock(x, y);

                }


            }

            if (MouseInput.getMouseState().RightButton == ButtonState.Pressed)
            {
                if (normalizedX >= 0 && normalizedY >= 0 && normalizedX / 16 < Handler.walls.GetLength(0) && normalizedY / 16 < Handler.walls.GetLength(1))
                {

                    int x = normalizedX / 16;
                    int y = normalizedY / 16;

                    Handler.walls[x, y] = new Wall(new Vector2(normalizedX, normalizedY), Wall.WallID.none, 0);

                    UpdateBlock(x, y);

                }

            }

        }

        public void UpdateBlock(int x, int y)
        {

            int minX = x - 1;
            int minY = y - 1;
            int maxX = x + 2;
            int maxY = y + 2;

            if (minX < 0) minX = 0;
            if (minY < 0) minY = 0;
            if (maxX >= Handler.tiles.GetLength(0)) maxX = Handler.tiles.GetLength(0);
            if (maxY >= Handler.tiles.GetLength(1)) maxY = Handler.tiles.GetLength(1);

            for (int i = minX; i < maxX; i++)
            {
                for (int j = minY; j < maxY; j++)
                {

                    Handler.tiles[i, j].InitImg(Handler.tiles);

                }
            }

        }


        public void InitButton()
        {

            Info.SetTexture(Main.ButtonTexture[6], new Rectangle(0, 0, 16, 16));
            Info.SetColor(Color.White, Color.Black);
            Info.SetFont(Main.UltimateFont);
            Info.SetScale(4);
            Info.SetPosition(toolsPosition.X + 1740, toolsPosition.Y + 15);

            TileButton.SetTexture(Main.ButtonTexture[8], new Rectangle(0, 0, 16, 16));
            TileButton.SetColor(Color.White, Color.Black);
            TileButton.SetFont(Main.UltimateFont);
            TileButton.SetScale(4);
            TileButton.SetPosition(toolsPosition.X + 1440, toolsPosition.Y + 15);

            WallButton.SetTexture(Main.ButtonTexture[13], new Rectangle(0, 0, 16, 16));
            WallButton.SetColor(Color.White, Color.Black);
            WallButton.SetFont(Main.UltimateFont);
            WallButton.SetScale(4);
            WallButton.SetPosition(toolsPosition.X + 1520, toolsPosition.Y + 15);

        }

        public int GetVarianteNum(int wallID)
        {
            switch (wallID)
            {
                /// Prendre -1

                case 0:
                    return 9;

                case 1:
                    return 9;

                case 2:
                    return 9;

                case 3:
                    return 9;

                case 4:
                    return 9;

                case 5:
                    return 9;

                case 6:
                    return 9;

                case 7:
                    return 9;

                default:
                    return 0;
            }
        }

        public void InitCameraPosition()
        {
            cameraPosition = Vector2.Zero;
        }

        public enum Brush
        {
            none = 0,
            tile = 1,
            wall = 2,
        }

    }
}