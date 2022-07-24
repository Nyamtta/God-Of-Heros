using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace Rodems
{
    [CreateAssetMenu(fileName = "Player Settings", menuName = "Settings/Player Settings")]
    public class PlayerSettings : ScriptableObject
    {
        [SerializeField] private float _hitPoints;
        [SerializeField] private LayerMask _groundLayer;
        [SerializeField] private List<PlayerAttackSettings> _attackSettings;

        public float hitPoints { get => _hitPoints; private set => _hitPoints = value; }
        public LayerMask groundLayer { get => _groundLayer; private set => _groundLayer = value; }
        public List<PlayerAttackSettings> attackSettings { get => _attackSettings; set => _attackSettings = value; }

        public PlayerAttackSettings getAttackSettings(PlayerAttackType attackType)
        {
            foreach (var attack in _attackSettings)
            {
                if(attack.attackType == attackType)
                    return attack;
            }

            return null;
        }
    }

}
