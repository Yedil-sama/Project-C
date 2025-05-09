using UnityEngine;

namespace Core.Cards.Units.Passives
{
    [CreateAssetMenu(menuName = "SO/Passives/Last Whisper/Revive", fileName = "Revive")]
    public class ReviveOnDiePassive : ScriptableObject, IOnDiePassive
    {
        [SerializeField] protected int maxRevives = 1;
        [SerializeField] protected float healthFlatOnRevive = 0;
        [SerializeField, Range(0, 1f)] protected float healthPercentOnRevive = 0.5f;

        protected Unit owner;
        private int leftRevives;

        public void Initialize(Unit owner)
        {
            this.owner = owner; 
        }

        public virtual void OnDie()
        {
            if (leftRevives <= 0) return;

            leftRevives--;

            GameObject revivedGO = Object.Instantiate(owner.gameObject, owner.transform.position, Quaternion.identity);
            if (revivedGO.TryGetComponent<Unit>(out Unit revivedUnit))
            {
                revivedUnit.SetHealth(healthFlatOnRevive + revivedUnit.MaxHealth * healthPercentOnRevive);
            }
        }
    }

}
