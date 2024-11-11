using Game.Scripts.Upgrades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Core.Interfaces
{
    public interface IUIManager
    {
        void UpdateHealth(float currentHealth, float maxHealth);
        void UpdateExperience(int currentExperience, int maxExperienceForNextLevel);
        void UpdatePlayerPosition(Vector2 newPosition);
        void Render(string formattedTime);
        void ShowUpgradePanel(List<IUpgrade> upgrades);
        void HideUpgradePanel();
        void HandleUpgradeSelection();
    }
}
