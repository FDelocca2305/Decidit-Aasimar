using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Core
{
    public class Transform
    {
        public Vector2 Position { get; set; } = new Vector2(0, 0);
        public Vector2 Rotation { get; set; } = new Vector2(0, 0);
        public Vector2 Scale { get; set; } = new Vector2(1, 1);
    }
}
