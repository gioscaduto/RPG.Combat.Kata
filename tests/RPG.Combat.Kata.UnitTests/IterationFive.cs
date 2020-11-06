using Xunit;

namespace RPG.Combat.Kata.UnitTests
{
    [Collection((nameof(TestsCollection)))]
    public class IterationFive
    {
        private readonly TestsFixture _testsFixture;

        public IterationFive(TestsFixture testsFixture)
        {
            _testsFixture = testsFixture;
        }

        [Theory]
        [InlineData(600)]
        [InlineData(2000)]
        public void Characters_Can_Damage_Non_Character_Things_Props(double health)
        {
            // Arrange 
            var character = _testsFixture.GenerateRangedGighter();
            var tree = _testsFixture.GenerateTree(health);

            // Act
            character.DealDamage(tree, 1);

            // Assert
            Assert.Equal(health - 1, tree.Health);
            Assert.False(tree.Destroyed);
        }

        [Theory]
        [InlineData(600)]
        [InlineData(2000)]
        public void When_Reduced_To_0_Health_Things_Are_Destroyed(double health)
        {
            // Arrange 
            var character = _testsFixture.GenerateRangedGighter();
            var tree = _testsFixture.GenerateTree(health);
            
            // Act
            character.DealDamage(tree, health);

            // Assert
            Assert.True(tree.Destroyed);
        }
    }
}
