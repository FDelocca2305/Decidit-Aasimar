using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Scripts.Upgrades
{
    internal class MaxHealthUpgrade : IUpgrade
    {
        public void Apply(Player player)
        {
            player.IncreaseMaxHealth(50);
        }

        public override string ToString()
        {
            return "aumentar salud maxima";
        }
    }
}
