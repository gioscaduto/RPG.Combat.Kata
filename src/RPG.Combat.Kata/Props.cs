namespace RPG.Combat.Kata
{
    public abstract class Props
    {
        public double Health { get; private set; }
        public bool Destroyed { get; private set; }
        
        public Props(double health)
        {
            Destroyed = false;
            Health = health;
        }

        internal void ReduceHealth(double damage)
        {
            Health -= damage;

            if(Health <= 0)
            {
                Health = 0;
                Destroyed = true;
            }
        }
    }
}
