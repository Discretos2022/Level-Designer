using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Discretos_Level_Designer.NetCore;
using System;
using System.Collections.Generic;
using SiedelGUI;

namespace Discretos_Level_Designer
{
    class Menu
    {
        private Main main;

        private ButtonV3 SinglePlayer2;
        private ButtonV3 MultiPlayer2;
        private ButtonV3 Editor;
        private ButtonV3 Settings2;
        private ButtonV3 Quit2;

        private List<ButtonV3> buttons2 = new List<ButtonV3>();

        private Color grayColor = new Color(60, 60, 60);

        private ButtonV4 buttonV4 = new ButtonV4();


        public Menu(Main main)
        {

            this.main = main;

            SinglePlayer2 = new ButtonV3();
            MultiPlayer2 = new ButtonV3();
            Settings2 = new ButtonV3();
            Editor = new ButtonV3();
            Quit2 = new ButtonV3();

            buttons2.Add(SinglePlayer2);
            buttons2.Add(MultiPlayer2);
            buttons2.Add(Settings2);
            buttons2.Add(Editor);
            buttons2.Add(Quit2);

            ButtonManager.mainMenuButtons = buttons2;

            InitButton();

            buttonV4.SetTexture(Main.ButtonTexture[2]);
            buttonV4.SetScale(4f);

        }

        public void Update(GameState state, GameTime gameTime, Screen screen)
        {

            #region SinglePlayerButton2

            SinglePlayer2.Update(gameTime, screen);

            if (SinglePlayer2.IsSelected())
                SinglePlayer2.SetColor(Color.Gray, grayColor);
            else
                SinglePlayer2.SetColor(Color.White, grayColor);

            if (SinglePlayer2.IsCliqued())
            {

                Handler.InitPlayersList();
                Handler.AddPlayerV2(1);

                Main.inWorldMap = true;
                Main.inLevel = false;
                Camera.Zoom = 1f;
                Main.gameState = GameState.Playing;
                NetPlay.IsMultiplaying = false;

            }

            #endregion


            #region MultiPlayerButton2

            MultiPlayer2.Update(gameTime, screen);

            if (MultiPlayer2.IsSelected())
                MultiPlayer2.SetColor(Color.Red, grayColor);
            else
                MultiPlayer2.SetColor(Color.White, grayColor);

            if (MultiPlayer2.IsCliqued())
            {
                //MultiPlayer2.SetText("not exist");
                Background.SetBackground(3);
                Main.gameState = GameState.MultiplayerMode;
            }

            #endregion


            #region EditorButton2

            Editor.Update(gameTime, screen);

            if (Editor.IsSelected())
                Editor.SetColor(Color.Aquamarine, grayColor);
            else
                Editor.SetColor(Color.White, grayColor);

            if (Editor.IsCliqued())
            {
                //MultiPlayer2.SetText("not exist");
                Background.SetBackground(3);
                Main.gameState = GameState.Editor;

                Handler.InitEditorGrid();

            }

            #endregion


            #region SettingsButton2

            Settings2.Update(gameTime, screen);

            if (Settings2.IsSelected())
                Settings2.SetColor(Color.Blue, grayColor);
            else
                Settings2.SetColor(Color.White, grayColor);

            if (Settings2.IsCliqued())
            {
                Main.gameState = GameState.Settings;
                Background.SetBackground(3);

                if (!MouseInput.IsActived)
                    ButtonManager.settingsButtons[0].SetIsSelected(true);

            }


            #endregion


            #region QuitButton2

            Quit2.Update(gameTime, screen);

            if (Quit2.IsSelected())
                Quit2.SetColor(Color.Gold, grayColor);
            else
                Quit2.SetColor(Color.White, grayColor);

            if (Quit2.IsCliqued())
                main.QuitGame();

            #endregion


            if (KeyInput.isSimpleClick(Keys.Up, Keys.Down) || GamePadInput.isSimpleClick(PlayerIndex.One, Buttons.DPadUp, Buttons.DPadDown))
            {
                for (int i = 0; i < buttons2.Count; i++)
                {
                    if (buttons2[i].IsSelected())
                        goto L_2;

                }

                SinglePlayer2.SetIsSelected(true);
                MouseInput.IsActived = false;

            L_2:;
            }

            //NetPlay.IsMultiplaying = false;

            buttonV4.Update(MouseInput.GetScreenPosition(screen));

            if (buttonV4.IsCliqued())
            {
                Background.SetBackground(3);
                Main.gameState = GameState.Editor;
                Handler.InitEditorGrid();
            }

            buttonV4.SetAlignment(ButtonV4.Alignment.Center);
            buttonV4.SetPosition(new Vector2(0, 100));

            ButtonV4.ScreenWidth = 1920;
            ButtonV4.ScreenHeight = 1080;

        }

