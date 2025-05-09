using System.Collections.Generic;
using UnityEngine;

namespace Core.Stats
{
    public class UnitStats : MonoBehaviour
    {
        [SerializeField] private float maxHealth;
        [SerializeField] private float armor;
        [SerializeField] private float attackDamage;
        [SerializeField] private float firstAttackSpeed;
        [SerializeField] private float attackSpeed;
        [SerializeField] private float attackRange;
        [SerializeField] private float viewRange;
        [SerializeField] private float chaseRange;
        [SerializeField] private float speed;

        private readonly List<StatModifier> modifiers = new();

        public float GetStat(StatType type)
        {
            float baseValue = type switch
            {
                StatType.MaxHealth => maxHealth,
                StatType.Armor => armor,
                StatType.AttackDamage => attackDamage,
                StatType.FirstAttackSpeed => firstAttackSpeed,
                StatType.AttackSpeed => attackSpeed,
                StatType.AttackRange => attackRange,
                StatType.ViewRange => viewRange,
                StatType.ChaseRange => chaseRange,
                StatType.Speed => speed,
                _ => 0
            };

            float result = baseValue;
            foreach (var mod in modifiers)
            {
                if (mod.StatType != type) continue;

                result += mod.ModifierType == ModifierType.Flat ? mod.Amount : baseValue * mod.Amount;
            }

            return Mathf.Max(0, result);
        }

        public void SetStat(StatType type, float value)
        {
            switch (type)
            {
                case StatType.MaxHealth:
                    maxHealth = value;
                    break;
                case StatType.Armor:
                    armor = value;
                    break;
                case StatType.AttackDamage:
                    attackDamage = value;
                    break;
                case StatType.FirstAttackSpeed:
                    firstAttackSpeed = value;
                    break;
                case StatType.AttackSpeed:
                    attackSpeed = value;
                    break;
                case StatType.AttackRange:
                    attackRange = value;
                    break;
                case StatType.ViewRange:
                    viewRange = value;
                    break;
                case StatType.ChaseRange:
                    chaseRange = value;
                    break;
                case StatType.Speed:
                    speed = value;
                    break;
            }
        }

        public void AddModifier(StatModifier modifier) => modifiers.Add(modifier);
        public void RemoveModifier(StatModifier modifier) => modifiers.Remove(modifier);
    }
}
