namespace Core.Cards.Units.Passives
{
    public interface IOnDealDamagePassive : IPassive
    {
        void OnDealDamage(IAttackable target, ref Damage damage);
    }
}
