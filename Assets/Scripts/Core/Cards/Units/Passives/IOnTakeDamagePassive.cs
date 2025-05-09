namespace Core.Cards.Units.Passives
{
    public interface IOnTakeDamagePassive : IPassive
    {
        void OnTakeDamage(ref Damage damage);
    }
}
