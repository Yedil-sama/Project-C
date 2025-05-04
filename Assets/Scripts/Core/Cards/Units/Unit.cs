using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Cards.Passives;
using Core.Stats;

namespace Core
{
    [RequireComponent(typeof(UnitStats))]
    public class Unit : MonoBehaviour
    {
        [SerializeField] private float health;
        public float Health => health;
        public float MaxHealth => stats.GetStat(StatType.MaxHealth);
        public float Armor => stats.GetStat(StatType.Armor);
        public float AttackDamage => stats.GetStat(StatType.AttackDamage);
        public float AttackSpeed => stats.GetStat(StatType.AttackSpeed);
        public float AttackRange => stats.GetStat(StatType.AttackRange);
        public float Speed => stats.GetStat(StatType.Speed);

        public event Action<Damage> OnTakeDamage;
        public event Action<float> OnGetPreMitigationDamage;
        public event Action<float> OnGetPostMitigationDamage;

        private readonly List<IPassive> passives = new();
        private UnitStats stats;

        public void Awake()
        {
            Initialize();
        }

        public void Initialize()
        {
            stats = GetComponent<UnitStats>();
        }

        private void Start()
        {
            health = MaxHealth;
        }

        public virtual void Attack(Unit target)
        {
            // Wind-up, animation, etc.
            // DealDamage(target, new Damage(AttackDamage, DamageType.Physical, this));
        }

        public virtual Damage TakeDamage(Damage damage)
        {
            if (damage.Amount < 0) return damage;

            float preMitigationDamage = GetPreMitigationDamage(damage);
            float postMitigationDamage = GetPostMitigationDamage(preMitigationDamage, damage.Type);

            damage.Amount = postMitigationDamage;

            if (damage.Amount > 0)
            {
                health -= postMitigationDamage;
                OnTakeDamage?.Invoke(damage);
            }
            else damage.Amount = 0;

            return damage;
        }

        public virtual Damage DealDamage(Unit target, Damage damage)
        {
            if (target == null || damage.Amount <= 0) return default;

            foreach (IPassive passive in passives)
            {
                if (passive is IOnDealDamagePassive onDealDamage)
                {
                    onDealDamage.OnDealDamage(target, ref damage);
                }
            }

            return target.TakeDamage(damage);
        }

        public virtual void Die()
        {
            foreach (IPassive passive in passives)
            {
                if (passive is IOnDiePassive onDie)
                {
                    onDie.OnDie();
                }
            }

            Destroy(gameObject);
        }

        public virtual float HealUp(float heal)
        {
            if (heal < 0) return 0;

            health += heal;
            if (health > MaxHealth)
            {
                (health, heal) = (MaxHealth, MaxHealth + heal - health);
            }

            return heal;
        }

        public virtual void AddPassive(IPassive passive)
        {
            passives.Add(passive);
            passive.Initialize(this);
        }

        public float GetPreMitigationDamage(Damage damage)
        {
            if (damage.Type != DamageType.True)
            {
                foreach (IPassive passive in passives)
                {
                    if (passive is IOnGetPreMitigationDamagePassive pre)
                    {
                        pre.OnGetPreMitigationDamage(ref damage);
                    }
                }
            }

            OnGetPreMitigationDamage?.Invoke(damage.Amount);
            return damage.Amount;
        }

        public float GetPostMitigationDamage(float damageAmount, DamageType damageType)
        {
            float result = damageAmount;

            if (damageType == DamageType.Physical)
            {
                result *= (100f / (100f + Armor));
            }

            if (damageType != DamageType.True)
            {
                foreach (IPassive passive in passives)
                {
                    if (passive is IOnGetPostMitigationDamagePassive post)
                    {
                        post.OnGetPostMitigationDamage(ref result);
                    }
                }
            }

            OnGetPostMitigationDamage?.Invoke(result);
            return result;
        }

        public void SetHealth(float health)
        {
            if (health <= 0) Die();
            if (health > MaxHealth) health = MaxHealth;
            this.health = health;
        }

        public void SetStat(StatType type, float newValue)
        {
            if (type == StatType.MaxHealth && newValue <= 0)
            {
                Die();
                return;
            }

            stats?.SetStat(type, newValue);

            if (type == StatType.MaxHealth)
            {
                float maxHealth = stats.GetStat(StatType.MaxHealth);
                if (health > maxHealth)
                {
                    health = maxHealth;
                }
            }
        }

        public void ApplyTimedStatModifier(StatModifier modifier, float duration)
        {
            stats.AddModifier(modifier);
            StartCoroutine(RemoveAfter(modifier, duration));
        }

        private IEnumerator RemoveAfter(StatModifier mod, float duration)
        {
            yield return new WaitForSeconds(duration);
            stats.RemoveModifier(mod);
        }
    }
}
