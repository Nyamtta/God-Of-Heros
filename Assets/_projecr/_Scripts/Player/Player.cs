using UnityEngine;
using UnityEngine.AI;
using static UnityEditor.Experimental.GraphView.GraphView;

namespace Rodems
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private PlayerSettings _settings;
        [SerializeField] private Animator _animator;
        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private PlayerAnimatorEvents _animatorEvents;
        [SerializeField] private TriggerHandler _triggerHandler;

        private bool _canAttac = true;
        private bool _isMoving = false;
        private bool _isAttacking = false;
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
            PlayerAttackState jumpAttackState = new PlayerAttackState(this, _animatorEvents, _settings.getAttackSettings(PlayerAttackType.JumpAttack), _animatorContriller);
            PlayerAttackState handOnGroundAttackState = new PlayerAttackState(this, _animatorEvents, _settings.getAttackSettings(PlayerAttackType.HandOnGround), _animatorContriller);
            PlayerAttackState swingAttackState = new PlayerAttackState(this, _animatorEvents, _settings.getAttackSettings(PlayerAttackType.Swing), _animatorContriller);
            PlayerAttackState LowUpAttackState = new PlayerAttackState(this, _animatorEvents, _settings.getAttackSettings(PlayerAttackType.LowUp), _animatorContriller);

            _stateMachine.AddTransition(idleState, moveState, () => _isMoving);
            _stateMachine.AddTransition(moveState, idleState, () => !_isMoving);
            _stateMachine.AddAnyTransition(handOnGroundAttackState, () => PlayerAttackType.HandOnGround == CheckAttackInput());
            _stateMachine.AddAnyTransition(jumpAttackState, () => PlayerAttackType.JumpAttack == CheckAttackInput());
            _stateMachine.AddAnyTransition(swingAttackState, () => PlayerAttackType.Swing == CheckAttackInput());
            _stateMachine.AddAnyTransition(LowUpAttackState, () => PlayerAttackType.LowUp == CheckAttackInput());
            _stateMachine.AddTransition(handOnGroundAttackState, idleState, () => !_isAttacking);
            _stateMachine.AddTransition(jumpAttackState, idleState, () => !_isAttacking);
            _stateMachine.AddTransition(swingAttackState, idleState, () => !_isAttacking);
            _stateMachine.AddTransition(LowUpAttackState, idleState, () => !_isAttacking);

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

            if (_canAttac && Input.GetKeyDown(KeyCode.Space))
            {
                _isAttacking = true;
                _attackType = PlayerAttackType.JumpAttack;
            }
            else if(_canAttac && Input.GetKeyDown(KeyCode.Q))
            {
                _isAttacking = false;
                _attackType = PlayerAttackType.HandOnGround;
            }
            else if (_canAttac && Input.GetKeyDown(KeyCode.W))
            {
                _isAttacking = false;
                _attackType = PlayerAttackType.Swing;
            }
            else if (_canAttac && Input.GetKeyDown(KeyCode.E))
            {
                _isAttacking = false;
                _attackType = PlayerAttackType.LowUp;
            }

            return _attackType;
        }

        public AttackAction instantiateAttackAction(AttackAction attackAction) => Instantiate(attackAction);

        public void setCanAttack(bool canAttack) => _canAttac = canAttack;

        public Vector3 getPosition() => transform.position;

        public Vector3 getMovePoint() => _movePoint;

        public Quaternion getRotation() => transform.rotation;

        public void attackEnded() => _isAttacking = false;

        

    }
}
