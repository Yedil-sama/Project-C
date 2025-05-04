namespace Core.Cards.Passives
{
    public interface IOnGetPreMitigationDamagePassive : IPassive
    {
        void OnGetPreMitigationDamage(ref Damage damage);
    }

}
