using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.AxHost;

namespace Game.Core
{
    public class TextManager
    {
        private static TextManager instance;
        private Dictionary<char, Texture> letterTextures = new Dictionary<char, Texture>();
        private float letterSpacing = 1.5f;

        private TextManager()
        {
            LoadAlphabet();
        }

        public static TextManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new TextManager();
                }
                return instance;
            }
        }

        private void LoadAlphabet()
        {
            string path = "Assets/Fonts/WhitePeaberry/";
            for (char letter = 'a'; letter <= 'z'; letter++)
            {
                string filePath = $"{path}{letter}.png";
                Texture letterTexture = Engine.GetTexture(filePath);
                letterTextures.Add(letter, letterTexture);
            }

            for (char number = '0'; number <= '9'; number++)
            {
                string filePath = $"{path}{number}.png";
                Texture numberTexture = Engine.GetTexture(filePath);
                letterTextures.Add(number, numberTexture);
            }

            letterTextures.Add(' ', null);
            letterTextures.Add(':', Engine.GetTexture($"{path}dp.png"));
        }

        public void DrawText(string text, float x, float y, float fontSize = 1f)
        {
            float startX = x;

            foreach (char letter in text)
            {
                if (letterTextures.ContainsKey(letter))
                {
                    Texture texture = letterTextures[letter];
                    if (texture != null)
                    {
                        Engine.Draw(texture, x, y, fontSize, fontSize);
                        x += (texture.Width * fontSize) + (letterSpacing * fontSize);
                    }
                    else
                    {
                        x += letterSpacing * 7 * fontSize;
                    }
                }
            }
        }
    }
}
