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

        private Texture healthBarBackground;
        private Texture healthBarBackgroundRed;
        private Texture healthBarForegroundGreen;

        private Texture expBarBackgroundBlack;
        private Texture expBarForegroundBlue;

        private float currentHealth;
        private int currentExperience;
        private Vector2 playerPosition;

        public UIManager(Player player)
        {
            instance = this;

            healthBarBackground = Engine.GetTexture("Assets/bar_empty.png");
            healthBarBackgroundRed = Engine.GetTexture("Assets/bar_red.png");
            healthBarForegroundGreen = Engine.GetTexture("Assets/bar_green.png");

            expBarBackgroundBlack = Engine.GetTexture("Assets/exp_bar_black.png");
            expBarForegroundBlue = Engine.GetTexture("Assets/exp_bar_blue.png");

            player.OnDamageTaken += UpdateHealth;
            player.OnExperienceChanged += UpdateExperience;
            player.OnPositionChanged += UpdatePlayerPosition;
            currentHealth = player.Health;
            currentExperience = player.Experience;
            playerPosition = player.Position;
        }

        private void UpdateHealth(int health)
        {
            currentHealth = health;
        }

        private void UpdateExperience(int experience)
        {
            currentExperience = experience;
        }

        private void UpdatePlayerPosition(Vector2 newPosition)
        {
            playerPosition = newPosition;
        }

        public void Render(string formattedTime)
        {
            RenderHealthBar();
            RenderExpBar();
            RenderGameTime(formattedTime);
        }

        private void RenderGameTime(string formattedTime)
        {
            int timeX = 900;
            int timeY = 80;
            TextManager.Instance.DrawText(formattedTime, timeX, timeY, 1.5f);
        }

        private void RenderHealthBar()
        {
            float healthPercentage = currentHealth / 100f;
            float barX = playerPosition.X - 50f;
            float barY = playerPosition.Y - 20f;

            Engine.Draw(healthBarBackground, barX, barY, 1f, 0.2f);

            Engine.Draw(healthBarBackgroundRed, barX, barY, 1f, 0.2f);

            Engine.Draw(healthBarForegroundGreen, barX, barY, healthPercentage, 0.2f);
        }

        private void RenderExpBar()
        {
            float expPercentage = currentExperience / 100f;
            int barX = 50;
            int barY = 10;

            Engine.Draw(expBarBackgroundBlack, barX, barY, 1f, .5f);

            Engine.Draw(expBarForegroundBlue, barX, barY, expPercentage, .5f);
        }
    }
}
