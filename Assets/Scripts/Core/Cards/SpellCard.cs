using Core.Cards.Spells;
using UnityEngine;

namespace Core.Cards
{
    [CreateAssetMenu(menuName = "SO/Cards/Spell Card", fileName = "Spell Card")]
    public class SpellCard : Card
    {
        [SerializeField] private GameObject spellPrefab;

        public override void Play(Player owner, Vector3 position)
        {
            if (spellPrefab == null)
            {
                Debug.LogWarning($"SpellCard '{CardName}' has no assigned prefab.");
                return;
            }

            GameObject spellInstance = Instantiate(spellPrefab, position, Quaternion.identity);

            if (spellInstance.TryGetComponent(out ISpell spell))
            {
                spell.Cast(owner, position);
            }
        }
    }
}
