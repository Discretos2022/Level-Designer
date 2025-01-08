using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discretos_Level_Designer.GUI
{
    public class InfoGUI : MessageBoxV2
    {

        private static InfoGUI _instance;

        private Vector2 GridPosition;

        private ButtonV3 AddLeft;
        private ButtonV3 AddRight;
        private ButtonV3 AddUp;
        private ButtonV3 AddDown;

        private ButtonV3 RemoveLeft;
        private ButtonV3 RemoveRight;
        private ButtonV3 RemoveUp;
        private ButtonV3 RemoveDown;

        private ButtonV3 Load;
        private ButtonV3 Save;
        private ButtonV3 SaveAs;

        private string text = "";
        private Vector2 textPos;

        public TextButton LevelNameButton;


        private InfoGUI() : base()
        {
            
        }

        public static InfoGUI GUI
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new InfoGUI();

                    GUI.Dimension = new Rectangle(0, 0, 1000, 800);
                    GUI.SetPositionInCenter(1920, 1080);
                    //CloseBox.SetPosition(GUI.Position.X + GUI.Dimension.Width - 70, GUI.Position.Y + 10);
                    GUI.AddCloseButton(new Vector2(GUI.Position.X + GUI.Dimension.Width - 70, GUI.Position.Y + 10));

                    GUI.AddLeft = new ButtonV3();
                    GUI.AddRight = new ButtonV3();
                    GUI.AddUp = new ButtonV3();
                    GUI.AddDown = new ButtonV3();
                    GUI.RemoveLeft = new ButtonV3();
                    GUI.RemoveRight = new ButtonV3();
                    GUI.RemoveUp = new ButtonV3();
                    GUI.RemoveDown = new ButtonV3();

                    GUI.Load = new ButtonV3();
                    GUI.Save = new ButtonV3();
                    GUI.SaveAs = new ButtonV3();


                    #region AddButton and RemoveButton

                    GUI.AddLeft.SetTexture(Main.ButtonTexture[3], new Rectangle(0, 0, 16, 16));
                    GUI.AddLeft.SetColor(Color.White, Color.Black);
                    GUI.AddLeft.SetFont(Main.UltimateFont);
                    GUI.AddLeft.SetScale(4);
                    GUI.AddLeft.SetPosition(GUI.Position.X + GUI.Dimension.Width - 70, GUI.Position.Y + GUI.Dimension.Height - 70);

                    GUI.AddRight.SetTexture(Main.ButtonTexture[3], new Rectangle(0, 0, 16, 16));
                    GUI.AddRight.SetColor(Color.White, Color.Black);
                    GUI.AddRight.SetFont(Main.UltimateFont);
                    GUI.AddRight.SetScale(4);
                    GUI.AddRight.SetPosition(GUI.Position.X + GUI.Dimension.Width - 70, GUI.Position.Y + GUI.Dimension.Height - 70);
                    
                    GUI.AddUp.SetTexture(Main.ButtonTexture[3], new Rectangle(0, 0, 16, 16));
                    GUI.AddUp.SetColor(Color.White, Color.Black);
                    GUI.AddUp.SetFont(Main.UltimateFont);
                    GUI.AddUp.SetScale(4);
                    GUI.AddUp.SetPosition(GUI.Position.X + GUI.Dimension.Width - 70, GUI.Position.Y + GUI.Dimension.Height - 70);
                    
                    GUI.AddDown.SetTexture(Main.ButtonTexture[3], new Rectangle(0, 0, 16, 16));
                    GUI.AddDown.SetColor(Color.White, Color.Black);
                    GUI.AddDown.SetFont(Main.UltimateFont);
                    GUI.AddDown.SetScale(4);
                    GUI.AddDown.SetPosition(GUI.Position.X + GUI.Dimension.Width + 70, GUI.Position.Y + GUI.Dimension.Height - 70);
                    


                    GUI.RemoveLeft.SetTexture(Main.ButtonTexture[4], new Rectangle(0, 0, 16, 16));
                    GUI.RemoveLeft.SetColor(Color.White, Color.Black);
                    GUI.RemoveLeft.SetFont(Main.UltimateFont);
                    GUI.RemoveLeft.SetScale(4);
                    GUI.RemoveLeft.SetPosition(GUI.Position.X + GUI.Dimension.Width - 70, GUI.Position.Y + GUI.Dimension.Height - 70);
                    
                    GUI.RemoveRight.SetTexture(Main.ButtonTexture[4], new Rectangle(0, 0, 16, 16));
                    GUI.RemoveRight.SetColor(Color.White, Color.Black);
                    GUI.RemoveRight.SetFont(Main.UltimateFont);
                    GUI.RemoveRight.SetScale(4);
                    GUI.RemoveRight.SetPosition(GUI.Position.X + GUI.Dimension.Width - 70, GUI.Position.Y + GUI.Dimension.Height - 70);
                    
                    GUI.RemoveUp.SetTexture(Main.ButtonTexture[4], new Rectangle(0, 0, 16, 16));
                    GUI.RemoveUp.SetColor(Color.White, Color.Black);
                    GUI.RemoveUp.SetFont(Main.UltimateFont);
                    GUI.RemoveUp.SetScale(4);
                    GUI.RemoveUp.SetPosition(GUI.Position.X + GUI.Dimension.Width - 70, GUI.Position.Y + GUI.Dimension.Height - 70);

                    GUI.RemoveDown.SetTexture(Main.ButtonTexture[4], new Rectangle(0, 0, 16, 16));
                    GUI.RemoveDown.SetColor(Color.White, Color.Black);
                    GUI.RemoveDown.SetFont(Main.UltimateFont);
                    GUI.RemoveDown.SetScale(4);
                    GUI.RemoveDown.SetPosition(GUI.Position.X + GUI.Dimension.Width - 70, GUI.Position.Y + GUI.Dimension.Height - 70);

                    #endregion


                    GUI.Load.SetTexture(Main.ButtonTexture[2], new Rectangle(0, 0, 16, 16));
                    GUI.Load.SetColor(Color.White, Color.Black);
                    GUI.Load.SetFont(Main.UltimateFont);
                    GUI.Load.SetScale(4);
                    GUI.Load.SetPosition(GUI.Position.X + GUI.Dimension.Width - 80, GUI.Position.Y + GUI.Dimension.Height - 80);

                    GUI.Save.SetTexture(Main.ButtonTexture[1], new Rectangle(0, 0, 16, 16));
                    GUI.Save.SetColor(Color.White, Color.Black);
                    GUI.Save.SetFont(Main.UltimateFont);
                    GUI.Save.SetScale(4);
                    GUI.Save.SetPosition(GUI.Position.X + GUI.Dimension.Width - 160, GUI.Position.Y + GUI.Dimension.Height - 80);

                    GUI.SaveAs.SetTexture(Main.ButtonTexture[15], new Rectangle(0, 0, 16, 16));
                    GUI.SaveAs.SetColor(Color.White, Color.Black);
                    GUI.SaveAs.SetFont(Main.UltimateFont);
                    GUI.SaveAs.SetScale(4);
                    GUI.SaveAs.SetPosition(GUI.Position.X + GUI.Dimension.Width - 240, GUI.Position.Y + GUI.Dimension.Height - 80);

                    GUI.LevelNameButton = new TextButton(new Vector2(0, 0), 25, 4f, 4f, false, "enter level name", false, false);


                }
                return _instance;
            }
        }

        public override void Update(GameTime gameTime, Screen screen, Editor editor)
        {

            base.Update(gameTime, screen, editor);

            GridPosition = new Vector2(Position.X + 100, Position.Y + 506);


            AddLeft.SetPosition(GridPosition.X - 12 - 64, GridPosition.Y + 20);
            RemoveLeft.SetPosition(GridPosition.X - 12 - 64, GridPosition.Y + 192 - 64 - 20);

            AddRight.SetPosition(GridPosition.X + 204, GridPosition.Y + 20);
            RemoveRight.SetPosition(GridPosition.X + 204, GridPosition.Y + 192 - 64 - 20);

            AddUp.SetPosition(GridPosition.X + 192 - 64 - 20, GridPosition.Y - 12 - 64);
            RemoveUp.SetPosition(GridPosition.X + 20, GridPosition.Y - 12 - 64);

            AddDown.SetPosition(GridPosition.X + 192 - 64 - 20, GridPosition.Y + 204);
            RemoveDown.SetPosition(GridPosition.X + 20, GridPosition.Y + 204);


            AddLeft.Update(gameTime, screen);
            RemoveLeft.Update(gameTime, screen);

            AddRight.Update(gameTime, screen);
            RemoveRight.Update(gameTime, screen);

            AddUp.Update(gameTime, screen);
            RemoveUp.Update(gameTime, screen);

            AddDown.Update(gameTime, screen);
            RemoveDown.Update(gameTime, screen);

            Load.Update(gameTime, screen);
            Save.Update(gameTime, screen);
            SaveAs.Update(gameTime, screen);


            #region AddButtons

            if (isActive)
            {

                if (AddLeft.IsSelected())
                    AddLeft.SetColor(Color.Gray, Color.Gray);
                else
                    AddLeft.SetColor(Color.White, Color.Black);

                if (AddLeft.IsCliqued())
                { Handler.AddBlockLineLeft(); }


                if (AddRight.IsSelected())
                    AddRight.SetColor(Color.Gray, Color.Gray);
                else
                    AddRight.SetColor(Color.White, Color.Black);

                if (AddRight.IsCliqued())
                { Handler.AddBlockLineRight(); }


                if (AddUp.IsSelected())
                    AddUp.SetColor(Color.Gray, Color.Gray);
                else
                    AddUp.SetColor(Color.White, Color.Black);

                if (AddUp.IsCliqued())
                { Handler.AddBlockLineUp(); }


                if (AddDown.IsSelected())
                    AddDown.SetColor(Color.Gray, Color.Gray);
                else
                    AddDown.SetColor(Color.White, Color.Black);

                if (AddDown.IsCliqued())
                { Handler.AddBlockLineDown(); }

            }

            #endregion

            #region RemoveButtons

            if (isActive)
            {

                if (RemoveLeft.IsSelected())
                    RemoveLeft.SetColor(Color.Gray, Color.Gray);
                else
                    RemoveLeft.SetColor(Color.White, Color.Black);

                if (Handler.tiles.GetLength(0) == 30)
                    RemoveLeft.SetColor(Color.Gray * 0.5f, Color.White);

                if (RemoveLeft.IsCliqued() && Handler.tiles.GetLength(0) > 30)
                { Handler.RemoveBlockLineLeft(); }


                if (RemoveRight.IsSelected())
                    RemoveRight.SetColor(Color.Gray, Color.Gray);
                else
                    RemoveRight.SetColor(Color.White, Color.Black);

                if (Handler.tiles.GetLength(0) == 30)
                    RemoveRight.SetColor(Color.Gray * 0.5f, Color.White);

                if (RemoveRight.IsCliqued() && Handler.tiles.GetLength(0) > 30)
                { Handler.RemoveBlockLineRight(); }


                if (RemoveUp.IsSelected())
                    RemoveUp.SetColor(Color.Gray, Color.Gray);
                else
                    RemoveUp.SetColor(Color.White, Color.Black);

                if (Handler.tiles.GetLength(1) == 15)
                    RemoveUp.SetColor(Color.Gray * 0.5f, Color.White);

                if (RemoveUp.IsCliqued() && Handler.tiles.GetLength(1) > 15)
                { Handler.RemoveBlockLineUp(); }


                if (RemoveDown.IsSelected())
                    RemoveDown.SetColor(Color.Gray, Color.Gray);
                else
                    RemoveDown.SetColor(Color.White, Color.Black);

                if (Handler.tiles.GetLength(1) == 15)
                    RemoveDown.SetColor(Color.Gray * 0.5f, Color.White);

                if (RemoveDown.IsCliqued() && Handler.tiles.GetLength(1) > 15)
                { Handler.RemoveBlockLineDown(); }
            }

            #endregion


            text = "";
            textPos = new Vector2(MouseInput.GetRectangle(screen).X, MouseInput.GetRectangle(screen).Y);

            if (isActive)
            {

                if (Load.IsSelected())
                {
                    Load.SetColor(Color.Gray, Color.Gray);
                    text = "load";
                }
                else
                    Load.SetColor(Color.White, Color.Black);

                if (Load.IsCliqued())
                { 
                    LevelGUI.GUI.isActive = true;
                    LevelManager.LoadLevelDataSets();
                    LevelGUI.GUI.InitLevelsButtons();
                    isActive = false;
                }

                if (Save.IsSelected())
                {
                    Save.SetColor(Color.Gray, Color.Gray);
                    text = "save";
                }
                else
                    Save.SetColor(Color.White, Color.Black);

                if(!LevelManager.isLoadedLevel)
                    Save.SetColor(Color.Gray * 0.5f, Color.White);

                if (Save.IsCliqued() && LevelManager.isLoadedLevel)
                {
                    LevelManager.SaveLevel("level 2");
                    isActive = false;
                }

                if (SaveAs.IsSelected())
                {
                    SaveAs.SetColor(Color.Gray, Color.Gray);
                    text = "save as";
                }
                else
                    SaveAs.SetColor(Color.White, Color.Black);

                if (SaveAs.IsCliqued())
                {
                    LevelManager.SaveAsLevel(LevelNameButton.box.GetText());
                    isActive = false;
                }

                LevelNameButton.Update(gameTime, screen);

                if (LevelNameButton.button.IsCliqued())
                {
                    TextInputManager.InitText();
                }

                if (LevelNameButton.isActive)
                {

                    string str = LevelNameButton.box.GetText() + TextInputManager.GetText();

                    for (int i = 1; i < str.Length; i++)
                    {
                        if (str[i] == '\b')
                        {
                            str = str.Remove(i);
                            str = str.Remove(i - 1);
                        }
                            
                    }

                    LevelNameButton.box.SetText(str);

                }

            }


        }

        public override void Draw(SpriteBatch spriteBatch)
        {

            base.Draw(spriteBatch);

            if (isActive)
            {
                spriteBatch.Draw(Main.Bounds, new Rectangle((int)Position.X, (int)Position.Y, Dimension.Width, 96), new Color(110, 110, 110));

                Writer.DrawText(Main.UltimateFont, "LEVEL INFO", new Vector2(1920 / 2 - Main.UltimateFont.MeasureString("LEVEL INFO").ToPoint().X / 2 * 4, Position.Y + 15), new Color(60, 60, 60), Color.White, 0f, Vector2.Zero, 4f, SpriteEffects.None, 0f, 4f, spriteBatch, Color.Black, false);
                spriteBatch.Draw(Main.Grid, new Vector2(GridPosition.X, GridPosition.Y), null, Color.White, 0f, Vector2.Zero, 6f, SpriteEffects.None, 0f);

                AddLeft.Draw(spriteBatch);
                RemoveLeft.Draw(spriteBatch);

                AddRight.Draw(spriteBatch);
                RemoveRight.Draw(spriteBatch);

                AddUp.Draw(spriteBatch);
                RemoveUp.Draw(spriteBatch);

                AddDown.Draw(spriteBatch);
                RemoveDown.Draw(spriteBatch);

                Load.Draw(spriteBatch);
                Save.Draw(spriteBatch);
                SaveAs.Draw(spriteBatch);

                /// Level Name Bar
                LevelNameButton.SetPosition(new Vector2(1920 / 2 - (190 * 4) / 2, Position.Y + 100));
                LevelNameButton.Draw(spriteBatch);

                // new Vector2(GUI.Position.X + GUI.Dimension.Width - 240, GUI.Position.Y + GUI.Dimension.Height - 80)
                if (text != "")
                    Writer.DrawText(Main.UltimateFont, text, textPos + new Vector2(20, 20), Color.Black, Color.White, 0f, Vector2.Zero, 3f, SpriteEffects.None, 0f, 3f, spriteBatch, false);

                DrawCloseButton(spriteBatch);

            }

        }

    }
}
