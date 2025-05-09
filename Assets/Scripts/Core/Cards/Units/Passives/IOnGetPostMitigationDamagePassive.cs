namespace Core.Cards.Units.Passives
{
    public interface IOnGetPostMitigationDamagePassive : IPassive
    {
        void OnGetPostMitigationDamage(ref float mitigatedDamage);
    }

}
