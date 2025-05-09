using UnityEngine;
using Core.Cards.Units;

namespace Core.Cards
{
    [CreateAssetMenu(menuName = "SO/Cards/Unit Card", fileName = "Unit Card")]
    public class UnitCard : Card
    {
        [SerializeField] private Unit unitPrefab;

        public override void Play(Player owner, Vector3 position)
        {
            Unit unitInstance = Object.Instantiate(unitPrefab, position, Quaternion.identity);
            unitInstance.Initialize(owner);
            unitInstance.Play(position);
        }
    }
}
