using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

namespace Rodems
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private float _hitPoints;
        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private Player _player;
        [SerializeField] private TriggerHandler _triggerHandler;
        [SerializeField] private Animator _animator;

        private EnemyAnimator _enemyAnimator;
        private bool _canMove = true;
        private Vector3 _previousPosition;

        private void Start()
        {
            _previousPosition = transform.position;
            _enemyAnimator = new EnemyAnimator(_animator);
            _triggerHandler.onTriggerEnter += onTriggerEnter;
            _triggerHandler.onTriggerExit += onTriggerExit;
        }

        private void Update()
        {
            if (_canMove)
            {
                move();
            }
        }

        public void move()
        {
            float moveSpeed = getMoveSpeed();
            _enemyAnimator.play(EnemyAnimationsType.MoveSpeed, moveSpeed);
            _navMeshAgent.destination = _player.transform.position;
        }

        public float getMoveSpeed()
        {
            Vector3 curMove = transform.position - _previousPosition;
            float curSpeed = curMove.magnitude / Time.deltaTime;
            _previousPosition = transform.position;
            
            float speedInPercentage = curSpeed / _navMeshAgent.speed;
            return speedInPercentage;
        }

        public void takeDamege(float damage)
        {
            _hitPoints -= damage;

            if(_hitPoints <= 0)
            {
                _enemyAnimator.play(EnemyAnimationsType.DeathTrigger);
                Destroy(this);
            }
        }

        public void startAttack()
        {
            stopMove();
            _canMove = false;
            _enemyAnimator.play(EnemyAnimationsType.HandAttackTrigger);
        }

        private void onTriggerEnter(GameObject obj)
        {
            if(obj.TryGetComponent(out Player player))
            {
                startAttack();
            }
        }

        private void onTriggerExit(GameObject obj)
        {
            if (obj.TryGetComponent(out Player player))
            {
                _navMeshAgent.isStopped = false;
                _canMove = true;
            }
        }

        private void stopMove()
        {
            _navMeshAgent.isStopped = true;
            _navMeshAgent.destination = transform.position;
        }
    }
}
