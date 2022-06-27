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
        SplashAttackTrigger,
        MoveSpeed
    }

    public enum EnemyAnimationsType
    {
        Idle,
        MoveSpeed,
        DeathTrigger,
        HandAttackTrigger
    }

    public enum AttackDirectionType
    {
        RadiusAttack,
        CircledAttacl
    }

    public enum PlayerAttackType
    {
        NotAttacking,
        JumpAttack,
        swingAttack
    }

    [Serializable]
    public class AttackSettings
    {
        public AttackDirectionType directionType;
        public float damage;
        public float radius;
        public float duration;
    }

    [Serializable]
    public class PlayerAttackSettings : AttackSettings
    {
        public PlayerAttackType attackType;
    }
}
