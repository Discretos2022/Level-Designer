﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NetworkEngine_5._0.Client;
using NetworkEngine_5._0.Error;
using Discretos_Level_Designer.NetCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Discretos_Level_Designer
{
    class ConnectServer
    {

        public List<ButtonV3> connectionButtons;

        private ButtonV3 Connect;
        private ButtonV3 Back;

        public static bool IsConnected;

        public TextBox textBoxIP;
        public TextBox textBoxPort;

        public ButtonV3 PortButton;
        public ButtonV3 IPButton;

        private State clientState = State.Connection;

        private int timeOut = 0;
        public bool connection = false;

        private int animTime = 0;
        private string stateText = "";
        private Color stateTextColor = Color.White;

        public ConnectServer()
        {

            connectionButtons = new List<ButtonV3>();

            Connect = new ButtonV3();
            Back = new ButtonV3();

            PortButton = new ButtonV3();
            IPButton = new ButtonV3();

            InitButton();

            connectionButtons.Add(Connect);
            connectionButtons.Add(Back);
            connectionButtons.Add(PortButton);
            connectionButtons.Add(IPButton);

            textBoxIP = new TextBox(15, 4, 4, false, "192.168.1.25", true, false);
            textBoxIP.SetPosition(0, 252, ButtonV3.Position.centerX);

            textBoxPort = new TextBox(5, 4, 4, false, "7777", true, true);
            textBoxPort.SetPosition(0, 402, ButtonV3.Position.centerX);
            textBoxPort.Update();

        }


        public void Update(GameState state, GameTime gameTime, Screen screen)
        {

            if (clientState == State.Connection)
            {
                #region ConnectButton

                Connect.Update(gameTime, screen);

                if (Connect.IsSelected())
                    Connect.SetColor(Color.Gray, Color.Black);
                else
                    Connect.SetColor(Color.White, Color.Black);

                if (!IsValidIP(textBoxIP.GetText()) || !IsValidPort(textBoxPort.GetText()) || Client.GetState() == Client.ClientState.Connecting)
                    Connect.SetColor(Color.DarkGray, Color.Gray);  // 0.6f       0.2f

                if (IsValidIP(textBoxIP.GetText()) && IsValidPort(textBoxPort.GetText()) && Client.GetState() == Client.ClientState.Disconnected)
                    if (Connect.IsCliqued())
                    {

                        if (textBoxPort.GetText() == "")
                            Connection(textBoxIP.GetText(), int.Parse("7777")); 
                        else
                            Connection(textBoxIP.GetText(), int.Parse(textBoxPort.GetText()));

                    }

                if (Client.GetState() == Client.ClientState.Connected)
                {
                    clientState = State.WaitPlayer;
                    NetPlay.IsMultiplaying = true;
                }

                #endregion

                #region BackButton

                Back.Update(gameTime, screen);

                if (Back.IsSelected())
                    Back.SetColor(Color.Gray, Color.Black);
                else
                    Back.SetColor(Color.White, Color.Black);

                if (Back.IsCliqued())
                    Main.gameState = GameState.MultiplayerMode;

                #endregion

                #region PortButton

                PortButton.Update(gameTime, screen);

                if (PortButton.IsSelected())
                    PortButton.SetTexture(Main.PortBox, new Rectangle(53, 0, 52, 16));
                else
                {
                    if (textBoxPort.isSelected)
                        PortButton.SetTexture(Main.PortBox, new Rectangle(106, 0, 52, 16));
                    else
                        PortButton.SetTexture(Main.PortBox, new Rectangle(0, 0, 52, 16));
                }


                if (PortButton.IsCliqued())
                { textBoxPort.isSelected = true; textBoxIP.isSelected = false; }

                #endregion

                #region IPButton

                IPButton.Update(gameTime, screen);

                if (IPButton.IsSelected())
                    IPButton.SetTexture(Main.IPBox, new Rectangle(121, 0, 120, 16));
                else
                {
                    if (textBoxIP.isSelected)
                        IPButton.SetTexture(Main.IPBox, new Rectangle(242, 0, 120, 16));
                    else
                        IPButton.SetTexture(Main.IPBox, new Rectangle(0, 0, 120, 16));
                }


                if (IPButton.IsCliqued())
                { textBoxIP.isSelected = true; textBoxPort.isSelected = false; }

                #endregion

                if (textBoxIP.isSelected)
                    textBoxIP.Update();
                if (textBoxPort.isSelected)
                    textBoxPort.Update();

                if (MouseInput.isSimpleClickLeft())
                {
                    if (!IPButton.IsSelected())
                        textBoxIP.isSelected = false;
                    if (!PortButton.IsSelected())
                        textBoxPort.isSelected = false;
                }

                if (textBoxPort.GetText() != "")
                {
                    if (int.Parse(textBoxPort.GetText()) > Main.MaxPort)
                        textBoxPort.SetColor(Color.Red, Color.Black);
                    else
                        textBoxPort.SetColor(Color.White, Color.Black);
                }

                if (!IsValidIP(textBoxIP.GetText()))
                    textBoxIP.SetColor(Color.Red, Color.Black);
                else
                    textBoxIP.SetColor(Color.White, Color.Black);
            }
            else if(clientState == State.WaitPlayer)
            {
                if (Client.IsLostConnection())
                {
                    stateTextColor = Color.Red;
                    stateText = "connection lost";
                    clientState = State.Connection;
                    NetPlay.IsMultiplaying = false;
                }
            }

        }


        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {

            if(clientState == State.Connection)
            {
                Writer.DrawText(Main.UltimateFont, "connection", new Vector2((1920 / 2) - (Main.UltimateFont.MeasureString("connection").X * 8f + 9 * 8f) / 2, 25 - 15), new Color(60, 60, 60), Color.LightGray, 0f, Vector2.Zero, 8f, SpriteEffects.None, 0f, 6f, spriteBatch, Color.Black, false);


                for (int i = 0; i < connectionButtons.Count; i++)
                {
                    connectionButtons[i].Draw(spriteBatch);
                }


                Writer.DrawText(Main.UltimateFont, "ip : " + NetPlay.LocalIPAddress(), new Vector2(1640, 1040), Color.Black, Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f, 2f, spriteBatch);

                textBoxIP.Draw(spriteBatch);

                textBoxPort.Draw(spriteBatch);

                if (Client.GetState() == Client.ClientState.Connecting)
                {

                    stateTextColor = Color.White;

                    animTime += 1;

                    if (animTime == 60)
                        animTime = 0;

                    if (animTime >= 0 && animTime < 20)
                        stateText = "connecting.";
                    else if (animTime >= 20 && animTime < 40)
                        stateText = "connecting..";
                    else if (animTime >= 40 && animTime < 60)
                        stateText = "connecting...";

                }

                Writer.DrawText(Main.UltimateFont, stateText, new Vector2(10, 5), Color.Black, stateTextColor, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f, 2f, spriteBatch);

            }

            else if (clientState == State.WaitPlayer)
            {

                Writer.DrawText(Main.UltimateFont, "waiting for players", new Vector2((1920 / 2) - (Main.UltimateFont.MeasureString("waiting for players").X * 8f + 9 * 8f) / 2, 25 - 15), new Color(60, 60, 60), Color.LightGray, 0f, Vector2.Zero, 8f, SpriteEffects.None, 0f, 6f, spriteBatch, Color.Black, false);
                Writer.DrawText(Main.UltimateFont, "player " + NetPlay.MyPlayerID(), new Vector2((1920 / 2) - (Main.UltimateFont.MeasureString("player " + NetPlay.MyPlayerID()).X * 4f + 9 * 4f) / 2, 200), new Color(60, 60, 60), Color.LightGray, 0f, Vector2.Zero, 4f, SpriteEffects.None, 0f, 2f, spriteBatch, Color.Black, false);

                for (int i = 0; i < NetPlay.usedPlayerID.Count; i++)
                {
                    Writer.DrawText(Main.UltimateFont, $"player {NetPlay.usedPlayerID[i]} is connected", new Vector2(20, 600 + (NetPlay.usedPlayerID[i]) * 50), Color.Black, Color.White, 0f, Vector2.Zero, 3f, SpriteEffects.None, 0f, 3f, spriteBatch, true);
                }
                    

            }


        }

        public bool IsValidIP(string IP)
        {
            int num = 0;
            int point = 0;

            if (IP.Length < 7) return false;

            if (IP.ToCharArray()[0] == '.' || IP.ToCharArray()[IP.Length - 1] == '.') return false;

            for (int i = 0; i < IP.Length; i++)
            {
                if (IP.ToCharArray()[i] == '.')
                    point += 1;

                if (char.IsDigit(IP.ToCharArray()[i]))
                    num += 1;

            }

            if (num < 4) return false;
            if (point < 3) return false;

            point = 0;
            num = 0;

            for (int i = 0; i < IP.Length; i++)
            {
                if (char.IsDigit(IP.ToCharArray()[i]))
                { num += 1; point = 0; }

                if (IP.ToCharArray()[i] == '.')
                { point += 1; num = 0; }

                if (point > 1 || num > 3)
                    return false;

            }
            return true;
        }

        public bool IsValidPort(string Port)
        {

            if (Port == "")
                return true;

            if (int.Parse(Port) <= Main.MaxPort)
                return true;

            return false;
        }

        public enum State
        {
            Connection = 0,
            WaitPlayer = 1,
        };


        public void InitButton()
        {

            IPButton.SetTexture(Main.IPBox, new Rectangle(0, 0, 120, 16));
            IPButton.SetColor(Color.White, Color.Black);
            IPButton.SetFont(Main.UltimateFont);
            IPButton.SetScale(5);
            IPButton.SetFrontThickness(4);
            IPButton.SetAroundButton(Connect, Connect);
            IPButton.SetPosition(0, 250, ButtonV3.Position.centerX);

            PortButton.SetTexture(Main.PortBox, new Rectangle(0, 0, 52, 16));
            PortButton.SetColor(Color.White, Color.Black);
            PortButton.SetFont(Main.UltimateFont);
            PortButton.SetScale(5);
            PortButton.SetFrontThickness(4);
            PortButton.SetAroundButton(Connect, Connect);
            PortButton.SetPosition(0, 400, ButtonV3.Position.centerX);



            Connect.SetText("connect");
            Connect.SetColor(Color.White, Color.Black);
            Connect.SetFont(Main.UltimateFont);
            Connect.SetScale(5);
            Connect.IsMajuscule(false);
            Connect.SetFrontThickness(4);
            Connect.SetAroundButton(Back, Back);
            Connect.SetPosition(0, 600, ButtonV3.Position.centerX);



            Back.SetText("back");
            Back.SetColor(Color.White, Color.Black);
            Back.SetFont(Main.UltimateFont);
            Back.SetScale(5);
            Back.IsMajuscule(false);
            Back.SetFrontThickness(4);
            Back.SetAroundButton(Connect, Connect);
            Back.SetPosition(0, 700, ButtonV3.Position.centerX);



        }


        /// <summary>
        /// Sous-tache async pour récup l'erreur !
        /// </summary>
        /// <param name="IP"></param>
        /// <param name="port"></param>
        public async void Connection(string IP, int port)
        {
            try
            {
                await Client.Connect(IP, port);
            }
            catch (ConnectionError)
            {
                stateTextColor = Color.Red;
                stateText = "connection failed";
            }
            catch (FullError)
            {
                stateTextColor = Color.Red;
                stateText = "max player is reached";
            }
            catch (RefuseError)
            {
                stateTextColor = Color.Red;
                stateText = "game was already started";
            }



        }

    }
}
