using System.Collections;
using System.Collections.Generic;
using MyBox;
using UnityEngine;

namespace Rodems
{
    public class Swing : AttackAction
    {
        [SerializeField] private Transform _hitTransform;

        public override void init(float duration, float maxHitDistance, Vector3 position, Quaternion rotate)
        {
            base.init(duration, maxHitDistance, position, rotate);
            transform.position = position;
            transform.rotation = rotate;
            StartCoroutine(startHitAction());
        }

        private IEnumerator startHitAction()
        {
            float time = 0;
            float lerpTime = 0;
            Vector3 endPosition = _hitTransform.position + _hitTransform.forward * _maxHitDistance;

            while (lerpTime <= 1)
            {
                time += Time.deltaTime;
                lerpTime = time / _duretion;

                _hitTransform.position = Vector3.Lerp(_hitTransform.position, endPosition, lerpTime);

                yield return null;
            }

            _triggerHandler.onTriggerEnter -= onTriggerEnter;
            resetSubscriptions();
            Destroy(gameObject);
        }


    }
}
