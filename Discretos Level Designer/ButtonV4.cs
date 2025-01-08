using Discretos_Level_Designer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SiedelGUI
{
    public class ButtonV4
    {

        public static List<ButtonV4> buttons = new List<ButtonV4>();

        public static int ScreenWidth;
        public static int ScreenHeight;

        protected static Mode mode = Mode.Mouse;

        public Texture2D texture;
        public string text;

        private Vector2 position;
        private Vector2 lateralPosition;
        private Vector2 size;
        private Alignment alignment = Alignment.None;
        private float scale = 1f;

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        private Rectangle bounds = new Rectangle();

        private bool isSelected;
        private bool isCliqued;
        private bool isReleased;


        public ButtonV4()
        {

        }

        public void Update(Vector2 mousePosition)
        {

            Rectangle mousePointer = new Rectangle((int)mousePosition.X, (int)mousePosition.Y, 1, 1);

            if (bounds.Intersects(mousePointer)) isSelected = true;
            else isSelected = false;

            if (isSelected && MouseInput.getMouseState().LeftButton == ButtonState.Pressed && (MouseInput.getOldMouseState().LeftButton != ButtonState.Pressed || isCliqued)) 
                { isCliqued = true; isSelected = false; }
            else isCliqued = false;

            if (bounds.Intersects(mousePointer) && MouseInput.getMouseState().LeftButton != ButtonState.Pressed && MouseInput.getOldMouseState().LeftButton == ButtonState.Pressed)
            { isReleased = true; isCliqued = false; }
            else
                isReleased = false;


        }

        public void Draw(SpriteBatch spriteBatch, Color color)
        {

            if (texture != null)
                spriteBatch.Draw(texture, position, null, color, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
        }

        public bool IsSelected()
        {
            return isSelected;
        }

        public bool IsCliqued()
        {
            return isCliqued;
        }

        public bool IsReleased()
        {
            return isReleased;
        }

        public void SetScale(float _scale)
        {
            size /= scale;
            scale = _scale;
            size *= scale;
            CalculateBounds();
        }

        public void SetTexture(Texture2D _texture)
        {
            texture = _texture;
            size = new Vector2(texture.Width * scale, texture.Height * scale);
            CalculateBounds();
        }


        private void CalculateBounds()
        {

            CalculatePosition();

            if(texture != null)
                bounds = new Rectangle((int)position.X, (int)position.Y, (int)(size.X), (int)(size.Y));

        }

        private void CalculatePosition()
        {

            if(texture != null)
            {

                if (alignment == Alignment.None)
                {
                    position = lateralPosition;
                }

                else if (alignment == Alignment.Center)
                {
                    position.X = ScreenWidth / 2 - size.X / 2;
                    position.Y = ScreenHeight / 2 - size.Y / 2;
                    position += lateralPosition;
                }

                else if (alignment == Alignment.CenterX)
                {
                    position.X = ScreenWidth / 2 - size.X / 2;
                    position += lateralPosition;
                }

                else if (alignment == Alignment.CenterY)
                {
                    position.Y = ScreenHeight / 2 - size.Y / 2;
                    position += lateralPosition;
                }


            }

        }

        public void SetPosition(Vector2 newPos)
        {
            lateralPosition = newPos;
            CalculateBounds();
        }

        public void SetAlignment(Alignment alignment)
        {
            this.alignment = alignment;
            CalculateBounds();
        }


        public static void SetMode(Mode mode)
        {
            ButtonV4.mode = mode;
        }


        public enum Alignment
        {
            None = 0,
            CenterX = 1,
            CenterY = 2,
            Center = 3,
        };

        public enum Mode
        {
            KeyBoard = 1,
            Mouse = 2,
        };

    }
}
