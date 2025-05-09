using UnityEngine;

namespace Core.Cards.Units
{
    public class UnitAnimator
    {
        private readonly Animator animator;

        public UnitAnimator(Animator animator)
        {
            this.animator = animator;
        }

        public void PlayIdle() => animator.SetBool(AnimationParameters.Idle, true);
        public void PlayMove() => animator.SetBool(AnimationParameters.Move, true);
        public void StopMove() => animator.SetBool(AnimationParameters.Move, false);
        public void PlayAttack() => animator.SetTrigger(AnimationParameters.Attack);
        public void PlayDie() => animator.SetTrigger(AnimationParameters.Die);
    }
}
