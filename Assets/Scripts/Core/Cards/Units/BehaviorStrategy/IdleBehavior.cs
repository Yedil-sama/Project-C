using UnityEngine;

namespace Core.Cards.Units.BehaviorStrategy
{
    public class IdleBehavior : IUnitBehavior
    {
        private readonly float waitDuration;
        private float elapsedTime;

        public IdleBehavior(float waitDuration = 1f)
        {
            this.waitDuration = waitDuration;
        }

        public void OnEnter(UnitBrain brain, Unit unit)
        {
            elapsedTime = 0f;
            brain.StopMovement();
        }

        public void Tick(UnitBrain brain, Unit unit)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime < waitDuration)
                return;

            Collider[] hits = Physics.OverlapSphere(unit.transform.position, unit.ViewRange);

            foreach (Collider hit in hits)
            {
                if (hit.transform != unit.transform && hit.TryGetComponent<Unit>(out var enemy) && enemy.Owner != unit.Owner && enemy.CanBeSeenBy(unit))
                {
                    brain.SetBehavior(new ChaseBehavior(enemy.transform));
                    return;
                }
            }
        }

        public void OnExit(UnitBrain brain, Unit unit) { }
    }
}
