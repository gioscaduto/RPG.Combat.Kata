using System;
using Xunit;

namespace RPG.Combat.Kata.UnitTests
{
    [Collection((nameof(TestsCollection)))]
    public class IterationFour
    {
        private readonly TestsFixture _testsFixture;
        
        public IterationFour(TestsFixture testsFixture)
        {
            _testsFixture = testsFixture;
        }

        [Fact]
        public void Newly_Created_Characters_Belong_To_No_Faction()
        {
            // Arrange && Act
            var character = _testsFixture.GenerateRangedGighter();

            // Assert
            Assert.Empty(character.GetFactions());
        }

        [Fact]
        public void A_Character_May_Join_One_Or_More_Factions()
        {
            // Arrange
            var character = _testsFixture.GenerateRangedGighter();

            // Act
            var brazilFaction = _testsFixture.GenerateBrazilFaction();
            var usaFaction = _testsFixture.GenerateUsaFaction();
          
            character.JoinFaction(brazilFaction);
            character.JoinFaction(usaFaction);

            // Assert
            Assert.Equal(2, character.GetFactions().Count);
        }

        [Fact]
        public void A_Character_May_Leave_One_Or_More_Factions()
        {
            // Arrange
            var character = _testsFixture.GenerateRangedGighter();

            // Act
            var brazilFaction = _testsFixture.GenerateBrazilFaction();
            var usaFaction = _testsFixture.GenerateUsaFaction();
           
            character.JoinFaction(brazilFaction);
            character.JoinFaction(usaFaction);
            character.LeaveFaction(brazilFaction);
            character.LeaveFaction(usaFaction);

            // Assert
            Assert.Empty(character.GetFactions());
        }

        [Fact]
        public void Players_Belonging_To_The_Same_Faction_Are_Considered_Allies()
        {
            // Arrange
            var rangedGighter = _testsFixture.GenerateRangedGighter();
            var meleeFighter = _testsFixture.GenerateMeleeFighter();

            // Act
            var brazilFaction = _testsFixture.GenerateBrazilFaction();
            var usaFaction = _testsFixture.GenerateUsaFaction();

            rangedGighter.JoinFaction(brazilFaction);
            rangedGighter.JoinFaction(usaFaction);
            meleeFighter.JoinFaction(brazilFaction);

            // Assert
            Assert.True(rangedGighter.IsAllie(meleeFighter));
        }

        [Fact]
        public void Allies_Cannot_Deal_Damage_To_One_Another()
        {
            // Arrange
            var rangedGighter = _testsFixture.GenerateRangedGighter();
            var meleeFighter = _testsFixture.GenerateMeleeFighter();

            // Act
            var brazilFaction = _testsFixture.GenerateBrazilFaction();
            var usaFaction = _testsFixture.GenerateUsaFaction();

            rangedGighter.JoinFaction(brazilFaction);
            meleeFighter.JoinFaction(brazilFaction);
           
            // Assert
            Assert.Throws<Exception>(() => rangedGighter.DealDamage(meleeFighter, 1));
        }

        [Theory]
        [InlineData(30)]
        [InlineData(900)]
        public void Allies_Can_Heal_One_Another(double damage)
        {
            // Arrange
            var rangedGighter = _testsFixture.GenerateRangedGighter();
            var meleeFighter = _testsFixture.GenerateMeleeFighter();
            var enemy = _testsFixture.GenerateRangedGighter();

            // Act
            var brazilFaction = _testsFixture.GenerateBrazilFaction();
           
            rangedGighter.JoinFaction(brazilFaction);
            meleeFighter.JoinFaction(brazilFaction);
            enemy.DealDamage(rangedGighter, damage);
            meleeFighter.HealAllie(rangedGighter, damage);

            // Assert
            Assert.Equal(1000, rangedGighter.Health);
        }

        [Theory]
        [InlineData(30)]
        [InlineData(900)]
        public void Character_Can_Not_Heal_One_Character_In_Different_Factions(double damage)
        {
            // Arrange
            var rangedGighter = _testsFixture.GenerateRangedGighter();
            var meleeFighter = _testsFixture.GenerateMeleeFighter();
            var enemy = _testsFixture.GenerateRangedGighter();

            // Act
            var brazilFaction = _testsFixture.GenerateBrazilFaction();
            rangedGighter.JoinFaction(brazilFaction);

            enemy.DealDamage(rangedGighter, damage);
            
            // Assert
            Assert.Throws<Exception>(() => meleeFighter.HealAllie(rangedGighter, damage));
        }
    }
}
