using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Discretos_Level_Designer.GUI
{
    public class LoadingGUI : MessageBoxV2
    {

        private static LoadingGUI _instance;

        private float value;
        private int maxValue = 100;



        private LoadingGUI() : base()
        {

        }

        public static LoadingGUI GUI
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new LoadingGUI();

                    GUI.Dimension = new Rectangle(0, 0, 800, 100);
                    GUI.SetPositionInCenter(1920, 1080);


                }
                return _instance;
            }
        }

        public override void Update(GameTime gameTime, Screen screen, Editor editor)
        {

            base.Update(gameTime, screen, editor);

            if (value >= maxValue)
            {
                value = 0;
                isActive = false;
            }

        }

        public override void Draw(SpriteBatch spriteBatch)
        {

            base.Draw(spriteBatch);

            if (isActive)
            {

                int thinkness = 25;

                spriteBatch.Draw(Main.Bounds, new Rectangle(1920 / 2 - 758 / 2 - 4, 1080 / 2 - (thinkness + 8) / 2, 758 + 8, thinkness + 8), Color.Black);
                spriteBatch.Draw(Main.Bounds, new Rectangle(1920 / 2 - 750 / 2 - 4, 1080 / 2 - thinkness / 2, 750 + 8, thinkness), Color.DarkGray);

                int upBar = (int)Math.Round(thinkness * (2.0f/5.0f));
                int bottomBar = thinkness - upBar;

                spriteBatch.Draw(Main.Bounds, new Rectangle(1920 / 2 - 750 / 2 - 4, 1080 / 2 - upBar / 2 - bottomBar / 2, (int)(value * 750 / 100), upBar), Color.LightGreen);
                spriteBatch.Draw(Main.Bounds, new Rectangle(1920 / 2 - 750 / 2 - 4, 1080 / 2 - bottomBar / 2 + upBar / 2, (int)(value * 750 / 100), bottomBar), Color.Green);

            }

        }

        public void SetMaxValue()
        {

        }

        public void SetValue(float value)
        {
            this.value = value;
        }

    }
}
