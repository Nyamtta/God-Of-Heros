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
        [SerializeField] private MeshRenderer _meshRenderer;
        [SerializeField] private NavMeshAgent _navMeshAgent;

        Color _color;

        private void Start()
        {
            _color = _meshRenderer.material.color;
        }

        public void takeDamege(float damage)
        {
            _meshRenderer.material.color = Color.red;
            _hitPoints -= damage;

            DOTween.Sequence()
                .AppendInterval(0.2f)
                .AppendCallback(() => {
                    _meshRenderer.material.color = _color;
                });

            if(_hitPoints <= 0)
            {
                Destroy(gameObject);
            }
        }

    }
}
