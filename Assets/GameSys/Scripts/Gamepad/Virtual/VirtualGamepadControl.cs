using UnityEngine;

namespace GameSys.Gamepad
{
    public abstract class VirtualGamepadControl : MonoBehaviour,
        IGamepadControl
    {
        public event GamepadControlChangeEvent GamepadControlChange;

        public virtual void Poll() { }

        public void ControlChanged(AxisType axisType, float before, float after)
        {
            GamepadControlChange?.Invoke(axisType, before, after);
        }
    }
}
