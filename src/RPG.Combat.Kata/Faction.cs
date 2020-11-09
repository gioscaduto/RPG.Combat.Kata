using System.Collections.Generic;

namespace RPG.Combat.Kata
{
    public abstract class Faction
    {
        public string Name { get; private set; }
        protected List<Character> Characters { get; private set; } = new List<Character>();

        protected Faction(string name)
        {
            Name = name;
        }

        public void AddCharacter(Character character)
        {
            Characters.Add(character);
        }

        public void RemoveCharacter(Character character)
        {
            Characters.Remove(character);
        }

        public override bool Equals(object obj)
        {
            var anotherFaction = obj as Faction;

            if(anotherFaction != null)
            {
                return anotherFaction.Name.Trim().ToLower() == Name.Trim().ToLower();
            }

            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
