using System;
using System.IO;
using Newtonsoft.Json;

namespace Game.Scripts.Utils
{
    public static class ConfigLoader
    {
        public static PlayerConfig PlayerConfig { get; private set; }
        public static EnemyConfig EnemyConfig { get; private set; }
        public static WaveConfig WaveConfig { get; private set; }
        public static LevelConfig LevelConfig { get; private set; }

        public static void LoadConfigurations(string filePath)
        {
            try
            {
                string json = File.ReadAllText(filePath);
                
                var config = JsonConvert.DeserializeObject<GameConfig>(json);
                
                if (config == null) throw new JsonSerializationException("Archivo de configuración vacío o inválido.");
                PlayerConfig = config.PlayerConfig ?? new PlayerConfig();
                EnemyConfig = config.EnemyConfig ?? new EnemyConfig();
                WaveConfig = config.WaveConfig ?? new WaveConfig();
                LevelConfig = config.LevelConfig ?? new LevelConfig();
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Error leyendo el archivo JSON: {ex.Message}");
                throw;
            }
            catch (JsonSerializationException ex)
            {
                Console.WriteLine($"Error deserializando el archivo JSON: {ex.Message}");
                throw;
            }
        }
    }
    [Serializable]
    public class GameConfig
    {
        public PlayerConfig PlayerConfig { get; set; }
        public EnemyConfig EnemyConfig { get; set; }
        public WaveConfig WaveConfig { get; set; }
        public LevelConfig LevelConfig { get; set; }
    }

    [Serializable]
    public class PlayerConfig
    {
        public int BaseDamageTaken { get; set; }
        public int Health { get; set; }
        public float MaxHealth { get; set; }
        public float Speed { get; set; }
        public float AttackCooldown { get; set; }
        public float AttackDuration { get; set; }
        public float Attack { get; set; }
        public float InvulnerabilityTime { get; set; }
    }

    [Serializable]
    public class EnemyConfig
    {
        public EnemyStats BasicEnemy { get; set; }
        public EnemyStats FastEnemy { get; set; }
        public EnemyStats Boss { get; set; }
    }

    [Serializable]
    public class EnemyStats
    {
        public int Health { get; set; }
        public float Speed { get; set; }
    }

    [Serializable]
    public class WaveConfig
    {
        public int BasicWaveEnemyCount { get; set; }
        public int SpecialWaveEnemyCount { get; set; }
        public float TimeBetweenWaves { get; set; }
        public int BasicWavesBeforeSpecial { get; set; }
        public float TimeUntilFirstWave { get; set; }
        public float TimeUntilGameEnds { get; set; }
    }

    [Serializable]
    public class LevelConfig
    {      
        public int InitialLevel { get; set; }
        public int ExperienceToNextLevel { get; set; }
    }
}
