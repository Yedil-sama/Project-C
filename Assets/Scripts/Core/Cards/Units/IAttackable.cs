namespace Core
{
    public interface IAttackable
    {
        Damage TakeDamage(Damage damage);
        bool CanBeSeenBy(IAttacker observer);
    }

}
