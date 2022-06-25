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
        [SerializeField] private SphereCollider _attackSphereCollider;
        [SerializeField] private PlayerAnimatorEvents _animatorEvents;
        [SerializeField] private TriggerHandler _triggerHandler;

        private PlayerAnimator _animatorContriller;
        private Camera _mainCamera;
        private bool _canMove = true;
        private bool _canAttack = true;

        private void OnValidate()
        {
            _navMeshAgent = _navMeshAgent == null ? GetComponent<NavMeshAgent>() : _navMeshAgent;
            _triggerHandler = _triggerHandler == null ? GetComponent<TriggerHandler>() : _triggerHandler;
        }

        private void Start()
        {
            _animatorContriller = new PlayerAnimator(_animator);
            _mainCamera = Camera.main;
            _triggerHandler.onTriggerEnter += onTriggerEnter;
            _animatorEvents.splashAttack += simpleAttack;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
                moveToTarget();

            if (Input.GetKeyDown(KeyCode.Space) && _canAttack)
                splashAttackAction();
        }

        private void moveToTarget()
        {
            if(!_canMove)
                return;

            _animatorContriller.play(PlayerAnimationsType.MoveSpeed, 1f);
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit, 1000, _settings.groundLayer))
                StartCoroutine(moveRoutine(hit.point));
        }

        private void splashAttackAction()
        {
            _navMeshAgent.isStopped = true;
            _navMeshAgent.destination = transform.position;
            _canAttack = false;
            _canMove = false;
            _animatorContriller.play(PlayerAnimationsType.SplashAttackTrigger);
        }

        private void simpleAttack()
        {
            AttackSettings settings = _settings.getAttackSettings(PlayerAttackType.SplashAttack);
            StartCoroutine(attackAction(settings.duration, settings.radius));
        }

        private void onTriggerEnter(GameObject obj)
        {
            if(obj.TryGetComponent(out Enemy enemy))
                enemy.takeDamege(1f);
        }

        private IEnumerator attackAction(float duretion, float radius)
        {
            float lertTime = 0;
            float timer = 0;

            while (lertTime < 1f)
            {
                timer += Time.deltaTime;
                lertTime = timer / duretion;
                _attackSphereCollider.radius = Mathf.Lerp(0, radius, lertTime);

                yield return null;
            }

            _navMeshAgent.isStopped = false;
            _attackSphereCollider.radius = 0f;
            _canMove = true;
            _canAttack = true;
        }

        private IEnumerator moveRoutine(Vector3 targetPoint)
{
            _navMeshAgent.SetDestination(targetPoint);
            yield return null;

            Vector3 previousPosition = transform.position;
            Vector3 curMove;
            float curSpeed;
            float speedInPercentage;

            while (_navMeshAgent.hasPath)
            {
                curMove = transform.position - previousPosition;
                curSpeed = curMove.magnitude / Time.deltaTime;
                previousPosition = transform.position;
                speedInPercentage = curSpeed / _navMeshAgent.speed;

                _animatorContriller.play(PlayerAnimationsType.MoveSpeed, speedInPercentage);
                yield return null;
            }
            
            _animatorContriller.play(PlayerAnimationsType.Idle);
        }
    }
}
