using Discretos_Level_Designer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Text;

namespace Discretos_Level_Designer
{
    class State
    {

        public SpriteBatch spriteBatch;
        private Main main;
        private Menu menu;
        private Play play;
        private Settings settings;
        private MultiplayerMode multiplayerMode;
        private CreateServer createServer;
        private ConnectServer connectServer;
        private Editor editor;

        public State(SpriteBatch spriteBatch, Menu menu, Settings settings, Play play, MultiplayerMode multiplayerMode, CreateServer createServer, ConnectServer connectServer, Editor editor, Main game)
        {
            this.spriteBatch = spriteBatch;
            this.main = game;
            this.menu = menu;
            this.play = play;
            this.settings = settings;
            this.multiplayerMode = multiplayerMode;
            this.createServer = createServer;
            this.connectServer = connectServer;
            this.editor = editor;
        }


        public void Update(GameState state, GameTime gameTime, Screen screen, Camera camera, Main main)
        {
            switch (state)
            {
                case GameState.Menu:
                    menu.Update(state, gameTime, screen);
                    break;
                case GameState.Settings:
                    settings.Update(state, gameTime, screen, main);
                    break;
                case GameState.Playing:
                    play.Update(state, gameTime, screen);
                    break;
                case GameState.MultiplayerMode:
                    multiplayerMode.Update(state, gameTime, screen);
                    break;
                case GameState.CreateServer:
                    createServer.Update(state, gameTime, screen);
                    break;
                case GameState.ConnectToServer:
                    connectServer.Update(state, gameTime, screen);
                    break;
                case GameState.Editor:
                    editor.Update(state, gameTime, screen, camera);
                    break;


            }
        }

        public void Draw(SpriteBatch spriteBatch, GameState state, GameTime gameTime)
        {
            switch (state)
            {
                case GameState.Menu:
                    menu.Draw(spriteBatch);
                    break;
                case GameState.Settings:
                    settings.Draw(spriteBatch, gameTime);
                    break;
                case GameState.Playing:
                    play.Draw(spriteBatch, gameTime);
                    break;
                case GameState.MultiplayerMode:
                    multiplayerMode.Draw(spriteBatch, gameTime);
                    break;
                case GameState.CreateServer:
                    createServer.Draw(spriteBatch, gameTime);
                    break;
                case GameState.ConnectToServer:
                    connectServer.Draw(spriteBatch, gameTime);
                    break;
                case GameState.Editor:
                    editor.Draw(spriteBatch, gameTime);
                    break;

            }


        }

        public void DrawInCamera(SpriteBatch spriteBatch, GameState state, GameTime gameTime)
        {
            switch (state)
            {
                case GameState.Menu:
                    break;
                case GameState.Settings:
                    break;
                case GameState.Playing:
                    play.DrawInCamera(spriteBatch, gameTime);
                    break;
                case GameState.MultiplayerMode:
                    break;
                case GameState.CreateServer:
                    break;
                case GameState.ConnectToServer:
                    break;
                case GameState.Editor:
                    editor.DrawInCamera(spriteBatch, gameTime);
                    break;
                    

            }
        }


    }

    public enum GameState { Menu, Settings, Playing, MultiplayerMode, CreateServer, ConnectToServer, Editor }

    public enum PlayState { InWorldMap, InLevel }

}
