using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

namespace Rodems
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private PlayerSettings _settings;
        [SerializeField] private Animator _animator;
        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private PlayerAnimatorEvents _animatorEvents;
        [SerializeField] private TriggerHandler _triggerHandler;

        private bool _canAttac;
        private bool _isMoving;
        private PlayerAttackType _attackType;
        private Vector3 _movePoint;
        private PlayerAnimator _animatorContriller;
        private StateMachine _stateMachine;

        private void OnValidate()
        {
            _navMeshAgent = _navMeshAgent == null ? GetComponent<NavMeshAgent>() : _navMeshAgent;
            _triggerHandler = _triggerHandler == null ? GetComponent<TriggerHandler>() : _triggerHandler;
        }

        private void Start()
        {
            _stateMachine = new StateMachine();
            _animatorContriller = new PlayerAnimator(_animator);

            PlayerMoveState moveState = new PlayerMoveState(_animatorContriller, _navMeshAgent, this);
            PlayerIdleState idleState = new PlayerIdleState(this, Camera.main, _settings);
            PlayerJumpAttackState jumpAttackState = new PlayerJumpAttackState(
                 this, _animatorEvents, _settings.getAttackSettings(PlayerAttackType.JumpAttack), _animatorContriller);
            PlayerSwingAttackState swingAttackState = new PlayerSwingAttackState();

            _stateMachine.AddTransition(idleState, moveState, () => _isMoving);
            _stateMachine.AddTransition(moveState, idleState, () => !_isMoving);
            _stateMachine.AddAnyTransition(swingAttackState, () => PlayerAttackType.swingAttack == CheckAttackInput());
            _stateMachine.AddAnyTransition(jumpAttackState, () => PlayerAttackType.JumpAttack == CheckAttackInput());

            _stateMachine.SetState(idleState);
        }

        private void Update() => _stateMachine.Tick();

        public void setMovePoint(Vector3 movePoint)
        {
            _movePoint = movePoint;
            _isMoving = true;
        }

        public void setMoving(bool isMoving) => _isMoving = isMoving;

        public PlayerAttackType CheckAttackInput()
        {
            _attackType = PlayerAttackType.NotAttacking;

            if (Input.GetKeyDown(KeyCode.Space) && _canAttac)
                _attackType = PlayerAttackType.JumpAttack;
            else if(Input.GetKeyDown(KeyCode.Q) && _canAttac)
                _attackType = PlayerAttackType.swingAttack;

            return _attackType;
        }

        public void setCanAttack(bool canAttack) => _canAttac = canAttack;

        public Vector3 getPosition() => transform.position;

        public Vector3 getMovePoint() => _movePoint;
    }
}
