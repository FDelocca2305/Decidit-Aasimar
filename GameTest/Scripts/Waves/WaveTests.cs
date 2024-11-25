using Game.Scripts.Waves;
using Game.Scripts;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Core;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using Game.Core.Interfaces;
using Game.Scripts.Interfaces;
using Game.Scripts.Utils;
using Moq;

namespace Test
{
    public class WaveTests
    {
        private ObjectManager objectManager;
        private Player player;
        private BasicWave basicWave;
        private Mock<IUIManager> uiManagerMock;
        private Mock<ILevelManager> levelManagerMock;

        [SetUp]
        public void Setup()
        {
            ConfigLoader.LoadConfigurations("Scripts/Utils/testConfigs.json");
            
            uiManagerMock = new Mock<IUIManager>();
            levelManagerMock = new Mock<ILevelManager>();
            
            player = new Player(0, 0, uiManagerMock.Object, levelManagerMock.Object);
            objectManager = new ObjectManager(player);
            basicWave = new BasicWave(1, objectManager, player);
        }

        [Test]
        public void StartWave_ShouldSpawnEnemies()
        {
            // Act
            basicWave.StartWave();

            // Assert
            var enemies = objectManager.GameObjectsManager.GetObjectsToAdd().OfType<Enemy>();
            Assert.IsTrue(enemies.Any());
            Assert.AreEqual(ConfigLoader.WaveConfig.BasicWaveEnemyCount, enemies.Count());
        }

        [Test]
        public void CalculateEnemyCountForWave_ShouldReturnExpectedValue()
        {
            // Arrange
            int baseEnemyCount = ConfigLoader.WaveConfig.BasicWaveEnemyCount;
            
            // Act
            int enemyCount = basicWave.CalculateEnemyCountForWave(5);

            // Assert
            Assert.AreEqual(baseEnemyCount, enemyCount);
        }
    }
}
