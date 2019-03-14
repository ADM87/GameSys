using UnityEngine;

namespace GameSys.Gamepad
{
    public class GamepadButton : GamepadControl,
        IGamepadButton
    {
        public AxisType Axis { get; private set; }

        public string AxisName { get; private set; }

        public float Value { get; private set; }

        public GamepadButton(AxisType axis)
        {
            Axis = axis;

            AxisName = axis.ToString();

            Value = 0;
        }

        public override void Poll()
        {
            float oldValue = Value;

            Value = Input.GetAxis(AxisName);

            if (Value != oldValue)
            {
                ControlChanged(Axis, oldValue, Value);
            }
        }
    }
}
