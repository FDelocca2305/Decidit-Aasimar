using Game.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        private bool isUpgradePanelVisible = false;
        private List<string> currentUpgrades;
        private Texture upgradePanelTexture;

        private float currentHealth;
        private float maxHealth;
        private int currentExperience;
        private int maxExperienceForNextLevel;
        private Vector2 playerPosition;

        public UIManager(Player player)
        {
            instance = this;

            healthBarBackground = Engine.GetTexture("Assets/bar_empty.png");
            healthBarBackgroundRed = Engine.GetTexture("Assets/bar_red.png");
            healthBarForegroundGreen = Engine.GetTexture("Assets/bar_green.png");

            expBarBackgroundBlack = Engine.GetTexture("Assets/exp_bar_black.png");
            expBarForegroundBlue = Engine.GetTexture("Assets/exp_bar_blue.png");

            upgradePanelTexture = Engine.GetTexture("Assets/upgrades_panel.png");

            player.OnDamageTaken += UpdateHealth;
            player.OnExperienceChanged += UpdateExperience;
            player.OnPositionChanged += UpdatePlayerPosition;
            currentHealth = player.Health;
            currentExperience = player.Experience;
            playerPosition = player.Position;
            maxHealth = player.MaxHealth;
            maxExperienceForNextLevel = player.ExperienceToNextLevel;
        }

        private void UpdateHealth(int health, float maxPlayerHealth)
        {
            currentHealth -= health;
            maxHealth = maxPlayerHealth;
        }

        private void UpdateExperience(int experience, int experienceToNextLevel)
        {
            currentExperience = experience;
            maxExperienceForNextLevel = experienceToNextLevel;
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

            if (isUpgradePanelVisible)
            {
                RenderUpgradePanel();
            }
        }

        public void ShowUpgradePanel(List<string> upgrades)
        {
            isUpgradePanelVisible = true;
            currentUpgrades = upgrades;
        }

        private void RenderUpgradePanel()
        {
            Engine.Draw(upgradePanelTexture, 500, 200);
            for (int i = 0; i < currentUpgrades.Count; i++)
            {
                TextManager.Instance.DrawText($"{i + 1} - {currentUpgrades[i]}", 550, 250 + (i * 50), 1.5f);
            }
        }

        public void HideUpgradePanel()
        {
            isUpgradePanelVisible = false;
        }

        public void HandleUpgradeSelection()
        {
            if (isUpgradePanelVisible)
            {
                if (Engine.GetKey(Keys.Num1))
                {
                    GameManager.Instance.UpgradeManager.ApplyUpgrade(1);
                }
                else if (Engine.GetKey(Keys.Num2))
                {
                    GameManager.Instance.UpgradeManager.ApplyUpgrade(2);
                }
                else if (Engine.GetKey(Keys.Num3))
                {
                    GameManager.Instance.UpgradeManager.ApplyUpgrade(3);
                }
            }
        }

        private void RenderGameTime(string formattedTime)
        {
            int timeX = 900;
            int timeY = 80;
            TextManager.Instance.DrawText(formattedTime, timeX, timeY, 1.5f);
        }

        private void RenderHealthBar()
        {
            float healthPercentage = currentHealth / maxHealth;
            float barX = playerPosition.X - 50f;
            float barY = playerPosition.Y - 20f;

            Engine.Draw(healthBarBackground, barX, barY, 1f, 0.2f);
            Engine.Draw(healthBarBackgroundRed, barX, barY, 1f, 0.2f);
            Engine.Draw(healthBarForegroundGreen, barX, barY, healthPercentage, 0.2f);
        }

        private void RenderExpBar()
        {
            float expPercentage = (float)currentExperience / maxExperienceForNextLevel;
            int barX = 50;
            int barY = 10;

            Engine.Draw(expBarBackgroundBlack, barX, barY, 1f, .5f);
            Engine.Draw(expBarForegroundBlue, barX, barY, expPercentage, .5f);
        }
    }
}
