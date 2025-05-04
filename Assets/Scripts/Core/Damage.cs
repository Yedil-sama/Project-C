using Core.Cards;

namespace Core
{
    public struct Damage
    {
        public float Amount;
        public DamageType Type;
        public Unit Attacker;

        public Damage(float amount, DamageType type = DamageType.Physical, Unit attacker = null)
        {
            Amount = amount;
            Type = type;
            Attacker = attacker;
        }
    }

}
