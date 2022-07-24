using UnityEngine;

namespace Rodems
{
    public class PlayerAnimator
    {
        private Animator _animator;

        private const string JUMP_ATTACK_NAME = "Jump_Attack";
        private const string HAND_ON_GROUND_ATTACK_NAME = "Attack_Hand_On_Ground";
        private const string SWING_ATTACK_NAME = "Attack_Hit_In_Swing";
        private const string LOW_UP_ATTACK_NAME = "Attack_Hit_From_low_to_Up";

        public PlayerAnimator(Animator animator)
        {
            _animator = animator;
        }

        public void play(PlayerAnimationsType playerAnimationsType, float speed = 0)
        {
            if (speed == 0)
                resetAllFloadValue();

            switch (playerAnimationsType)
            {
                case PlayerAnimationsType.Idle: idle(); break;
                case PlayerAnimationsType.JumpAttackTrigger: JumpAttack(); break;
                case PlayerAnimationsType.HandOnGrounAttackTrigger: HandOnGroundAttack(); break;
                case PlayerAnimationsType.SwingAttackTrigger: swingAttack(); break;
                case PlayerAnimationsType.HitLowUp: LowUpAttack(); break;
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

        private void HandOnGroundAttack()
        {
            _animator.SetTrigger(PlayerAnimationsType.HandOnGrounAttackTrigger.ToString());
        }

        private void swingAttack()
        {
            _animator.SetTrigger(PlayerAnimationsType.SwingAttackTrigger.ToString());
        }

        private void LowUpAttack()
        {
            _animator.SetTrigger(PlayerAnimationsType.HitLowUp.ToString());
        }

        private void JumpAttack()
        {
            _animator.SetTrigger(PlayerAnimationsType.JumpAttackTrigger.ToString());
        }

        public float getAnimationClipTime(PlayerAttackType playerAttackType)
        {
            AnimationClip[] clips = _animator.runtimeAnimatorController.animationClips;
            float time = 0;

            foreach (AnimationClip clip in clips)
            {
                switch (playerAttackType)
                {
                    case PlayerAttackType.JumpAttack: if(clip.name == JUMP_ATTACK_NAME) time = clip.averageDuration; break;
                    case PlayerAttackType.HandOnGround: if (clip.name == HAND_ON_GROUND_ATTACK_NAME) time = clip.averageDuration; break;
                    case PlayerAttackType.LowUp: if (clip.name == LOW_UP_ATTACK_NAME) time = clip.averageDuration; break;
                    case PlayerAttackType.Swing: if (clip.name == SWING_ATTACK_NAME) time = clip.averageDuration; break;
                    case PlayerAttackType.Ultimate: break;
                }
            }
            
            return time;
        }

    }
}
