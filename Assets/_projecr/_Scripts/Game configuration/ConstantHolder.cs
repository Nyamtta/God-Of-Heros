using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rodems
{
    public class ConstantHolder : MonoBehaviour
    {
    }

    public enum PlayerAnimationsType
    {
        Idle,
        JumpAttackTrigger,
        HandOnGrounAttackTrigger,
        HitLowUp,
        SwingAttackTrigger,
        MoveSpeed
    }

    public enum EnemyAnimationsType
    {
        Idle,
        MoveSpeed,
        DeathTrigger,
        HandAttackTrigger
    }

    public enum PlayerAttackType
    {
        NotAttacking,
        JumpAttack,
        HandOnGround,
        LowUp,
        Swing,
        Ultimate
    }

    [Serializable]
    public class AttackSettings
    {
        public AttackAction attackAction;
        public float damage;
        public float radius;
        public float duration;
    }

    [Serializable]
    public class PlayerAttackSettings : AttackSettings
    {
        public PlayerAttackType attackType;
        public PlayerAnimationsType animationsType;
    }
}
