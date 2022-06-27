using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Rodems
{
    public class PlayerJumpAttackState : IState
    {
        private Player _player;
        private AttackSettings _attackSettings;
        private PlayerAnimatorEvents _animatorEvents;
        private PlayerAnimator _playerAnimator;

        public PlayerJumpAttackState(Player player, PlayerAnimatorEvents animatorEvents, AttackSettings attackSettings, PlayerAnimator playerAnimator)
        {
            _player = player;
            _attackSettings = attackSettings;
            _animatorEvents = animatorEvents;
            _playerAnimator = playerAnimator;
        }

        public void Tick()
        {

        }

        public void OnEnter()
        {
            _player.setCanAttack(false);
            _playerAnimator.play(PlayerAnimationsType.JumpAttackTrigger);
            _animatorEvents.jumpAttack += attackAction;
        }

        public void OnExit()
        {
            _animatorEvents.jumpAttack -= attackAction;
        }

        private void attackAction()
        {

        }
    }
}
