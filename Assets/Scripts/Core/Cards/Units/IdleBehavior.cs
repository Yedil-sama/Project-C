using UnityEngine;

namespace Core
{
    public class IdleBehavior : IUnitBehavior
    {
        private float idleDuration;
        private float elapsedTime;

        public IdleBehavior(float idleDuration = 1f)
        {
            this.idleDuration = idleDuration;
        }

        public void OnEnter(UnitBrain brain, Unit unit)
        {
            elapsedTime = 0f;
            brain.StopMovement();
        }

        public void Tick(UnitBrain brain, Unit unit)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= idleDuration)
            {
                // Optionally switch to another behavior
                // Example: brain.SetBehavior(new ChaseAndAttackBehavior(...));
            }
        }

        public void OnExit(UnitBrain brain, Unit unit)
        {
            // Optional cleanup
        }
    }
}
