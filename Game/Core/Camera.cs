using Game.Core;
using Game.Scripts;

public class Camera
{
    public Vector2 Position { get; private set; }
    private int screenWidth;
    private int screenHeight;
    private int worldWidth;
    private int worldHeight;
    public float Zoom { get; set; }

    public Camera(int screenWidth, int screenHeight, int worldWidth, int worldHeight, float zoom = 1.0f)
    {
        this.screenWidth = screenWidth;
        this.screenHeight = screenHeight;
        this.worldWidth = worldWidth;
        this.worldHeight = worldHeight;
        this.Zoom = zoom;
        Position = new Vector2(0, 0);
    }

    public void Follow(Character target)
    {
        // Calcula la posición objetivo de la cámara centrada en el personaje
        float targetX = target.Position.x - (screenWidth / 2) / Zoom;
        float targetY = target.Position.y - (screenHeight / 2) / Zoom;

        // Calcular los límites máximos de la cámara considerando el zoom y el tamaño del mundo
        float maxX = worldWidth - screenWidth / Zoom;
        float maxY = worldHeight - screenHeight / Zoom;

        // Asegúrate de que la cámara no se mueva fuera de los límites del mundo
        Position = new Vector2(
            MathUtils.Clamp(targetX, 0, maxX > 0 ? maxX : 0),
            MathUtils.Clamp(targetY, 0, maxY > 0 ? maxY : 0)
        );
    }

    public Vector2 WorldToScreen(Vector2 worldPosition)
    {
        return new Vector2(
            (worldPosition.x - Position.x) * Zoom,
            (worldPosition.y - Position.y) * Zoom
        );
    }
}
