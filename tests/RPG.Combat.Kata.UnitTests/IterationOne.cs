using System;
using Xunit;

namespace RPG.Combat.Kata.UnitTests
{
    [Collection(nameof(TestsCollection))]
    public class IterationOne
    {
        private readonly TestsFixture _testsFixture;

        public IterationOne(TestsFixture testsFixture)
        {
            _testsFixture = testsFixture;
        }

        [Fact]
        public void MeleeFighter_Created_Health_Equals_1000()
        {
            // Arrange e Act 
            var meleeFighter = _testsFixture.GenerateMeleeFighter();
            
            // Assert
            Assert.Equal(1000, meleeFighter.Health);
        }

        [Fact]
        public void RangedGighter_Created_Health_Equals_1000()
        {
            // Arrange e Act 
            var rangedGighter = _testsFixture.GenerateRangedGighter();

            // Assert
            Assert.Equal(1000, rangedGighter.Health);
        }

        [Fact]
        public void MeleeFighter_Created_Level_Equals_1()
        {
            // Arrange e Act 
            var meleeFighter = _testsFixture.GenerateMeleeFighter();

            // Assert
            Assert.Equal(1, meleeFighter.Level);
        }

        [Fact]
        public void RangedGighter_Created_Level_Equals_1()
        {
            // Arrange e Act 
            var rangedGighter = _testsFixture.GenerateRangedGighter();

            // Assert
            Assert.Equal(1, rangedGighter.Level);
        }

        
        [Fact]
        public void MeleeFighter_Created_Is_Alive()
        {
            // Arrange e Act 
            var meleeFighter = _testsFixture.GenerateMeleeFighter();

            // Assert
            Assert.True(meleeFighter.Alive);
        }

        [Fact]
        public void RangedGighter_Created_Is_Alive()
        {
            // Arrange e Act 
            var rangedGighter = _testsFixture.GenerateRangedGighter();

            // Assert
            Assert.True(rangedGighter.Alive);
        }

        [Fact]
        public void Damage_Opponent_Is_Subtracted_From_Heath()
        {
            // Arrange
            var character = _testsFixture.GenerateMeleeFighter();
            var opponent = _testsFixture.GenerateRangedGighter();

            //Act
            character.DealDamage(opponent, 70);

            // Assert
            Assert.Equal(930, opponent.Health);
        }

        [Fact]
        public void Opponent_Is_Died_When_Damage_Exceeds_Current_Heath()
        {
            // Arrange
            var character = _testsFixture.GenerateMeleeFighter();
            var opponent = _testsFixture.GenerateRangedGighter();

            //Act
            character.DealDamage(opponent, 1001);

            // Assert
            Assert.Equal(0, opponent.Health);
            Assert.False(opponent.Alive);
        }

        [Fact]
        public void Dead_Characters_Cannot_Be_Healed()
        {
            // Arrange
            var character = _testsFixture.GenerateMeleeFighter();
            var opponent = _testsFixture.GenerateRangedGighter();

            //Act
            character.DealDamage(opponent, 1000);
            
            // Assert
            Assert.Throws<Exception>(() => opponent.Heal(1000));
        }

        [Fact]
        public void Dead_Allie_Cannot_Be_Healed()
        {
            // Arrange
            var character = _testsFixture.GenerateMeleeFighter();
            var allie = _testsFixture.GenerateRangedGighter();
            var enemy = _testsFixture.GenerateRangedGighter();

            //Act
            var brazilFaction = _testsFixture.GenerateBrazilFaction();
            
            character.JoinFaction(brazilFaction);
            allie.JoinFaction(brazilFaction);
            enemy.DealDamage(allie, 1000);

            // Assert
            Assert.Throws<Exception>(() => character.HealAllie(allie, 1000));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(1001)]
        [InlineData(2001)]
        public void Healing_Cannot_Raise_Health_Above_1000(double health)
        {
            // Arrange
            var character = _testsFixture.GenerateMeleeFighter();
            
            //Act
            character.Heal(health);

            // Assert
            Assert.Equal(1000, character.Health);
        }
    }
}
