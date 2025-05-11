using UnityEngine;
using System;

namespace Core.Stats
{
    [CreateAssetMenu(menuName = "SO/Stats/Unit Stats Data", fileName = "UnitStatsData")]
    public class UnitStatsData : ScriptableObject
    {
        public event Action OnStatsChanged;

        [SerializeField] public float maxHealth;
        [SerializeField] public float armor;
        [SerializeField] public float attackDamage;
        [SerializeField] public float firstAttackSpeed;
        [SerializeField] public float attackSpeed;
        [SerializeField] public float attackRange;
        [SerializeField] public float viewRange;
        [SerializeField] public float chaseRange;
        [SerializeField] public float speed;

        public void SetStat(StatType type, float value)
        {
            switch (type)
            {
                case StatType.MaxHealth: maxHealth = value; break;
                case StatType.Armor: armor = value; break;
                case StatType.AttackDamage: attackDamage = value; break;
                case StatType.FirstAttackSpeed: firstAttackSpeed = value; break;
                case StatType.AttackSpeed: attackSpeed = value; break;
                case StatType.AttackRange: attackRange = value; break;
                case StatType.ViewRange: viewRange = value; break;
                case StatType.ChaseRange: chaseRange = value; break;
                case StatType.Speed: speed = value; break;
            }

            OnStatsChanged?.Invoke();
        }
    }
}
