using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rodems
{
    public class PlayerAnimatorEvents : MonoBehaviour
    {
        public event Action splashAttack;
        public event Action jumpAttack;
        public event Action hitSwingAttack;
        public event Action hitLowUpAttack;

        public void attackSplash() => splashAttack?.Invoke();
        public void attackjump() => jumpAttack?.Invoke();
        public void attackSwing() => hitSwingAttack?.Invoke();
        public void attackLow() => hitLowUpAttack?.Invoke();
    }
}
