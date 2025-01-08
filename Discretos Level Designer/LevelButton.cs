using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discretos_Level_Designer
{
    /// <summary>
    /// Level Button for Load a Level.
    /// </summary>
    public class LevelButton
    {

        public ButtonV3 Load;
        public ButtonV3 Delete;
        public ButtonV3 Info;

        public Vector2 Position;
        public Color color;

        public String LevelName;
        public String LevelDate;
        public String LevelSize;

        public LevelButton(Vector2 position, String levelName, String levelDate, String levelSize)
        {
            Position = position;
            LevelName = levelName;
            LevelDate = levelDate;
            LevelSize = levelSize;

            InitButton();

        }

        public void Draw(SpriteBatch spriteBatch, Color _color)
        {
            spriteBatch.Draw(Main.LevelGUIBar, Position, null, _color, 0f, Vector2.Zero, 4f, SpriteEffects.None, 0f);

            Load.Draw(spriteBatch);
            Delete.Draw(spriteBatch);

            Writer.DrawText(Main.UltimateFont, LevelName, Position + new Vector2(30, -2), Color.Black, Color.White, 0f, Vector2.Zero, 3f, SpriteEffects.None, 0f, 3f, spriteBatch);
            Writer.DrawText(Main.UltimateFont, LevelDate, Position + new Vector2(30, 50), Color.Black, Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f, 2f, spriteBatch);
            Writer.DrawText(Main.UltimateFont, LevelSize, Position + new Vector2(360, 50), Color.Black, Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f, 2f, spriteBatch);



        }

        public void SetPosition(Vector2 NewPosition)
        {
            Position = NewPosition;
            InitButton();
        }

        public void SetPositionWithoutReInit(Vector2 NewPosition)
        {
            Position = NewPosition;
            Load.SetPosition(Position.X + 680 - 16 * 4 - 10, Position.Y + 16);
            Delete.SetPosition(Position.X + 680, Position.Y + 16);
        }

        private void InitButton()
        {
            Load = new ButtonV3();
            Load.SetTexture(Main.ButtonTexture[2], new Rectangle(0, 0, 16, 16));
            Load.SetColor(Color.White, Color.Black);
            Load.SetFont(Main.UltimateFont);
            Load.SetScale(4);
            Load.SetPosition(Position.X + 680 - 16 * 4 - 10, Position.Y + 16);

            Delete = new ButtonV3();
            Delete.SetTexture(Main.ButtonTexture[11], new Rectangle(0, 0, 16, 16));
            Delete.SetColor(Color.White, Color.Black);
            Delete.SetFont(Main.UltimateFont);
            Delete.SetScale(4);
            Delete.SetPosition(Position.X + 680, Position.Y + 16);


        }


    }
}
