using System;
using UnityEngine;

namespace Rodems
{
    public class PlayerAnimatorEvents : MonoBehaviour
    {
        public event Action handOnGroundAttack;
        public event Action jumpAttack;
        public event Action hitSwingAttack;
        public event Action hitLowUpAttack;

        public void attackHandOnGround() => handOnGroundAttack?.Invoke();
        public void attackjump() => jumpAttack?.Invoke();
        public void attackSwing() => hitSwingAttack?.Invoke();
        public void attackLow() => hitLowUpAttack?.Invoke();
    }
}
