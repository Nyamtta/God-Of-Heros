using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rodems
{
    public class TriggerHandler : MonoBehaviour
    {
        public event Action<GameObject> onTriggerEnter;
        public event Action<GameObject> onTriggerExit;

        private void OnTriggerEnter(Collider other)
        {
            if (other.attachedRigidbody != null)
                onTriggerEnter?.Invoke(other.attachedRigidbody.gameObject);
            else
                onTriggerEnter?.Invoke(other.gameObject);
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.attachedRigidbody != null)
                onTriggerExit?.Invoke(other.attachedRigidbody.gameObject);
            else
                onTriggerExit?.Invoke(other.gameObject);
        }
    }
}
