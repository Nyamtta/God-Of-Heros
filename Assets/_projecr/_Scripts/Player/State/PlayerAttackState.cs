using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Pool;

namespace Rodems
{
    public class PlayerAttackState : IState
    {
        private Player _player;
        private PlayerAttackSettings _attackSettings;
        private PlayerAnimatorEvents _animatorEvents;
        private PlayerAnimator _playerAnimator;
        private AttackAction _attackAction;
        private List<Enemy> _hitEnemy;
        private float _attckTime;
        private float _timer;

        public PlayerAttackState(Player player, PlayerAnimatorEvents animatorEvents, PlayerAttackSettings attackSettings, PlayerAnimator playerAnimator)
        {
            _player = player;
            _attackSettings = attackSettings;
            _animatorEvents = animatorEvents;
            _playerAnimator = playerAnimator;
            onsubscribeEvents();
        }

        public void Tick()
        {
            _timer += Time.deltaTime;
            if(_timer >= _attckTime)
                _player.attackEnded();
        }

        public void OnEnter()
        {
            _timer = 0;
            _hitEnemy = new List<Enemy>();
            _player.setCanAttack(false);
            _playerAnimator.play(_attackSettings.animationsType);
            _attckTime = _playerAnimator.getAnimationClipTime(_attackSettings.attackType);
        }

        public void OnExit()
        {
            _player.setCanAttack(true);
            _hitEnemy.Clear();
        }

        private void onsubscribeEvents()
        {
            switch (_attackSettings.attackType)
            {
                case PlayerAttackType.JumpAttack: _animatorEvents.jumpAttack += attackAction; break;
                case PlayerAttackType.HandOnGround: _animatorEvents.handOnGroundAttack += attackAction; break;
                case PlayerAttackType.LowUp: _animatorEvents.hitLowUpAttack += attackAction; break;
                case PlayerAttackType.Swing: _animatorEvents.hitSwingAttack += attackAction; break;
                case PlayerAttackType.Ultimate: _animatorEvents.hitLowUpAttack += attackAction; break;
            }
        }

        private void unsubscribeEvents()
        {
            switch (_attackSettings.attackType)
            {
                case PlayerAttackType.JumpAttack: _animatorEvents.jumpAttack -= attackAction; break;
                case PlayerAttackType.HandOnGround: _animatorEvents.handOnGroundAttack -= attackAction; break;
                case PlayerAttackType.LowUp: _animatorEvents.hitLowUpAttack -= attackAction; break;
                case PlayerAttackType.Swing: _animatorEvents.hitLowUpAttack -= attackAction; break;
                case PlayerAttackType.Ultimate: _animatorEvents.hitLowUpAttack -= attackAction; break;
            }
        }

        private void attackAction()
        {
            _attackAction = _player.instantiateAttackAction(_attackSettings.attackAction);
            _attackAction.init(_attackSettings.duration, _attackSettings.radius, _player.getPosition(), _player.getRotation());
            _attackAction.attackObject += attackEnemy;
        }

        private void attackEnemy(GameObject obj)
        {
            if (obj.TryGetComponent(out Enemy enemy) && !_hitEnemy.Contains(enemy))
            {
                _hitEnemy.Add(enemy);
                enemy.takeDamege(_attackSettings.damage);
            }
        }

    }
}
