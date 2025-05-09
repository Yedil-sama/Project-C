namespace Core.Cards.Units.Passives
{
    public interface IOnGetPreMitigationDamagePassive : IPassive
    {
        void OnGetPreMitigationDamage(ref Damage damage);
    }

}
