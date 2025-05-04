using UnityEngine;

namespace Core
{
    public class ChaseAndAttackBehavior : IUnitBehavior
    {
        private Transform target;
        private float attackRange;
        private float attackSpeed;
        private float lastAttackTime;

        public ChaseAndAttackBehavior(Transform target, float attackSpeed, float attackRange)
        {
            this.target = target;
            this.attackSpeed = attackSpeed;
            this.attackRange = attackRange;
        }

        public void OnEnter(UnitBrain brain, Unit unit)
        {
            if (brain.Agent != null && target != null)
            {
                brain.Agent.isStopped = false;
                brain.Agent.SetDestination(target.position);
            }
        }

        public void Tick(UnitBrain brain, Unit unit)
        {
            if (target == null || !target.gameObject.activeInHierarchy)
            {
                brain.SetBehavior(new IdleBehavior());
                return;
            }

            var agent = brain.Agent;
            float distance = Vector3.Distance(unit.transform.position, target.position);

            if (distance <= attackRange)
            {
                if (!agent.isStopped)
                {
                    agent.isStopped = true;
                }

                if (Time.time - lastAttackTime >= attackSpeed)
                {
                    if (target.TryGetComponent<Unit>(out var enemy))
                    {
                        unit.DealDamage(enemy, new Damage(unit.AttackDamage, DamageType.Physical, unit));
                        lastAttackTime = Time.time;
                    }
                }
            }
            else
            {
                if (agent.isStopped)
                {
                    agent.isStopped = false;
                }

                if (Vector3.Distance(agent.destination, target.position) > 0.25f)
                {
                    agent.SetDestination(target.position);
                }
            }
        }

        public void OnExit(UnitBrain brain, Unit unit)
        {
            if (brain.Agent != null)
            {
                brain.Agent.isStopped = true;
            }
        }
    }
}