        public void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(Main.Banner, new Vector2(1920 / 2 - Main.Banner.Width * 8 / 2, 25), null, Color.White, 0f, new Vector2(0, 0), 8f, SpriteEffects.None, 0f);
            //spriteBatch.Draw(Main.Screens[1], new Vector2(0, 0), null, Color.White, 0f, new Vector2(0, 0), 4f, SpriteEffects.None, 0f);

            Writer.DrawText(Main.UltimateFont, "version " + Main.Version + " build " + Main.Build + " " + Main.State + " " + Main.Platform, new Vector2(10, 1040), Color.Black, Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f, 2f, spriteBatch);
            Writer.DrawText(Main.UltimateFont, "ip : " + Main.IP, new Vector2(1640, 1040), Color.Black, Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f, 2f, spriteBatch);


            for (int i = 0; i < buttons2.Count; i++)
            {
                buttons2[i].Draw(spriteBatch, false, true, Color.Black);
            }


            //Writer.DrawText(Main.ScoreFont, "1234567890", new Vector2(10, 800), Color.Black, Color.White, 0f, Vector2.Zero, 5f, SpriteEffects.None, 0f, 4f, spriteBatch);

            

            /// ***** Test ButtonV4 ***** ///
            if (buttonV4.IsCliqued())
                buttonV4.Draw(spriteBatch, Color.Blue);
            else if (buttonV4.IsReleased())
                buttonV4.Draw(spriteBatch, Color.Green);
            else if (buttonV4.IsSelected())
                buttonV4.Draw(spriteBatch, Color.Red);
            else
                buttonV4.Draw(spriteBatch, Color.White);


        }

        public void InitButton()
        {
            SinglePlayer2.SetText("single player");
            SinglePlayer2.SetColor(Color.White, Color.Black);
            SinglePlayer2.SetFont(Main.UltimateFont);
            SinglePlayer2.SetScale(5);
            SinglePlayer2.IsMajuscule(false);
            SinglePlayer2.SetFrontThickness(4);
            SinglePlayer2.SetAroundButton(null, MultiPlayer2);

            MultiPlayer2.SetText("multiplayer");
            MultiPlayer2.SetColor(Color.White, Color.Black);
            MultiPlayer2.SetFont(Main.UltimateFont);
            MultiPlayer2.SetScale(5);
            MultiPlayer2.IsMajuscule(false);
            MultiPlayer2.SetFrontThickness(4);
            MultiPlayer2.SetAroundButton(SinglePlayer2, Editor);

            Editor.SetText("editor");
            Editor.SetColor(Color.White, Color.Black);
            Editor.SetFont(Main.UltimateFont);
            Editor.SetScale(5);
            Editor.IsMajuscule(false);
            Editor.SetFrontThickness(4);
            Editor.SetAroundButton(MultiPlayer2, Settings2);

            Settings2.SetText("settings");
            Settings2.SetColor(Color.White, Color.Black);
            Settings2.SetFont(Main.UltimateFont);
            Settings2.SetScale(5);
            Settings2.IsMajuscule(false);
            Settings2.SetFrontThickness(4);
            Settings2.SetAroundButton(Editor, Quit2);

            Quit2.SetText("quit");
            Quit2.SetColor(Color.White, new Color(60,60,60));
            Quit2.SetFont(Main.UltimateFont);
            Quit2.SetScale(5);
            Quit2.IsMajuscule(false);
            Quit2.SetFrontThickness(4);
            Quit2.SetAroundButton(Settings2, null);
            

            SinglePlayer2.SetPosition(0, 440, ButtonV3.Position.centerX);
            MultiPlayer2.SetPosition(0, 540, ButtonV3.Position.centerX);
            Settings2.SetPosition(0, 640, ButtonV3.Position.centerX);
            Editor.SetPosition(0, 740, ButtonV3.Position.centerX);
            Quit2.SetPosition(0, 840, ButtonV3.Position.centerX);

        }

    }
}