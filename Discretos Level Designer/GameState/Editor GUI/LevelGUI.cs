using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discretos_Level_Designer.GUI
{
    public class LevelGUI : MessageBoxV2
    {

        private static LevelGUI _instance;

        private static List<LevelButton> levelButtons = new List<LevelButton>();
        private static Vector2 levelButtonPosition;

        private static int scrollValue;



        private LevelGUI() : base()
        {
            
        }

        public static LevelGUI GUI
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new LevelGUI();

                    GUI.Dimension = new Rectangle(0, 0, 1600, 800);
                    GUI.SetPositionInCenter(1920, 1080);

                    GUI.AddCloseButton(new Vector2(GUI.Position.X + GUI.Dimension.Width - 70, GUI.Position.Y + 10));


                }
                return _instance;
            }
        }

        public override void Update(GameTime gameTime, Screen screen, Editor editor)
        {

            base.Update(gameTime, screen, editor);


            for (int i = 0; i < levelButtons.Count; i++)
            {

                if (i % 2 == 0)
                    levelButtons[i].SetPositionWithoutReInit(new Vector2(Position.X + (80 / 3), Position.Y + 100 + i * 50) + levelButtonPosition);
                else
                    levelButtons[i].SetPositionWithoutReInit(new Vector2(Position.X + 190 * 4 + (80 / 3) * 2, Position.Y + 100 + (i - 1) * 50) + levelButtonPosition);

                levelButtons[i].Load.Update(gameTime, screen);
                levelButtons[i].Delete.Update(gameTime, screen);

                bool flag1 = (!MouseInput.GetRectangle(screen).Intersects(new Rectangle((int)Position.X, (int)Position.Y, Dimension.Width, 96)) && !MouseInput.GetRectangle(screen).Intersects(new Rectangle((int)Position.X, (int)Position.Y + Dimension.Height - 100, Dimension.Width, 100)));


                if (levelButtons[i].Load.IsSelected() && flag1)
                    levelButtons[i].Load.SetColor(Color.Gray, Color.Gray);
                else
                    levelButtons[i].Load.SetColor(Color.White, Color.Black);

                if (levelButtons[i].Load.IsReleased() && flag1)
                {
                    string levelPath = LevelManager.Levels[i].Name;
                    LevelManager.LoadLevel(levelPath);
                    editor.InitCameraPosition();
                    isActive = false;

                    //LevelNameButton.box.SetText(level);

                }


                if (levelButtons[i].Delete.IsSelected() && flag1)
                    levelButtons[i].Delete.SetColor(Color.Gray, Color.Gray);
                else
                    levelButtons[i].Delete.SetColor(Color.White, Color.Black);


            }

            if (KeyInput.getKeyState().IsKeyDown(Keys.Up) && levelButtonPosition.Y < 0)
                levelButtonPosition.Y += 4f;
            if (KeyInput.getKeyState().IsKeyDown(Keys.Down))
                levelButtonPosition.Y -= 4f;

            scrollValue = MouseInput.getMouseState().ScrollWheelValue - MouseInput.getOldMouseState().ScrollWheelValue;

            levelButtonPosition.Y += scrollValue / 5;

            if (levelButtonPosition.Y > 0)
                levelButtonPosition.Y = 0;



        }

        public override void Draw(SpriteBatch spriteBatch)
        {

            base.Draw(spriteBatch);

            if (isActive)
            {

                for (int i = 0; i < levelButtons.Count; i++)
                {
                    if (levelButtons[i].Position.Y > Position.Y && levelButtons[i].Position.Y < Position.Y + Dimension.Height - 96)
                        levelButtons[i].Draw(spriteBatch, Color.White);
                }

                spriteBatch.Draw(Main.Bounds, new Rectangle((int)Position.X, (int)Position.Y, Dimension.Width, 96), new Color(110, 110, 110));
                spriteBatch.Draw(Main.Bounds, new Rectangle((int)Position.X, (int)Position.Y + Dimension.Height - 100, Dimension.Width, 100), new Color(110, 110, 110));

                Writer.DrawText(Main.UltimateFont, "LEVELS", new Vector2(1920 / 2 - Main.UltimateFont.MeasureString("LEVELS").ToPoint().X / 2 * 4, Position.Y + 15), new Color(60, 60, 60), Color.White, 0f, Vector2.Zero, 4f, SpriteEffects.None, 0f, 4f, spriteBatch, Color.Black, false);

                DrawCloseButton(spriteBatch);

            }

        }


        public void InitLevelsButtons()
        {

            levelButtons.Clear();
            levelButtonPosition.Y = 0;

            for (int i = 0; i < LevelManager.Levels.Count; i++)
            {

                LevelDataSet level = LevelManager.Levels[i];

                levelButtons.Add(new LevelButton(new Vector2(0, 0), level.Name.ToLower(), level.Date, level.FileSize));

                if (i % 2 == 0)
                    levelButtons[i].SetPositionWithoutReInit(new Vector2(Position.X + (80 / 3), Position.Y + 100 + i * 50) + levelButtonPosition);
                else
                    levelButtons[i].SetPositionWithoutReInit(new Vector2(Position.X + 190 * 4 + (80 / 3) * 2, Position.Y + 100 + (i - 1) * 50) + levelButtonPosition);


            }

        }


    }
}
