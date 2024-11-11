using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Core.Interfaces
{
    internal interface IAudioManager
    {
        void LoadTrack(string name, string filePath);
        void PlayTrack(string name, bool loop = false);
        void StopTrack(string name);
    }
}
