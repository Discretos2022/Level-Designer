using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discretos_Level_Designer
{
    public class TextButton
    {

        public ButtonV3 button;
        public TextBox box;
        public bool isSelected;
        public bool isActive;

        public TextButton(Vector2 _position = default, int _maxCharacters = 20, float _scale = 1, float _frontThickness = 1, bool _isEdgeRounded = false, string _defaultText = "", bool _lockAlpha = false, bool _lockSign = false)
        {
            button = new ButtonV3();
            button.SetTexture(Main.LevelGUIBar, new Rectangle(0, 0, 190, 24));
            button.SetColor(Color.White, Color.Black);
            button.SetScale(4f);
            button.SetPosition(_position.X, _position.Y);

            box = new TextBox(_maxCharacters, _scale, _frontThickness, _isEdgeRounded, _defaultText, _lockAlpha, _lockSign);
        }

        public void Update(GameTime gameTime, Screen screen)
        {

            if(isActive)
                box.Update();


            button.Update(gameTime, screen);

            if (button.IsSelected() && !isActive)
                button.SetColor(Color.Gray, Color.Black);
            else if (!button.IsSelected() && !isActive)
                button.SetColor(Color.White, Color.Black);

            else if (!button.IsSelected() && MouseInput.getMouseState().LeftButton == ButtonState.Pressed && isActive)
            {
                isActive = false;
                button.SetTexture(Main.LevelGUIBar, new Rectangle(0, 0, 190, 24));
            }

            if (button.IsCliqued())
            {
                button.SetTexture(Main.LevelNameBar, new Rectangle(0, 0, 190, 24));
                isActive = true;
            }


        }

        public void Draw(SpriteBatch spriteBatch)
        {

            button.Draw(spriteBatch);

            if (isActive)
            {
                if (Main.UltimateFont.MeasureString(box.GetText()).X * 4 > 190 * 4 - 100)
                {
                    box.SetScale(3f, 3f);
                    box.SetPosition(button.GetPosition().X + 25, button.GetPosition().Y + 20);
                }
                else
                {
                    box.SetScale(4f, 4f);
                    box.SetPosition(button.GetPosition().X + 25, button.GetPosition().Y + 10);
                }
                    

                box.Draw(spriteBatch);
            }
                

            if (!isActive)
            {
                if(Main.UltimateFont.MeasureString(box.GetText()).X * 4 < 190 * 4 - 70)
                    Writer.DrawText(Main.UltimateFont, box.GetText(), new Vector2(button.GetPosition().X + button.GetWidth() / 2 - Main.UltimateFont.MeasureString(box.GetText()).X * 4 / 2 - 4 * 4 - 2, button.GetPosition().Y), Color.Black, Color.White, 0f, Vector2.Zero, 4f, SpriteEffects.None, 0f, 4f, spriteBatch);
                else
                    Writer.DrawText(Main.UltimateFont, box.GetText(), new Vector2(button.GetPosition().X + button.GetWidth() / 2 - Main.UltimateFont.MeasureString(box.GetText()).X * 3 / 2 - 3 * 3 - 2, button.GetPosition().Y + 15), Color.Black, Color.White, 0f, Vector2.Zero, 3f, SpriteEffects.None, 0f, 3f, spriteBatch);


            }

        }

        public void SetPosition(Vector2 Position)
        {
            button.SetPosition(Position.X, Position.Y);
            box.SetPosition(Position.X + 25, Position.Y + 10);
            box.CalculatePosition();
        }

    }
}
