using UnityEngine;

namespace Core.Cards.Units.BehaviorStrategy
{
    public class ChaseBehavior : IUnitBehavior
    {
        private readonly Transform target;

        public ChaseBehavior(Transform target)
        {
            this.target = target;
        }

        public void OnEnter(UnitBrain brain, Unit unit)
        {
            if (brain.Agent == null || target == null)
            {
                brain.SetBehavior(new IdleBehavior(0.1f));
                return;
            }

            brain.SetTarget(target);
            brain.Agent.isStopped = false;
            brain.Agent.SetDestination(target.position);
        }

        public void Tick(UnitBrain brain, Unit unit)
        {
            if (target == null)
            {
                brain.SetBehavior(new IdleBehavior(0.1f));
                return;
            }

            float distanceToTarget = Vector3.Distance(unit.transform.position, target.position);

            if (distanceToTarget > unit.ChaseRange)
            {
                brain.SetBehavior(new IdleBehavior(0.1f));
                return;
            }

            if (distanceToTarget <= unit.AttackRange)
            {
                brain.SetBehavior(new AttackBehavior(target));
                return;
            }

            if (Vector3.Distance(brain.Agent.destination, target.position) > Mathf.Epsilon)
                brain.Agent.SetDestination(target.position);
        }

        public void OnExit(UnitBrain brain, Unit unit)
        {
            if (brain.Agent != null)
                brain.Agent.isStopped = true;
        }
    }
}
