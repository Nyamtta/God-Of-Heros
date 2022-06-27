using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rodems
{
    public class HitOnGround : MonoBehaviour
    {
        [SerializeField] private float _duretion;
        [SerializeField] private float _maxHitDistance;
        [SerializeField] private SphereCollider _sphereCollider;
        [SerializeField] private TriggerHandler _triggerHandler;

        public event Action<GameObject> attackObject;

        public void init()
        {
            _triggerHandler.onTriggerEnter += onTriggerEnter;
            StartCoroutine(startHitAction());
        }

        private void onTriggerEnter(GameObject obj)
        {
            attackObject?.Invoke(obj);
        }

        private IEnumerator startHitAction()
        {
            float time = 0;
            float lerpTime = 0;

            while (lerpTime <= 1)
            {
                time += Time.deltaTime;
                lerpTime = time / _duretion;

                _sphereCollider.radius = Mathf.Lerp(0, _maxHitDistance, lerpTime);

                yield return null;
            }

            _triggerHandler.onTriggerEnter -= onTriggerEnter;
            Destroy(gameObject);
        }

    }
}
