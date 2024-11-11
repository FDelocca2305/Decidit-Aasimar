using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Scripts.Upgrades
{
    internal class AttackCooldownUpgrade : IUpgrade
    {
        public void Apply(Player player)
        {
            player.ReduceAttackCooldown(0.95f);
        }

        public override string ToString()
        {
            return "disparo rapido";
        }
    }
}
