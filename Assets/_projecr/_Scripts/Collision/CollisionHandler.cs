using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rodems
{
    public class CollisionHandler : MonoBehaviour
    {
        public event Action<GameObject> onCollisionEnter;
        public event Action<GameObject> onCollisionExit;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.body != null)
                onCollisionEnter?.Invoke(collision.body.gameObject);
            else
                onCollisionEnter?.Invoke(collision.gameObject);
        }

        private void OnCollisionExit(Collision collision)
        {
            if (collision.body != null)
                onCollisionExit?.Invoke(collision.body.gameObject);
            else
                onCollisionExit?.Invoke(collision.gameObject);
        }
    }
}
