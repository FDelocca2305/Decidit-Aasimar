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

            
            int groundRow = height - 2;
            for (int x = 0; x < width; x++)
            {
                tiles[x, groundRow] = 1;
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

        public void Draw(Camera camera)
        {
            for (int y = 0; y < tiles.GetLength(1); y++)
            {
                for (int x = 0; x < tiles.GetLength(0); x++)
                {
                    if (tiles[x, y] == 1)
                    {
                        // Calcular la posición en pantalla del tile considerando la cámara y el zoom
                        Vector2 tilePosition = camera.WorldToScreen(new Vector2(x * tileWidth, y * tileHeight));

                        // Dibujar el tile con el tamaño ajustado según el zoom
                        Engine.Draw("tile.png", tilePosition.x, tilePosition.y, tileWidth * camera.Zoom, tileHeight * camera.Zoom);
                    }
                }
            }
        }

    }
}