using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discretos_Level_Designer
{
    public abstract class MessageBoxV2
    {

        public static List<MessageBoxV2> activeBox = new List<MessageBoxV2>();

        public Vector2 Position;
        public Rectangle Dimension;

        private ButtonV3 Close;


        private bool IsActive;
        public bool isActive
        {
            get { return IsActive; } 
            set 
            {
                if (!IsActive && value) { IsActive = value; activeBox.Add(this); }
                if (IsActive && !value) { IsActive = value; activeBox.Remove(this); }
            }
        }


        public MessageBoxV2()
        {
            
        }

        public void SetPosition(Vector2 position)
        {
            Position = position;
        }

        public void SetPositionInCenter(int windowWidth, int windowHeight)
        {
            Position = new Vector2(windowWidth / 2 - Dimension.Width / 2, windowHeight / 2 - Dimension.Height / 2);
        }

        public void AddCloseButton(Vector2 position)
        {

            if(Close == null)
            {
                Close = new ButtonV3();
                Close.SetTexture(Main.ButtonTexture[7], new Rectangle(0, 0, 16, 16));
                Close.SetColor(Color.White, Color.Black);
                Close.SetFont(Main.UltimateFont);
                Close.SetScale(4);
                Close.SetPosition(position.X, position.Y);
            }
            

        }


        public virtual void Update(GameTime gameTime, Screen screen, Editor editor)
        {

            if(Close != null)
            {

                Close.Update(gameTime, screen);

                if (Close.IsSelected())
                    Close.SetColor(Color.Gray, Color.Gray);
                else
                    Close.SetColor(Color.White, Color.Black);

                if (Close.IsCliqued())
                { isActive = false; Close.Update(gameTime, screen); editor.oneReleaseMouse = false; }

            }

        }


        public void Draw(Vector2 position, SpriteBatch spriteBatch)
        {
            if (isActive)
            {
                Position = position;
                spriteBatch.Draw(Main.Bounds, new Rectangle((int)Position.X - 4, (int)Position.Y - 4, Dimension.Width + 8, Dimension.Height + 8), Color.Black);
                spriteBatch.Draw(Main.Bounds, new Rectangle((int)Position.X, (int)Position.Y, Dimension.Width, Dimension.Height), Color.Gray);
            }

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (isActive)
            {

                spriteBatch.Draw(Main.Bounds, new Rectangle(0, 0, 1920, 1080), Color.DarkGray * 0.5f);

                spriteBatch.Draw(Main.Bounds, new Rectangle((int)Position.X - 4, (int)Position.Y - 4, Dimension.Width + 8, Dimension.Height + 8), Color.Black);
                spriteBatch.Draw(Main.Bounds, new Rectangle((int)Position.X, (int)Position.Y, Dimension.Width, Dimension.Height), Color.Gray);

            }

        }

        public void DrawCloseButton(SpriteBatch spriteBatch)
        {
            if (Close != null)
                Close.Draw(spriteBatch);
        }

        public void Draw(Vector2 position, int borderThickness, SpriteBatch spriteBatch)
        {
            if (isActive)
            {
                Position = position;
                spriteBatch.Draw(Main.Bounds, new Rectangle((int)Position.X - borderThickness, (int)Position.Y - borderThickness, Dimension.Width + borderThickness, Dimension.Height + borderThickness), Color.Black);
                spriteBatch.Draw(Main.Bounds, new Rectangle((int)Position.X, (int)Position.Y, Dimension.Width, Dimension.Height), Color.Gray);
            }

        }



        public static bool HaveNotActiveBox()
        {
            if (activeBox.Count == 0) return true;
            else return false;
        }


        public static void CloseAllBox()
        {

            for (int i = 0; i < activeBox.Count; i++)
            {
                activeBox[i].isActive = false;
            }

        }

        public static void CloseLastBox()
        {
            activeBox[activeBox.Count - 1].isActive = false;
            //activeBoxs.RemoveAt(activeBoxs.Count - 1);
        }

        public static void UpdateBoxSystem(GameTime gameTime, Screen screen, Editor editor)
        {

            if(activeBox.Count != 0)
                activeBox[activeBox.Count - 1].Update(gameTime, screen, editor);

        }

        public static void DrawBoxSystem(SpriteBatch spriteBatch)
        {

            for (int i = 0; i < activeBox.Count; i++)
            {
                activeBox[i].Draw(spriteBatch);
            }

        }

    }
}
