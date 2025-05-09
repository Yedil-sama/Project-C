using UnityEngine;

namespace Core.Cards.Units.Passives
{
    [CreateAssetMenu(menuName = "SO/Passives/Block Damage/Pre Mitigation Block", fileName = "Block Pre Mitigation Damage")]
    public class BlockDamageOnGetPreMitigationDamagePassive : ScriptableObject, IOnGetPreMitigationDamagePassive
    {
        [SerializeField] private float blockAmount = 5f;
        [SerializeField, Range(0f, 1f)] private float blockPercent = 0f;

        public void Initialize(Unit unit) { }

        public void OnGetPreMitigationDamage(ref Damage damage)
        {
            float reduction = blockAmount + damage.Amount * blockPercent;
            damage.Amount = Mathf.Max(0, damage.Amount - reduction);
        }
    }
}
