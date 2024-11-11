using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Scripts.Upgrades
{
    internal class DamageUpgrade : IUpgrade
    {
        public void Apply(Player player)
        {
            player.IncreaseDamage(1.20f);
        }

        public override string ToString()
        {
            return "aumentar dano";
        }
    }
}
