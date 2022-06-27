using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

namespace Rodems
{
    public class PlayerMoveState : IState
    {
        private PlayerAnimator _playerAnimator;
        private NavMeshAgent _navMeshAgent;
        private Player _player;
        private Vector3 _previousPosition;
        private bool _oneFrameDelay;

        public PlayerMoveState(PlayerAnimator playerAnimator, NavMeshAgent navMeshAgent, Player player)
        {
            _playerAnimator = playerAnimator;
            _navMeshAgent = navMeshAgent;
            _player = player;
        }

        public void Tick()
        {
            _navMeshAgent.destination = _player.getMovePoint();
            _playerAnimator.play(PlayerAnimationsType.MoveSpeed, getMoveSpeed());

            if(!_navMeshAgent.hasPath && _oneFrameDelay)
                _player.setMoving(false);
        }


        public void OnEnter()
        {
            _previousPosition = _player.getPosition();
            _navMeshAgent.isStopped = false;
            _oneFrameDelay = false;

            DOTween.Sequence()
                .AppendInterval(Time.deltaTime)
                .AppendCallback(() => _oneFrameDelay = true);
        }

        public void OnExit()
        {
            _playerAnimator.resetAllFloadValue();
            stopMove();
        }

        public float getMoveSpeed()
        {
            Vector3 curMove = _player.getPosition() - _previousPosition;
            float curSpeed = curMove.magnitude / Time.deltaTime;
            _previousPosition = _player.getPosition();

            float speedInPercentage = curSpeed / _navMeshAgent.speed;
            return speedInPercentage;
        }

        private void stopMove()
        {
            _navMeshAgent.isStopped = true;
            _navMeshAgent.destination = _player.getPosition();
        }
    }
}
