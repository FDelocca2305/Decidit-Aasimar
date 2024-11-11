using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Scripts.Upgrades
{
    internal class RegerateHealthUpgrade : IUpgrade
    {
        public void Apply(Player player)
        {
            player.RegenerateHealth(20);
        }

        public override string ToString()
        {
            return "regeneracion de salud";
        }
    }
}
