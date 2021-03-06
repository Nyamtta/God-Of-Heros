using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rodems
{
    public class HitOnGround : AttackAction
    {
        [SerializeField] private SphereCollider _sphereCollider;

        public override void init(float duration, float maxHitDistance, Vector3 position, Quaternion rotate)
        {
            base.init(duration, maxHitDistance, position, rotate);
            transform.position = position;
            StartCoroutine(startHitAction());
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
            resetSubscriptions();
            Destroy(gameObject);
        }
    }
}
