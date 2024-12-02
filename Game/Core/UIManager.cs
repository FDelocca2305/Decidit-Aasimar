using Game.Core.Interfaces;
using Game.Scripts;
using Game.Scripts.Upgrades;
using Game.Scripts.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game.Core
{
    public class UIManager : IUIManager
    {
        private Texture healthBarBackground;
        private Texture healthBarBackgroundRed;
        private Texture healthBarForegroundGreen;

        private Texture expBarBackgroundBlack;
        private Texture expBarForegroundBlue;

        private bool isUpgradePanelVisible = false;
        private List<IUpgrade> currentUpgrades;
        private Texture upgradePanelTexture;

        private Texture bossBarBackgroundBlack;
        private Texture bossBarForegroundRed;

        private float currentHealth;
        private float maxHealth;
        private int currentExperience;
        private int maxExperienceForNextLevel;
        private Vector2 playerPosition;

        private bool isBossBarVisible = false;
        private float bossCurrentHealth;
        private float bossMaxHealth;

        public UIManager()
        {
            healthBarBackground = Engine.GetTexture("Assets/bar_empty.png");
            healthBarBackgroundRed = Engine.GetTexture("Assets/bar_red.png");
            healthBarForegroundGreen = Engine.GetTexture("Assets/bar_green.png");

            expBarBackgroundBlack = Engine.GetTexture("Assets/exp_bar_black.png");
            expBarForegroundBlue = Engine.GetTexture("Assets/exp_bar_blue.png");

            upgradePanelTexture = Engine.GetTexture("Assets/upgrades_panel.png");

            bossBarBackgroundBlack = Engine.GetTexture("Assets/exp_bar_black.png");
            bossBarForegroundRed = Engine.GetTexture("Assets/exp_bar_blue.png");
        }

        public void UpdateHealth(float currentHealth, float maxHealth)
        {
            this.currentHealth = currentHealth;
            this.maxHealth = maxHealth;
        }

        public void UpdateExperience(int currentExperience, int maxExperienceForNextLevel)
        {
            this.currentExperience = currentExperience;
            this.maxExperienceForNextLevel = maxExperienceForNextLevel;
        }

        public void UpdatePlayerPosition(Vector2 newPosition)
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

            if (isBossBarVisible)
            {
                RenderBossBar();
            }
        }

        public void ShowUpgradePanel(List<IUpgrade> upgrades)
        {
            isUpgradePanelVisible = true;
            currentUpgrades = upgrades;
        }

        public void HideUpgradePanel()
        {
            isUpgradePanelVisible = false;
        }

        public void HandleUpgradeSelection()
        {
            if (!isUpgradePanelVisible) return;

            if (Engine.GetKey(Keys.Num1) && currentUpgrades.Count > 0)
            {
                GameManager.Instance.UpgradeManager.ApplyUpgrade(1);
                HideUpgradePanel();
            }
            else if (Engine.GetKey(Keys.Num2) && currentUpgrades.Count > 1)
            {
                GameManager.Instance.UpgradeManager.ApplyUpgrade(2);
                HideUpgradePanel();
            }
            else if (Engine.GetKey(Keys.Num3) && currentUpgrades.Count > 2)
            {
                GameManager.Instance.UpgradeManager.ApplyUpgrade(3);
                HideUpgradePanel();
            }
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

        private void RenderUpgradePanel()
        {
            Engine.Draw(upgradePanelTexture, 500, 200);
            for (int i = 0; i < currentUpgrades.Count; i++)
            {
                TextManager.Instance.DrawText($"{i + 1} - {currentUpgrades[i].ToString()}", 550, 250 + (i * 50), 1.5f);
            }
        }

        private void RenderGameTime(string formattedTime)
        {
            int timeX = 900;
            int timeY = 80;
            TextManager.Instance.DrawText(formattedTime, timeX, timeY, 1.5f);
        }

        public void ShowBossBar(float bossCurrentHealth, float bossMaxHealth)
        {
            isBossBarVisible = true;
            this.bossCurrentHealth = bossCurrentHealth;
            this.bossMaxHealth = bossMaxHealth;
        }

        public void UpdateBossHealth(float bossCurrentHealth, float bossMaxHealth)
        {
            this.bossCurrentHealth = bossCurrentHealth;
            this.bossMaxHealth = bossMaxHealth;
        }

        public void HideBossBar()
        {
            isBossBarVisible = false;
        }

        private void RenderBossBar()
        {
            float bossHealthPercentage = bossCurrentHealth / bossMaxHealth;
            int barX = 50;
            int barY = GlobalConstants.GraphicsConstants.ScreenHeight - 50;

            Engine.Draw(bossBarBackgroundBlack, barX, barY, 1f, .5f);
            Engine.Draw(bossBarForegroundRed, barX, barY, bossHealthPercentage, .5f);
            TextManager.Instance.DrawText("boss health", barX + 10, barY - 20, 1.5f);
        }

    }
}
