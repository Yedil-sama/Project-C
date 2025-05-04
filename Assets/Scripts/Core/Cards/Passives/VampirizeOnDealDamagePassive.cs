using UnityEngine;

namespace Core.Cards.Passives
{
    [CreateAssetMenu(menuName = "SO/Passives/On Deal Damage/Vampirize", fileName = "Vampirize")]
    public class VampirizeDealDamagePassive : ScriptableObject, IOnDealDamagePassive
    {
        [SerializeField, Range(0f, 100f)] private float vampirizePercent = 0.1f;
        private Unit owner;

        public void Initialize(Unit unit)
        {
            owner = unit;
        }

        public void OnDealDamage(Unit target, ref Damage damage)
        {
            if (owner == null || damage.Amount <= 0f) return;

            float healAmount = damage.Amount * vampirizePercent;
            owner.HealUp(healAmount);
        }
    }
    
}
