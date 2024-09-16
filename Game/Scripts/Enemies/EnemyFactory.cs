using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Scripts.Enemies
{
    public class EnemyFactory
    {
        public enum EnemyType
        {
            Basic,
            Fast
        }

        public static Enemy CreateEnemy(EnemyType type, float x, float y, Player player, float difficultyMultiplier)
        {
            Enemy enemy = null;

            switch (type)
            {
                case EnemyType.Basic:
                    enemy = new BasicEnemy(x, y, player);
                    break;
                case EnemyType.Fast:
                    enemy = new FastEnemy(x, y, player);
                    break;
            }

            enemy.Initialize(difficultyMultiplier);
            return enemy;
        }
    }
}
