// Game.Core/TileMap.cs
using System;

namespace Game.Core
{
    public class TileMap
    {
        private int[,] tiles;
        private int tileWidth;
        private int tileHeight;

        public TileMap(int width, int height, int tileWidth, int tileHeight)
        {
            tiles = new int[width, height];
            this.tileWidth = tileWidth;
            this.tileHeight = tileHeight;
            
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (y > height / 2)
                        tiles[x, y] = 1;
                    else
                        tiles[x, y] = 0;
                }
            }
        }

        public bool CheckCollision(PhysicsObject obj)
        {
            int tileX = (int)(obj.Position.x / tileWidth);
            int tileY = (int)(obj.Position.y / tileHeight);
            
            if (tileX < 0 || tileX >= tiles.GetLength(0) || tileY < 0 || tileY >= tiles.GetLength(1))
            {
                return false;
            }
            
            return tiles[tileX, tileY] == 1;
        }

        public void Draw()
        {
            for (int y = 0; y < tiles.GetLength(1); y++)
            {
                for (int x = 0; x < tiles.GetLength(0); x++)
                {
                    if (tiles[x, y] == 1)
                    {
                        Engine.Draw("character.png", x * tileWidth, y * tileHeight);
                    }
                }
            }
        }
    }
}