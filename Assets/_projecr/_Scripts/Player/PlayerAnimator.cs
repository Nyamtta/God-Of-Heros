using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace Rodems
{
    public class PlayerAnimator
    {
        private Animator _animator;

        public PlayerAnimator(Animator animator)
        {
            _animator = animator;
        }

        public void play(PlayerAnimationsType playerAnimationsType, float speed = 0)
        {
            resetAllFloadValue();

            switch (playerAnimationsType)
            {
                case PlayerAnimationsType.Idle: idle(); break;
                case PlayerAnimationsType.JumpAttackTrigger: JumpAttack(); break;
                case PlayerAnimationsType.SplashAttackTrigger: SplashAttack(); break;
                case PlayerAnimationsType.MoveSpeed: move(speed); break;
            }
        }

        public void resetAllFloadValue()
        {
            _animator.SetFloat(PlayerAnimationsType.MoveSpeed.ToString(), 0);
        }

        private void idle()
        {
            _animator.SetFloat(PlayerAnimationsType.MoveSpeed.ToString(), 0);
        }

        private void move(float speed)
        {
            _animator.SetFloat(PlayerAnimationsType.MoveSpeed.ToString(), speed);
        }

        private void SplashAttack()
        {
            _animator.SetTrigger(PlayerAnimationsType.SplashAttackTrigger.ToString());
        }

        private void JumpAttack()
        {
            _animator.SetTrigger(PlayerAnimationsType.JumpAttackTrigger.ToString());
        }

    }
}
