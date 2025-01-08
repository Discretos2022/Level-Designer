using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discretos_Level_Designer
{
    public static class TextInputManager
    {

        private static Game game;
        private static string c;
        private static string text;

        public static void Init(Game _game) 
        {
            game = _game;
        }

        public static void Update()
        {

            game.Window.KeyUp += KeyInputHandler;
            game.Window.TextInput += TextInputHandler;

        }


        public static string GetText()
        {
            string result = text;
            text = "";

            return result;
        }

        public static void InitText()
        {
            text = "";
        }


        private static void TextInputHandler(object sender, TextInputEventArgs args)
        {
            var pressedKey = args.Key;
            var character = args.Character;


            //Console.WriteLine(character.ToString() + " ; " + c);


            if (c != character.ToString())
            {

                //Console.WriteLine(character.ToString() + " ; " + c + " <<<<<");

                /*if (character == '\b' && game.Window.Title.Length > 1)
                    game.Window.Title = game.Window.Title.Substring(0, game.Window.Title.Length - 1);
                else if (character == '\b')
                    game.Window.Title = "\b";
                else
                {*/
                text += character.ToString();
                //}

                //Console.WriteLine(text);

            }


            c = character.ToString();
        }

        private static void KeyInputHandler(object sender, InputKeyEventArgs args)
        {
            c = "";
        }

    }
}
