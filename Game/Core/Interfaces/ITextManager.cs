using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Core.Interfaces
{
    internal interface ITextManager
    {
        void DrawText(string text, float x, float y, float fontSize = 1f);
    }
}
