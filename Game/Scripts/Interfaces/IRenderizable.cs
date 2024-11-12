using Game.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Scripts.Interfaces
{
    public interface IRenderizable
    {
        Renderer Renderer { get; }
    }
}
