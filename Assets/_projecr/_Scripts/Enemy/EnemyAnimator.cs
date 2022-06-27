using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rodems
{
    public class EnemyAnimator : MonoBehaviour
    {
        private Animator _animator;

        public EnemyAnimator(Animator animator)
        {
            _animator = animator;
        }

        public void play(EnemyAnimationsType enemyAnimationsType, float speed = 0)
        {
            if(speed == 0)
                resetAllFloadValue();

            switch (enemyAnimationsType)
            {
                case EnemyAnimationsType.Idle: idle(); break;
                case EnemyAnimationsType.DeathTrigger: deathAttack(); break;
                case EnemyAnimationsType.HandAttackTrigger: handAttack(); break;
                case EnemyAnimationsType.MoveSpeed: move(speed); break;
            }
        }

        public void resetAllFloadValue()
        {
            _animator.SetFloat(EnemyAnimationsType.MoveSpeed.ToString(), 0);
        }

        private void idle()
        {
            _animator.SetFloat(EnemyAnimationsType.MoveSpeed.ToString(), 0);
        }

        private void move(float speed)
        {
            _animator.SetFloat(EnemyAnimationsType.MoveSpeed.ToString(), speed);
        }

        private void handAttack()
        {
            _animator.SetTrigger(EnemyAnimationsType.HandAttackTrigger.ToString());
        }

        private void deathAttack()
        {
            _animator.SetTrigger(EnemyAnimationsType.DeathTrigger.ToString());
        }
    }
}
