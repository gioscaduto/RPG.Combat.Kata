using System;
using Xunit;

namespace RPG.Combat.Kata.UnitTests
{
    [Collection(nameof(TestsCollection))]
    public class IterationThree
    {
        private readonly TestsFixture _testsFixture;

        public IterationThree(TestsFixture testsFixture)
        {
            _testsFixture = testsFixture;
        }

        [Theory]
        [InlineData(5)]
        [InlineData(750)]
        public void Characters_Must_Be_In_Range_To_Deal_Damage_To_A_Target(double damage)
        {
            // Arrange
            var attacker = _testsFixture.GenerateRangedGighter();
            var target = _testsFixture.GenerateMeleeFighter();

            // Act
            _testsFixture.Run(3, target);
            var targetHealthBeforeDamage = target.Health;
            attacker.DealDamage(target, damage);

            // Assert
            Assert.Equal(targetHealthBeforeDamage - damage, target.Health);
        }

        [Fact]
        public void RangedGighter_Have_An_Attack_Max_Range()
        {
            // Arrange
            var attacker = _testsFixture.GenerateRangedGighter();
            var target = _testsFixture.GenerateMeleeFighter();

            // Act && Assert
            _testsFixture.Run(11, target);
            Assert.Throws<Exception>(() => attacker.DealDamage(target, 1));
        }

        [Fact]
        public void MeleeFighterr_Have_An_Attack_Max_Range()
        {
            // Arrange
            var attacker = _testsFixture.GenerateMeleeFighter();
            var target = _testsFixture.GenerateRangedGighter();

            // Act && Assert
            _testsFixture.Run(3, target);
            Assert.Throws<Exception>(() => attacker.DealDamage(target, 1));
        }

        [Fact]
        public void Melee_Fighters_Have_A_Range_Of_2_meters()
        {
            // Arrange
            var meleeFighter = _testsFixture.GenerateMeleeFighter();

            //Act && Assert
            Assert.Equal(2, meleeFighter.AttackMaxRange);
        }

        [Fact]
        public void RangedGighter_Have_A_Range_Of_20_meters()
        {
            // Arrange
            var rangedGighter = _testsFixture.GenerateRangedGighter();

            //Act && Assert
            Assert.Equal(20, rangedGighter.AttackMaxRange);
        }
    }
}
