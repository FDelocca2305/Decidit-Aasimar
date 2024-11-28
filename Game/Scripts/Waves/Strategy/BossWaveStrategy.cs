﻿using Game.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Scripts.Waves.Strategy
{
    public class BossWaveStrategy : IWaveStrategy
    {
        public Wave GenerateWave(int currentWave, ObjectManager objectManager, Player player)
        {
            return new BossWave(currentWave, objectManager, player);
        }
    }
}
