using Game.Core;
using Game.Scripts.Upgrades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Scripts
{
    public class UpgradeManager
    {
        private readonly List<IUpgrade> availableUpgrades = new List<IUpgrade>
        {
            new SpeedUpgrade(),
            new DamageUpgrade(),
            new MaxHealthUpgrade(),
            new SpeedUpgrade(),
            new AttackCooldownUpgrade(),
        };
        private List<IUpgrade> currentUpgrades = new List<IUpgrade>();
        private Player player;
        private UIManager uiManager;
        

        public UpgradeManager(Player player, UIManager uiManager)
        {
            this.player = player;
            this.uiManager = uiManager;
        }

        public void ShowUpgradeOptions()
        {
            currentUpgrades = SelectRandomUpgrades();
            uiManager.ShowUpgradePanel(currentUpgrades);
        }

        public void ApplyUpgrade(int selection)
        {
            if (selection < 1 || selection > currentUpgrades.Count) return;

            currentUpgrades[selection - 1].Apply(player);
            uiManager.HideUpgradePanel();
        }

        private List<IUpgrade> SelectRandomUpgrades()
        {
            var rand = new Random();
            var selectedUpgrades = new List<IUpgrade>();
            for (int i = 0; i < 3; i++)
            {
                int randomIndex = rand.Next(availableUpgrades.Count);
                selectedUpgrades.Add(availableUpgrades[randomIndex]);
            }
            return selectedUpgrades;
        }
    }
}
