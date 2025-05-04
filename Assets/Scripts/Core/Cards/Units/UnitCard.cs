using UnityEngine;

namespace Core.Cards
{
    public class UnitCard : ScriptableObject, ICard
    {
        [SerializeField] private string cardName;
        public string CardName => cardName;

        [SerializeField] private int manaCost;
        public int ManaCost => manaCost;

        public GameObject unitPrefab;

        public virtual void Play(Vector3 targetPosition) => Object.Instantiate(unitPrefab, targetPosition, Quaternion.identity);

    }

}
