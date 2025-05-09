using System.Collections.Generic;
using UnityEngine;
using Core.Stats;
using Core.Cards.Units.Races;
using Core.Cards.Units.Passives;

namespace Core.Cards.Units
{
    [CreateAssetMenu(menuName = "SO/Units/Unit", fileName = "UnitData")]
    public class UnitData : ScriptableObject
    {
        public GameObject prefab;
        public IRace race;
        public List<IPassive> passives;

        [Header("Base Stats")]
        public float maxHealth = 100f;
        public float attackDamage = 10f;
        public float armor = 0;
        public float firstAttackSpeed = 1f;
        public float attackSpeed = 1f;
        public float attackRange = 2f;
        public float viewRange = 10f;
        public float chaseRange = 10f;
        public float speed = 2.5f;

        public List<StatModifier> additionalModifiers;

        public void ApplyStatsTo(Unit unit)
        {
            unit.SetStat(StatType.MaxHealth, maxHealth);
            unit.SetStat(StatType.AttackDamage, attackDamage);
            unit.SetStat(StatType.Armor, armor);
            unit.SetStat(StatType.FirstAttackSpeed, firstAttackSpeed);
            unit.SetStat(StatType.AttackSpeed, attackSpeed);
            unit.SetStat(StatType.AttackRange, attackRange);
            unit.SetStat(StatType.ViewRange, viewRange);
            unit.SetStat(StatType.ChaseRange, chaseRange);
            unit.SetStat(StatType.Speed, speed);

            foreach (StatModifier mod in additionalModifiers)
                unit.ApplyStatModifier(mod, float.MaxValue);
        }

        public void ApplyPassivesTo(Unit unit)
        {
            foreach (IPassive passive in passives)
            {
                passive.Initialize(unit);
                unit.AddPassive(passive);
            }
        }
    }
}
