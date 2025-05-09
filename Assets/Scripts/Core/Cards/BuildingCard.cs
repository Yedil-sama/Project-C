using UnityEngine;

namespace Core.Cards
{
    [CreateAssetMenu(menuName = "SO/Cards/Building Card", fileName = "Building Card")]
    public class BuildingCard : Card
    {
        [SerializeField] private GameObject buildingPrefab;

        public override void Play(Player owner, Vector3 position) => GameObject.Instantiate(buildingPrefab, position, Quaternion.identity);
    }
}
