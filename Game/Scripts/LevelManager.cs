using Game.Scripts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Scripts
{
    public class LevelManager: ILevelManager
    {
        private int experience = 0;
        private int level = 1;
        private int experienceToNextLevel = 100;

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
