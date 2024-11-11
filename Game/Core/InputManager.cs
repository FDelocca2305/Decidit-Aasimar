using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Core
{
    public static class InputManager
    {
        public static bool IsMoveUp() => Engine.GetKey(Keys.UP);
        public static bool IsMoveDown() => Engine.GetKey(Keys.DOWN);
        public static bool IsMoveLeft() => Engine.GetKey(Keys.LEFT);
        public static bool IsMoveRight() => Engine.GetKey(Keys.RIGHT);
    }
}
