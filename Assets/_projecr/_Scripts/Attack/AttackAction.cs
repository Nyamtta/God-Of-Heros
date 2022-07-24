using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rodems
{
    public abstract class AttackAction : MonoBehaviour
    {
        [SerializeField] protected TriggerHandler _triggerHandler;

        public event Action<GameObject> attackObject;
        protected float _duretion;
        protected float _maxHitDistance;

        public virtual void init(float duration, float maxHitDistance, Vector3 position, Quaternion rotate)
        {
            _duretion = duration;
            _maxHitDistance = maxHitDistance;
            _triggerHandler.onTriggerEnter += onTriggerEnter;
        }

        protected virtual void onTriggerEnter(GameObject obj)
        {
            attackObject?.Invoke(obj);
        }

        protected void resetSubscriptions() => attackObject = null;
    }
}
