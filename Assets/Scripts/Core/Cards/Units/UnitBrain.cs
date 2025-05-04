using UnityEngine;
using UnityEngine.AI;

namespace Core
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
            //if (unit != null)
            //{
            //    agent.speed = unit.Speed;
            //}

            //if (target != null)
            //{
            //    SetBehavior(new ChaseAndAttackBehavior(target, unit.AttackSpeed, unit.AttackRange));
            //}
            //else
            //{
            //    SetBehavior(new IdleBehavior());
            //}
        }


        private void Update()
        {
            agent.SetDestination(target.position);

           // currentBehavior?.Tick(this, unit);
        }

        public void SetTarget(Transform newTarget)
        {
            target = newTarget;
            SetBehavior(newTarget != null ? new ChaseAndAttackBehavior(newTarget, unit.AttackSpeed, unit.AttackRange) : new IdleBehavior());
        }

        public void SetBehavior(IUnitBehavior newBehavior)
        {
            currentBehavior?.OnExit(this, unit);
            currentBehavior = newBehavior;
            currentBehavior?.OnEnter(this, unit);
        }

        public void StopMovement()
        {
            if (agent != null) agent.isStopped = true;
        }
    }
}
