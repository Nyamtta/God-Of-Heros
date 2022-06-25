using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rodems
{
    public class PlayerAnimatorEvents : MonoBehaviour
    {
        public event Action splashAttack;

        public void attackSplash() => splashAttack?.Invoke();

    }
}
