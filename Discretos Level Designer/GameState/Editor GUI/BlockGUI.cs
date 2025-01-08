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
    public class BlockGUI : MessageBoxV2
    {

        private static BlockGUI _instance;

        private static int scrollValue;

        private Vector2 tileButtonPosition;


        private List<ObjectButton> tileButtons = new List<ObjectButton>();


        private BlockGUI() : base()
        {
            
        }

        public static BlockGUI GUI
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new BlockGUI();

                    GUI.Dimension = new Rectangle(0, 0, 1000, 800);
                    GUI.SetPositionInCenter(1920, 1080);

                    GUI.AddCloseButton(new Vector2(GUI.Position.X + GUI.Dimension.Width - 70, GUI.Position.Y + 10));

                    for (int i = 1; i < Main.Tileset.Length; i++)
                    {
                        ObjectButton b = new ObjectButton();

                        Rectangle _sourceRectangle = new Rectangle(17, 0, 16, 16);

                        if(i == 2) _sourceRectangle = new Rectangle(0, 0, 16, 16);

                        b.SetTexture(Main.Tileset[i], _sourceRectangle);
                        b.SetColor(Color.White, Color.Black);
                        b.SetScale(4);
                        b.SetPosition(100 * i, 10);

                        b.SetOutline(Main.Bounds, Color.Black, 4);

                        GUI.tileButtons.Add(b);
                    }

                    ObjectButton startPosButton = new ObjectButton();
                    startPosButton.SetTexture(Main.StartPosImg, new Rectangle(0, 0, 16, 16));
                    startPosButton.SetColor(Color.White, Color.Black);
                    startPosButton.SetScale(4);
                    startPosButton.SetOutline(Main.Bounds, Color.Black, 4);
                    GUI.tileButtons.Add(startPosButton);

                }
                return _instance;
            }

        }

        public override void Update(GameTime gameTime, Screen screen, Editor editor)
        {

            base.Update(gameTime, screen, editor);


            for (int i = 0; i < tileButtons.Count; i++)
            {

                tileButtons[i].Update(gameTime, screen);

                /*if (tileButtons[i].IsSelected())
                    tileButtons[i].SetColor(Color.Gray, Color.Gray);
                else
                    tileButtons[i].SetColor(Color.White, Color.Black);*/

                if (tileButtons[i].IsSelected())
                    tileButtons[i].SetOutlineColor(Color.White);
                else
                    tileButtons[i].SetOutlineColor(Color.Black);

                if (tileButtons[i].IsReleased())
                {
                    if(i == tileButtons.Count - 1)
                        Editor.currentBlock = EditorTile.BlockID.start_pos;
                    else
                        Editor.currentBlock = (EditorTile.BlockID)i + 1;
                    tileButtons[i].SetOutlineColor(Color.White);

                    Editor.brush = Editor.Brush.tile;
                    
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

                for (int i = 0; i < tileButtons.Count; i++)
                {

                    if (i < 12)
                        tileButtons[i].SetPosition(Position.X + 80 * i + 20, Position.Y + 120 + tileButtonPosition.Y);
                    else if (i >= 7)
                        tileButtons[i].SetPosition(Position.X + 80 * (i - 7) + 20, Position.Y + 200 + tileButtonPosition.Y);

                    if (tileButtons[i].GetPosition().Y + 64 > Position.Y + 100)
                        tileButtons[i].Draw(spriteBatch);

                }

                spriteBatch.Draw(Main.Bounds, new Rectangle((int)Position.X, (int)Position.Y, Dimension.Width, 96), new Color(110, 110, 110));
                spriteBatch.Draw(Main.Bounds, new Rectangle((int)Position.X, (int)Position.Y + Dimension.Height - 100, Dimension.Width, 100), new Color(110, 110, 110));

                Writer.DrawText(Main.UltimateFont, "BLOCKS", new Vector2(1920 / 2 - Main.UltimateFont.MeasureString("BLOCKS").ToPoint().X / 2 * 4, Position.Y + 15), new Color(60, 60, 60), Color.White, 0f, Vector2.Zero, 4f, SpriteEffects.None, 0f, 4f, spriteBatch, Color.Black, false);

                DrawCloseButton(spriteBatch);

            }

        }

        public void InitLevelsButtons()
        {
            tileButtonPosition.Y = 0;
        }


    }
}
