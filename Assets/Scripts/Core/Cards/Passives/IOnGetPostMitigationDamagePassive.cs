namespace Core.Cards.Passives
{
    public interface IOnGetPostMitigationDamagePassive : IPassive
    {
        void OnGetPostMitigationDamage(ref float mitigatedDamage);
    }

}
