using System;
using System.Collections.Generic;

namespace Game
{
    public class Program
    {
        static void Main(string[] args)
        {
            var gameEngine = new GameEngine();
            gameEngine.Initialize();
            gameEngine.Run();
        }
    }
}