using NUnit.Framework;
using Moq;
using Game.Core.Interfaces;
using Game.Scripts.Interfaces;
using Game.Scripts;
using Game.Scripts.Utils;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace Test
{
    public class PlayerTests
    {
        private Player player;
        private Mock<IUIManager> uiManagerMock;
        private Mock<ILevelManager> levelManagerMock;

        [SetUp]
        public void Setup()
        {
            ConfigLoader.LoadConfigurations("Scripts/Utils/testConfigs.json");
            
            uiManagerMock = new Mock<IUIManager>();
            levelManagerMock = new Mock<ILevelManager>();
            
            player = new Player(
                ConfigLoader.PlayerConfig.Health, 
                200, 
                uiManagerMock.Object, 
                levelManagerMock.Object
            );
        }

        [Test]
        public void TakeDamage_ShouldDecreaseHealth()
        {
            // Arrange
            int initialHealth = player.Health;
            int damage = ConfigLoader.PlayerConfig.BaseDamageTaken;

            // Act
            player.TakeDamage(damage);

            // Assert
            Assert.AreEqual(initialHealth - damage, player.Health);
            uiManagerMock.Verify(ui => ui.UpdateHealth(player.Health, player.MaxHealth), Times.Once);
        }

        [Test]
        public void TakeDamage_ShouldSetInactive_WhenHealthIsZero()
        {
            // Act
            player.TakeDamage(player.Health);

            // Assert
            Assert.AreEqual(0, player.Health);
            Assert.AreEqual(false, player.IsActive);
        }

        [Test]
        public void CollectExperience_ShouldCallLevelManagerAndUpdateUI()
        {
            // Arrange
            var orb = new ExperienceOrb(0, 0, 50);

            // Act
            player.CollectExperience(orb);

            // Assert
            levelManagerMock.Verify(lm => lm.AddExperience(50), Times.Once);
            uiManagerMock.Verify(ui => ui.UpdateExperience(It.IsAny<int>(), It.IsAny<int>()), Times.Once);
        }
    }
}
