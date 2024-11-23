﻿using Game.Scripts.Enemies;
using Game.Scripts;
using NUnit.Framework;
using System;
using Game.Core;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using Game.Core.Interfaces;
using Game.Scripts.Interfaces;
using Moq;

namespace Test
{
    public class EnemyTests
    {
        private BasicEnemy enemy;
        private Player player;
        private Mock<IUIManager> uiManagerMock;
        private Mock<ILevelManager> levelManagerMock;
        [SetUp]
        public void Setup()
        {
            uiManagerMock = new Mock<IUIManager>();
            levelManagerMock = new Mock<ILevelManager>();
            player = new Player(0, 0, uiManagerMock.Object, levelManagerMock.Object);
            enemy = new BasicEnemy();
            enemy.Reset(new Vector2(100, 100), 50, 20, player);
        }

        [Test]
        public void MoveTowardsPlayer_ShouldUpdatePosition()
        {
            // Arrange
            Vector2 initialPosition = enemy.Transform.Position;

            // Act
            enemy.Update(1.0f);

            // Assert
            Assert.AreNotEqual(initialPosition, enemy.Transform.Position);
            Assert.IsTrue(
                Math.Abs(player.Transform.Position.X - enemy.Transform.Position.X) <
                Math.Abs(initialPosition.X - player.Transform.Position.X)
            );
        }

        [Test]
        public void TakeDamage_ShouldDecreaseHealth()
        {
            // Arrange
            float initialHealth = 50;
            enemy.Reset(new Vector2(0, 0), initialHealth, 10, player);

            // Act
            enemy.TakeDamage(20);

            // Assert
            Assert.AreEqual(initialHealth - 20, enemy.CurrentHealth);
        }

        [Test]
        public void TakeDamage_ShouldTriggerOnDeath_WhenHealthIsZero()
        {
            // Arrange
            bool isDead = false;
            enemy.OnDeath += e => isDead = true;

            // Act
            enemy.TakeDamage(50);

            // Assert
            Assert.AreEqual(true, isDead);
            Assert.AreEqual(false, enemy.IsActive);
        }
    }
}
