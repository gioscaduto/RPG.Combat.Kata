using System.Collections.Generic;

namespace RPG.Combat.Kata
{
    public abstract class Faction
    {
        protected List<Character> Characters { get; set; } = new List<Character>();

        public void AddChacter(Character character)
        {
            Characters.Add(character);
        }

        public void RemoveChacter(Character character)
        {
            Characters.Remove(character);
        }
    }
}
