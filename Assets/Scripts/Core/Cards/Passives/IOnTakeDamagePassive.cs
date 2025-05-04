namespace Core.Cards.Passives
{
    public interface IOnTakeDamagePassive : IPassive
    {
        void OnTakeDamage(ref Damage damage);
    }

}
