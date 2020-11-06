using System;
using Xunit;

namespace RPG.Combat.Kata.UnitTests
{
    [CollectionDefinition(nameof(TestsCollection))]
    public class TestsCollection : ICollectionFixture<TestsFixture> 
    {}

    public class TestsFixture : IDisposable
    {
        public MeleeFighter GenerateMeleeFighter()
        {
            return new MeleeFighter();
        }

        public RangedGighter GenerateRangedGighter()
        {
            return new RangedGighter();
        }

        public void UpLevel(int level, Character character)
        {
            if (level < 5) level = 5;

            var opponent = new RangedGighter();

            for (int i = 0; i < (level * 12); i++)  // 5000 damage to next level
            {
                character.DealDamage(opponent, 500);
                opponent.Heal(1000);
            }
        }

        public void Run(int times, Character character)
        {
            for (int i = 0; i < times; i++)
            {
                character.Run();
            }
        }

        public BrazilFaction GenerateBrazilFaction()
        {
            return new BrazilFaction();
        }

        public UsaFaction GenerateUsaFaction()
        {
            return new UsaFaction();
        }

        public Tree GenerateTree(double health)
        {
            return new Tree(health);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
