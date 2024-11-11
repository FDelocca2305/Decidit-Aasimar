using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Scripts.Interfaces
{
    public interface ILevelManager
    {
        int Level { get;}
        int Experience { get; }
        int ExperienceToNextLevel { get; }
        event Action OnLevelUp;
        void AddExperience(int amount);
    }
}
