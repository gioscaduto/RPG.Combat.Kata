using System;
using Xunit;

namespace RPG.Combat.Kata.UnitTests
{
    [Collection(nameof(TestsCollection))]
    public class IterationTwo
    {
        private readonly TestsFixture _testsFixture;

        public IterationTwo(TestsFixture testsFixture)
        {
            _testsFixture = testsFixture;
        }

        [Fact]
        public void Character_Cannot_Deal_Damage_To_Itself()
        {
            // Arrange
            var character = _testsFixture.GenerateRangedGighter();

            //Act & Assert
            Assert.Throws<Exception>( () => character.DealDamage(character, 50));
        }

        [Theory]
        [InlineData(30, 10)]
        [InlineData(50, 5)]
        [InlineData(40.2, 9.9)]
        public void Character_Can_Only_Heal_ItSelf(double damage, double health)
        {
            // Arrange
            var character = _testsFixture.GenerateMeleeFighter();
            var opponent = _testsFixture.GenerateRangedGighter();

            //Act
            var opponentHealthBeforeDamage = opponent.Health;
            character.DealDamage(opponent, damage);
            opponent.Heal(health);

            // Assert
            Assert.Equal(opponentHealthBeforeDamage - damage + health, opponent.Health);
        }

        [Theory]
        [InlineData(100, 20)]
        [InlineData(105.2, 5)]
        public void Character_Cannot_Heal_Enemy(double damage, double health)
        {
            // Arrange
            var character = _testsFixture.GenerateMeleeFighter();
            var opponent = _testsFixture.GenerateRangedGighter();

            //Act && Assert
            character.DealDamage(opponent, damage);
            Assert.Throws<Exception>(() => character.HealAllie(opponent, health));
        }

        [Theory]
        [InlineData(5, 100)]
        [InlineData(6, 200)]
        [InlineData(7, 53)]
        [InlineData(50, 341)]
        public void If_The_Target_Is_5_Or_More_Levels_Above_The_Attacker_Damage_Is_Reduced_by_50_Percent
            (int targetLevel, double damage)
        {
            // Arrange
            var attacker = _testsFixture.GenerateMeleeFighter();
            var target = _testsFixture.GenerateRangedGighter();

            //Act 
            _testsFixture.UpLevel(targetLevel, target);
            var targetHealthBeforeDamage = target.Health;
            attacker.DealDamage(target, damage);

            // Assert
            Assert.Equal(targetHealthBeforeDamage - (damage * 0.5), target.Health);
        }

        [Theory]
        [InlineData(5, 100)]
        [InlineData(6, 200)]
        [InlineData(10, 500)]
        [InlineData(51, 359)]
        public void If_The_Target_Is_5_Or_More_Levels_Below_The_Attacker_Damage_Is_Increased_by_50_Percent
            (int attackerLevel, double damage)
        {
            // Arrange
            var attacker = _testsFixture.GenerateMeleeFighter();
            var target = _testsFixture.GenerateRangedGighter();

            //Act 
            _testsFixture.UpLevel(attackerLevel, attacker);
            var targetHealthBeforeDamage = target.Health;
            attacker.DealDamage(target, damage);

            // Assert
            Assert.Equal(targetHealthBeforeDamage - (damage * 1.5), target.Health);
        }
    }
}
