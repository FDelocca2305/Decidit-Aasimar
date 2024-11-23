using System;
using Game.Core;

namespace Game.Scripts.Waves
{
    public abstract class Wave
    {
        protected int currentWave;
        protected ObjectManager objectManager;
        protected Player player;
        protected float difficultyMultiplier;
        public Wave(int currentWave, ObjectManager objectManager, Player player)
        {
            this.currentWave = currentWave;
            this.objectManager = objectManager;
            this.player = player;
            this.difficultyMultiplier = 1.0f + (currentWave * 0.1f);
        }
        public abstract void StartWave();
        public int CalculateEnemyCountForWave(int baseMultiplier)
        {
            return currentWave * baseMultiplier;
        }
    }
}