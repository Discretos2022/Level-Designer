using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Discretos_Level_Designer.GUI
{
    public class WallGUI : MessageBoxV2
    {

        private static WallGUI _instance;

        private static int scrollValue;

        private Vector2 tileButtonPosition;


        //private List<ObjectButton> tileButtons = new List<ObjectButton>();

        private List<ObjectButton> wallButton = new List<ObjectButton>();
        private List<ObjectButton> variantWallButton = new List<ObjectButton>();


        private WallGUI() : base()
        {
            
        }

        public static WallGUI GUI
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new WallGUI();

                    GUI.Dimension = new Rectangle(0, 0, 1400, 800);
                    GUI.SetPositionInCenter(1920, 1080);

                    GUI.AddCloseButton(new Vector2(GUI.Position.X + GUI.Dimension.Width - 70, GUI.Position.Y + 10));

                    for (int i = 1; i < Main.Wallset.Length; i++)
                    {
                        ObjectButton b = new ObjectButton();

                        Rectangle _sourceRectangle = new Rectangle(17, 0, 16, 16);

                        if(i == 2) _sourceRectangle = new Rectangle(0, 0, 16, 16);

                        b.SetTexture(Main.Wallset[i], _sourceRectangle);
                        b.SetColor(Color.White, Color.Black);
                        b.SetScale(4);
                        b.SetPosition(100 * i, 10);

                        b.SetOutline(Main.Bounds, Color.Black, 4);

                        GUI.wallButton.Add(b);
                    }




                    #region Create Variantes Walls Buttons

                    GUI.BuildVarianteButton(Wall.WallID.brick_gray);

                    #endregion






                    /*ObjectButton startPosButton = new ObjectButton();
                    startPosButton.SetTexture(Main.StartPosImg, new Rectangle(0, 0, 16, 16));
                    startPosButton.SetColor(Color.White, Color.Black);
                    startPosButton.SetScale(4);
                    startPosButton.SetOutline(Main.Bounds, Color.Black, 4);
                    GUI.wallButton.Add(startPosButton);*/

                }
                return _instance;
            }

        }

        public override void Update(GameTime gameTime, Screen screen, Editor editor)
        {

            base.Update(gameTime, screen, editor);


            for (int i = 0; i < wallButton.Count; i++)
            {

                wallButton[i].Update(gameTime, screen);

                /*if (tileButtons[i].IsSelected())
                    tileButtons[i].SetColor(Color.Gray, Color.Gray);
                else
                    tileButtons[i].SetColor(Color.White, Color.Black);*/

                if (wallButton[i].IsSelected())
                    wallButton[i].SetOutlineColor(Color.White);
                else
                    wallButton[i].SetOutlineColor(Color.Black);

                if (wallButton[i].IsReleased())
                {

                    Editor.currentWall = (Wall.WallID)i + 1;
                    wallButton[i].SetOutlineColor(Color.White);

                    //Editor.brush = Editor.Brush.wall;

                    BuildVarianteButton((Wall.WallID)i);
                    
                }
            }


            for (int i = 0; i < variantWallButton.Count; i++)
            {

                variantWallButton[i].Update(gameTime, screen);

                /*if (tileButtons[i].IsSelected())
                    tileButtons[i].SetColor(Color.Gray, Color.Gray);
                else
                    tileButtons[i].SetColor(Color.White, Color.Black);*/

                if (variantWallButton[i].IsSelected())
                    variantWallButton[i].SetOutlineColor(Color.White);
                else
                    variantWallButton[i].SetOutlineColor(Color.Black);

                if (variantWallButton[i].IsReleased())
                {

                    Editor.currentVarianteWall = i + 1;
                    variantWallButton[i].SetOutlineColor(Color.White);

                    Editor.brush = Editor.Brush.wall;

                }
            }



            if (KeyInput.getKeyState().IsKeyDown(Keys.Up) && tileButtonPosition.Y < 0)
                tileButtonPosition.Y += 4f;
            if (KeyInput.getKeyState().IsKeyDown(Keys.Down))
                tileButtonPosition.Y -= 4f;

            scrollValue = MouseInput.getMouseState().ScrollWheelValue - MouseInput.getOldMouseState().ScrollWheelValue;

            if (MouseInput.GetRectangle(screen).Intersects(new Rectangle((int)Position.X, (int)Position.Y + 100, 7 * 80 + 24, Dimension.Height - 200)))
                tileButtonPosition.Y += scrollValue / 5;

            if (tileButtonPosition.Y > 0)
                tileButtonPosition.Y = 0;



        }

        public override void Draw(SpriteBatch spriteBatch)
        {

            base.Draw(spriteBatch);

            if (isActive)
            {

                for (int i = 0; i < wallButton.Count; i++)
                {

                    if (i < 7)
                        wallButton[i].SetPosition(Position.X + 80 * i + 20, Position.Y + 120 + tileButtonPosition.Y);
                    else if (i >= 7)
                        wallButton[i].SetPosition(Position.X + 80 * (i - 7) + 20, Position.Y + 200 + tileButtonPosition.Y);

                    if (wallButton[i].GetPosition().Y + 64 > Position.Y + 100)
                        wallButton[i].Draw(spriteBatch);

                }

                for (int i = 0; i < variantWallButton.Count; i++)
                {

                    if (i < 9)
                        variantWallButton[i].SetPosition(Position.X + 80 * i + 20 + 8 * 80, Position.Y + 120 + tileButtonPosition.Y);
                    else if (i >= 9)
                        variantWallButton[i].SetPosition(Position.X + 80 * (i - 7) + 20 + 8 * 80, Position.Y + 200 + tileButtonPosition.Y);

                    if (variantWallButton[i].GetPosition().Y + 64 > Position.Y + 100)
                        variantWallButton[i].Draw(spriteBatch);

                }

                spriteBatch.Draw(Main.Bounds, new Rectangle((int)Position.X, (int)Position.Y, Dimension.Width, 96), new Color(110, 110, 110));
                spriteBatch.Draw(Main.Bounds, new Rectangle((int)Position.X, (int)Position.Y + Dimension.Height - 100, Dimension.Width, 100), new Color(110, 110, 110));

                Writer.DrawText(Main.UltimateFont, "WALLS", new Vector2(1920 / 2 - Main.UltimateFont.MeasureString("BLOCKS").ToPoint().X / 2 * 4, Position.Y + 15), new Color(60, 60, 60), Color.White, 0f, Vector2.Zero, 4f, SpriteEffects.None, 0f, 4f, spriteBatch, Color.Black, false);

                DrawCloseButton(spriteBatch);

            }

        }

        public void InitLevelsButtons()
        {
            tileButtonPosition.Y = 0;
        }


        public void BuildVarianteButton(Wall.WallID id)
        {

            GUI.variantWallButton.Clear();
            int numOfVariant = 9;
            for (int w = 0; w < numOfVariant; w++)
            {
                GUI.variantWallButton.Add(new ObjectButton());

                Rectangle _sourceRectangle = new Rectangle(0, 0, 0, 0);
                switch (w + 1)
                {
                    case 1:
                        _sourceRectangle = new Rectangle(0, 0, 16, 16);
                        break;

                    case 2:
                        _sourceRectangle = new Rectangle(17, 0, 16, 16);
                        break;

                    case 3:
                        _sourceRectangle = new Rectangle(34, 0, 16, 16);
                        break;

                    case 4:
                        _sourceRectangle = new Rectangle(0, 17, 16, 16);
                        break;

                    case 5:
                        _sourceRectangle = new Rectangle(17, 17, 16, 16);
                        break;

                    case 6:
                        _sourceRectangle = new Rectangle(34, 17, 16, 16);
                        break;

                    case 7:
                        _sourceRectangle = new Rectangle(0, 34, 16, 16);
                        break;

                    case 8:
                        _sourceRectangle = new Rectangle(17, 34, 16, 16);
                        break;

                    case 9:
                        _sourceRectangle = new Rectangle(34, 34, 16, 16);
                        break;
                }

                GUI.variantWallButton[w].SetTexture(Main.Wallset[(int)id + 1], _sourceRectangle);
                GUI.variantWallButton[w].SetColor(Color.White, Color.Black);
                GUI.variantWallButton[w].SetScale(4);
                GUI.variantWallButton[w].SetPosition(100 * w, 10);

                GUI.variantWallButton[w].SetOutline(Main.Bounds, Color.Black, 4);
            }

        }


    }
}
