using System;
using System.Collections.Generic;
using System.Linq;

namespace RPG.Combat.Kata
{
    public abstract class Character
    {
        public string NickName { get; private set; }
        public double Health { get; private set; }
        public int Level { get; private set; }
        public bool Alive { get; private set; }
        public int AttackMaxRange { get; private set; }
        public int Position { get; private set; }
        protected List<Faction> Factions { get; private set; } = new List<Faction>();
        public double TotalDamage { get; private set; }

        protected Character(int attackMaxRange, string nickName)
        {
            Health = 1000;
            Level = 1;
            Alive = true;
            AttackMaxRange = attackMaxRange;
            Position = 0;
            NickName = nickName;
        }

        public void DealDamage(Character opponent, double damage)
        {
            if (opponent == null) throw new Exception("This opponent is invalid.");

            if (opponent == this) throw new Exception("A Character cannot Deal Damage to itself.");

            if(IsAllie(opponent)) throw new Exception("Allies cannot Deal Damage to one another.");

            VerifyOpponentPosition(opponent);

            damage = VerifyDamageByLevel(opponent, damage);

            if (damage >= opponent.Health)
            {
                opponent.Health = 0;
                opponent.Alive = false;
            }
            else
            {
                opponent.Health -= damage;
            }

            VerifyUpLevel(damage);
        }

        public void DealDamage(Props props, double damage)
        {
            if(props == null) throw new Exception("This props is invalid.");

            props.ReduceHealth(damage);
        }

        protected double VerifyDamageByLevel(Character opponent, double damage)
        {
            if (opponent == null) throw new Exception("This opponent is invalid.");

            if (opponent.Level - Level >= 5) 
                return damage *= 0.5;
            
            if (Level - opponent.Level >= 5)
                return damage *= 1.5;

            return damage;
        }

        protected void VerifyOpponentPosition(Character opponent)
        {
            if (opponent.Position != Position)
            {
                if (opponent.Position > Position)
                {
                    if(opponent.Position - Position > AttackMaxRange)
                    {
                        throw new Exception("Opponent must be in range to deal damage");
                    }
                }
                else
                {
                    if (Position - opponent.Position  > AttackMaxRange)
                    {
                        throw new Exception("Opponent must be in range to deal damage");
                    }
                }
            }
        }

        public void Heal(double health)
        {
            if (health <= 0) throw new Exception("The health must be greater than 0");

            if (!Alive) throw new Exception("Dead characters cannot be healed");

            Health += health;

            if (Health > 1000) Health = 1000;
        }

        public void HealAllie(Character allie, double health)
        {
            if (allie == null) throw new Exception("This allie is invalid.");

            if (!IsAllie(allie)) throw new Exception("This character isn't your allie");

            if (health <= 0) throw new Exception("The health must be greater than 0");

            if (!allie.Alive) throw new Exception("Dead characters cannot be healed");

            allie.Health += health;

            if (allie.Health > 1000) allie.Health = 1000;
        }

        public void Walk()
        {
            Position += 1;
        }

        public void Back()
        {
            Position -= 1;
            if (Position < 0) Position = 0;
        }

        public void Run()
        {
            Position += 3;
        }

        public void RunBack()
        {
            Position -= 3;
            if (Position < 0) Position = 0;
        }

        public List<Faction> GetFactions()
        {
            return new List<Faction>(Factions);
        }

        public void JoinFaction(Faction faction)
        {
            if (faction == null) throw new Exception("This faction is invalid.");

            if (Factions.Any(f => f.Equals(faction)))
            {
                throw new Exception("You already belong to this faction");
            }

            faction.AddCharacter(this);
            Factions.Add(faction);
        }

        public void LeaveFaction(Faction faction)
        {
            if (faction == null) throw new Exception("This faction is invalid.");

            if (!Factions.Any(f => f.Equals(faction)))
            {
                throw new Exception("You don't belong to this faction");
            }

            faction.RemoveCharacter(this);
            Factions.Remove(faction);
        }

        public bool IsAllie(Character opponent)
        {
            if (opponent == null) throw new Exception("This opponent is invalid.");

            if (opponent.Factions.Count == 0 || Factions.Count == 0) return false;

            return opponent.Factions.Any(of => Factions.Any(f => f.Equals(of)));
        }

        public void VerifyUpLevel(double damage)
        {
            if(TotalDamage + damage >= 5000)
            {
                TotalDamage = 0;
                Level += 1;
            }
            else
            {
                TotalDamage += damage;
            }
        }
    }
}
