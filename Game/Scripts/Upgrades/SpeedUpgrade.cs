using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Scripts.Upgrades
{
    internal class SpeedUpgrade : IUpgrade
    {
        public void Apply(Player player)
        {
            player.IncreaseSpeed(20f);
        }

        public override string ToString()
        {
            return "aumentar velocidad";
        }
    }
}
