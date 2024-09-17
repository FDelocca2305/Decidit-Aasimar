using Game.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Scripts
{
    public class UpgradeManager
    {
        private List<string> possibleUpgrades = new List<string>
        {
            "aumentar velocidad",
            "aumentar dano",
            "regeneracion de salud",
            "disparo rapido",
            "aumentar salud maxima"
        };

        private Player player;
        private UIManager uiManager;
        private List<string> currentUpgrades = new List<string>();

        public UpgradeManager(Player player, UIManager uiManager)
        {
            this.player = player;
            this.uiManager = uiManager;
        }

        public void ShowUpgradeOptions()
        {
            currentUpgrades.Clear();
            Random rand = new Random();
            for (int i = 0; i < 3; i++)
            {
                string upgrade = possibleUpgrades[rand.Next(possibleUpgrades.Count)];
                currentUpgrades.Add(upgrade);
            }

            uiManager.ShowUpgradePanel(currentUpgrades);
        }

        public void ApplyUpgrade(int selection)
        {
            int index = selection - 1;

            if (index < 0 || index >= currentUpgrades.Count)
                return;
            
            string selectedUpgrade = currentUpgrades[index];
            ApplyUpgradeEffect(selectedUpgrade);
            uiManager.HideUpgradePanel();
        }

        private void ApplyUpgradeEffect(string upgrade)
        {
            switch (upgrade)
            {
                case "aumentar velocidad":
                    player.IncreaseSpeed(20f);
                    break;
                case "aumentar dano":
                    player.IncreaseDamage(1.20f);
                    break;
                case "regeneracion de salud":
                    player.RegenerateHealth(20);
                    break;
                case "disparo rapido":
                    player.ReduceAttackCooldown(0.95f);
                    break;
                case "aumentar salud maxima":
                    player.IncreaseMaxHealth(50);
                    break;
                default:
                    break;
            }
        }
    }
}
