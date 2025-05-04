using UnityEngine;

namespace Core.Cards.Passives
{
    [CreateAssetMenu(menuName = "SO/Passives/Block Damage/Post Mitigation Block", fileName = "Block Post Mitigation Damage")]
    public class BlockDamageOnGetPostMitigationDamagePassive : ScriptableObject, IOnGetPostMitigationDamagePassive
    {
        [SerializeField] private float blockFlat = 5f;
        [SerializeField, Range(0f, 1f)] private float blockPercent = 0f;

        public void Initialize(Unit unit) { }

        public void OnGetPostMitigationDamage(ref float mitigatedDamage)
        {
            float reduction = blockFlat + mitigatedDamage * blockPercent;
            mitigatedDamage = Mathf.Max(0, mitigatedDamage - reduction);
        }
    }
}
