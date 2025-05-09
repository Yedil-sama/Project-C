namespace Core
{
    public interface IAttacker
    {
        Damage DealDamage(IAttackable target, Damage damage);
    }

}
