using UnityEngine;

namespace Core.Cards.Units.BehaviorStrategy
{
    public class AttackBehavior : IUnitBehavior
    {
        private readonly Transform target;
        private float lastAttackTime;
        private bool isFirstAttack;

        public AttackBehavior(Transform target)
        {
            this.target = target;
            isFirstAttack = false;
        }

        public void OnEnter(UnitBrain brain, Unit unit)
        {
            brain.StopMovement();
            isFirstAttack = false;
            lastAttackTime = Time.time;
        }

        public void Tick(UnitBrain brain, Unit unit)
        {
            if (target == null)
            {
                brain.SetBehavior(new IdleBehavior());
                return;
            }

            float distance = Vector3.Distance(unit.transform.position, target.position);

            if (distance > unit.AttackRange)
            {
                brain.SetBehavior(new ChaseBehavior(target));
                return;
            }

            float currentCooldown = isFirstAttack ? unit.AttackSpeed : unit.FirstAttackSpeed;

            if (Time.time - lastAttackTime >= currentCooldown)
            {
                if (target.TryGetComponent<Unit>(out var enemy) && enemy.Owner != unit.Owner)
                {
                    unit.Attack(enemy);
                    lastAttackTime = Time.time;
                    isFirstAttack = true;
                }
            }
        }

        public void OnExit(UnitBrain brain, Unit unit)
        {
            isFirstAttack = false;
        }
    }
}
