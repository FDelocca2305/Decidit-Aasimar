using Game.Scripts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Scripts.Utils;

namespace Game.Scripts
{
    public class LevelManager: ILevelManager
    {
        //This To?
        private int experience = 0;
        private int level = ConfigLoader.LevelConfig.InitialLevel;
        private int experienceToNextLevel =  ConfigLoader.LevelConfig.ExperienceToNextLevel;

        public int Level => level;
        public int Experience => experience;
        public int ExperienceToNextLevel => experienceToNextLevel;

        public event Action OnLevelUp;

        public void AddExperience(int amount)
        {
            experience += amount;
            while (experience >= experienceToNextLevel)
            {
                experience -= experienceToNextLevel;
                level++;
                Console.WriteLine($"Subió a nivel {Level}");
                experienceToNextLevel = CalculateExperienceForNextLevel();
                OnLevelUp?.Invoke();
            }
        }

        private int CalculateExperienceForNextLevel()
        {
            return (int)(experienceToNextLevel * 1.5f);
        }
    }
}
