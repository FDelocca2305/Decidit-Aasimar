using Game.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Scripts.Waves
{
    public interface IWaveStrategy
    {
        Wave GenerateWave(int currentWave, ObjectManager objectManager, Player player);
    }
}
