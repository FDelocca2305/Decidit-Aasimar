using Game.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Scripts.Enemies
{
    public class EnemyFactory
    {
        private readonly ObjectManager objectManager;

        public EnemyFactory(ObjectManager objectManager)
        {
            this.objectManager = objectManager;
        }

        public Enemy CreateEnemy(EnemyType type, Vector2 position, Player player, float difficultyMultiplier)
        {
            return objectManager.GetEnemy(type, position, player, difficultyMultiplier);
        }

        public enum EnemyType
        {
            Basic,
            Fast
        }
    }
}
