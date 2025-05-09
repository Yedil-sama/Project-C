using UnityEngine;
using UnityEngine.AI;

namespace Core.Cards.Units.BehaviorStrategy
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class UnitBrain : MonoBehaviour
    {
        [SerializeField] private Transform target;
        public Transform Target => target;

        private NavMeshAgent agent;
        public NavMeshAgent Agent => agent;

        private Unit unit;
        private IUnitBehavior currentBehavior;

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            unit = GetComponent<Unit>();
        }

        private void Start()
        {
            if (unit != null)
            {
                agent.speed = unit.Speed;
                agent.stoppingDistance = unit.AttackRange;
            }

            SetBehavior(new IdleBehavior());
        }

        private void Update()
        {
            currentBehavior?.Tick(this, unit);
        }

        public void SetTarget(Transform newTarget)
        {
            target = newTarget;

            if (target != null)
            {
                agent.isStopped = false;
                agent.SetDestination(target.position);
            }
            else
            {
                StopMovement();
            }
        }

        public void SetBehavior(IUnitBehavior newBehavior)
        {
            if (newBehavior == currentBehavior) return;

            currentBehavior?.OnExit(this, unit);
            currentBehavior = newBehavior;
            currentBehavior?.OnEnter(this, unit);
        }

        public void StopMovement()
        {
            if (agent != null)
            {
                unit.unitAnimator.StopMove();
                agent.isStopped = true;
                agent.ResetPath();
            }
        }
    }
}
