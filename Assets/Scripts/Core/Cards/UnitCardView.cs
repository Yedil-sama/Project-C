using UnityEngine;
using TMPro;
using Core.Cards.Units;

namespace Core.Cards
{
    public class UnitCardView : CardView
    {
        [SerializeField] private TMP_Text healthText;
        [SerializeField] private TMP_Text attackText;

        [SerializeField] private UnitCard unitCard;

        public override void Initialize(ICard card)
        {
            base.Initialize(card);

            if (card is UnitCard newUnitCard)
            {
                if (unitCard != null) unitCard.OnStatsChanged -= OnStatChanged;

                unitCard = newUnitCard;
                unitCard.OnStatsChanged += OnStatChanged;

                UpdateStatTexts();
            }
            else
            {
                Debug.LogWarning("Tried to initialize UnitCardView with non-UnitCard");
            }
        }

        protected override void Awake()
        {
            base.Awake();
            Initialize(unitCard);
        }

        private void OnDisable()
        {
            if (unitCard != null)
                unitCard.OnStatsChanged -= OnStatChanged;
        }

        private void OnStatChanged()
        {
            UpdateStatTexts();
        }

        private void UpdateStatTexts()
        {
            if (unitCard == null || unitCard.Stats == null) return;

            var stats = unitCard.Stats;
            healthText.text = stats.maxHealth.ToString();
            attackText.text = stats.attackDamage.ToString();
        }
    }
}
