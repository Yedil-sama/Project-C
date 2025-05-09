using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Stats;
using Core.Cards.Units.Passives;

namespace Core.Cards.Units
{
    [RequireComponent(typeof(UnitStats), typeof(Animator))]
    public class Unit : MonoBehaviour, IUnit, IAttackable, IAttacker
    {
        public UnitAnimator unitAnimator;

        [SerializeField] protected Player owner;
        public Player Owner => owner;
        [SerializeField] protected float health;
        public float Health => health;
        public float MaxHealth => stats.GetStat(StatType.MaxHealth);
        public float Armor => stats.GetStat(StatType.Armor);
        public float AttackDamage => stats.GetStat(StatType.AttackDamage);
        public float FirstAttackSpeed => stats.GetStat(StatType.FirstAttackSpeed);
        public float AttackSpeed => stats.GetStat(StatType.AttackSpeed);
        public float AttackRange => stats.GetStat(StatType.AttackRange);
        public float ViewRange => stats.GetStat(StatType.ViewRange);
        public float ChaseRange => stats.GetStat(StatType.ChaseRange);
        public float Speed => stats.GetStat(StatType.Speed);
        public Transform Transform { get; }

        public event Action<IAttackable> OnAttack;
        public event Action<Damage> OnTakeDamage;
        public event Action<Vector3> OnPlay;
        public event Action OnDie;
        public event Action<StatModifier, float> OnApplyStatModifier;
        public event Action<float> OnGetPreMitigationDamage;
        public event Action<float> OnGetPostMitigationDamage;

        protected readonly List<IPassive> passives = new();
        protected Animator animator;
        protected UnitStats stats;

        public void Initialize(Player owner)
        {
            this.owner = owner;
            animator = GetComponent<Animator>();
            stats = GetComponent<UnitStats>();
            unitAnimator = new UnitAnimator(animator);
        }

        public void Awake()
        {
            Initialize(owner);
        }

        private void Start()
        {
            health = MaxHealth;
        }

        public virtual void Attack(IAttackable target)
        {
            unitAnimator.PlayAttack();
            OnAttack?.Invoke(target);

            DealDamage(target, new Damage(AttackDamage, this));
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

            if (health <= 0)
            {
                Die();
            }

            return damage;
        }

        public virtual Damage DealDamage(IAttackable target, Damage damage)
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

            unitAnimator.PlayDie();
            OnDie?.Invoke();
            Destroy(gameObject);
        }

        public virtual void Play(Vector3 position)
        {
            foreach (IPassive passive in passives)
            {
                if (passive is IOnPlayPassive onPlay)
                {
                    onPlay.OnPlay(position);
                }
            }

            OnPlay?.Invoke(position);
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

        public void SetOwner(Player owner) => this.owner = owner;

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

        public void ApplyStatModifier(StatModifier modifier, float duration)
        {
            stats.AddModifier(modifier);
            StartCoroutine(DoApplyStatModifier(modifier, duration));

            OnApplyStatModifier?.Invoke(modifier, duration);
        }

        protected IEnumerator DoApplyStatModifier(StatModifier modifier, float duration)
        {
            yield return new WaitForSeconds(duration);
            stats.RemoveModifier(modifier);
        }

        public bool CanBeSeenBy(IAttacker observer)
        {
            return true;
        }
    }
}
