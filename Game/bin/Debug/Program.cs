using System;
using System.Collections.Generic;

namespace Game
{
    public class Program
    {
        static void Main(string[] args)
        {
            var gameEngine = new GameEngine();
            new Game();
            gameEngine.Run();
        }
    }
}