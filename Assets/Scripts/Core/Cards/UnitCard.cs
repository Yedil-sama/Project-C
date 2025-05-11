using Core.Stats;
using System;
using UnityEngine;

namespace Core.Cards.Units
{
    [CreateAssetMenu(menuName = "SO/Cards/Unit Card", fileName = "Unit Card")]
    public class UnitCard : Card
    {
        [SerializeField] private Unit unitPrefab;
        public Unit UnitPrefab => unitPrefab;

        [SerializeField] private UnitStatsData baseStats;
        private UnitStatsData runtimeStats;

        public UnitStatsData Stats => runtimeStats;

        public event Action OnStatsChanged;

        private void OnEnable()
        {
            runtimeStats = Instantiate(baseStats);
            runtimeStats.OnStatsChanged += NotifyOnStatsChanged;

            NotifyOnStatsChanged();
        }

        private void OnDisable()
        {
            if (runtimeStats != null)
                runtimeStats.OnStatsChanged -= NotifyOnStatsChanged;
        }

        public override void Play(Player owner, Vector3 position)
        {
            Unit unitInstance = Instantiate(unitPrefab, position, Quaternion.identity);
            unitInstance.Initialize(owner);
            unitInstance.Stats.LoadFrom(runtimeStats);
            unitInstance.Play(position);
        }

        private void NotifyOnStatsChanged() => OnStatsChanged?.Invoke();
    }
}
