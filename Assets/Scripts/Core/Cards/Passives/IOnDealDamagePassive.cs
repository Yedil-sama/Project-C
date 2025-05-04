namespace Core.Cards.Passives
{
    public interface IOnDealDamagePassive : IPassive
    {
        void OnDealDamage(Unit target, ref Damage damage);
    }
}
