namespace Core
{
    public struct Damage
    {
        public float Amount;
        public DamageType Type;
        public IAttacker Attacker;

        public Damage(float amount, IAttacker attacker, DamageType type = DamageType.Physical)
        {
            Amount = amount;
            Attacker = attacker;
            Type = type;
        }
    }

}
