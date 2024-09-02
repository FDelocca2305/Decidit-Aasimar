using Game.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Core
{
    public class UIManager
    {
        public static UIManager instance;

        private Player player;
        private Texture healthBarBackground;
        private Texture healthBarBackgroundRed;
        private Texture healthBarForegroundGreen;

        private Texture expBarBackgroundBlack;
        private Texture expBarForegroundBlue;

        public UIManager(Player player)
        {
            instance = this;

            this.player = player;
            healthBarBackground = Engine.GetTexture("Assets/bar_empty.png");
            healthBarBackgroundRed = Engine.GetTexture("Assets/bar_red.png");
            healthBarForegroundGreen = Engine.GetTexture("Assets/bar_green.png");

            expBarBackgroundBlack = Engine.GetTexture("Assets/exp_bar_black.png");
            expBarForegroundBlue = Engine.GetTexture("Assets/exp_bar_blue.png");
        }

        public void Render(string formattedTime)
        {
            RenderHealthBar();
            RenderExpBar();
            //RenderGameTime(formattedTime);
        }

        private void RenderGameTime(string formattedTime)
        {
            int timeX = 700;
            int timeY = 10;
            Engine.Draw(formattedTime, timeX, timeY, 20, 255, 255, 255);
        }

        private void RenderHealthBar()
        {
            float healthPercentage = player.Health / 100f;
            float barX = player.Position.X - 50f;
            float barY = player.Position.Y - 20f;

            Engine.Draw(healthBarBackground, barX, barY, 1f, 0.2f);

            Engine.Draw(healthBarBackgroundRed, barX, barY, 1f, 0.2f);

            Engine.Draw(healthBarForegroundGreen, barX, barY, healthPercentage, 0.2f);
        }

        private void RenderExpBar()
        {
            float expPercentage = player.Experience / 100f;
            int barX = 50;
            int barY = 10;

            Engine.Draw(expBarBackgroundBlack, barX, barY, 1f, .5f);

            Engine.Draw(expBarForegroundBlue, barX, barY, expPercentage, .5f);
        }
    }
}
