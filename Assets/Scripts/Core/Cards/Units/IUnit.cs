using UnityEngine;
using Core.Stats;
using Core.Cards.Units.Passives;

namespace Core.Cards.Units
{
    public interface IUnit : IAttackable, IAttacker
    {
        Player Owner { get; }
        float Health { get; }
        float MaxHealth { get; }
        float Armor { get; }
        float AttackDamage { get; }
        float AttackSpeed { get; }
        float FirstAttackSpeed { get; }
        float AttackRange { get; }
        float ViewRange { get; }
        float ChaseRange { get; }
        float Speed { get; }
        Transform Transform { get; }

        void Initialize(Player owner);
        void Play(Vector3 position);
        void SetOwner(Player owner);
        void SetStat(StatType type, float newValue);
        void ApplyStatModifier(StatModifier modifier, float duration);
        float HealUp(float heal);
        void AddPassive(IPassive passive);
        void SetHealth(float health);
    }
}
