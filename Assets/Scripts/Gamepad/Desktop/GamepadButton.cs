using UnityEngine;

namespace GameSystems.Gamepad
{
    public class GamepadButton :
        IGamepadControl, IGamepadButton
    {
        public event GamepadControlChangeEvent GamepadControlChange;

        public AxisType Axis { get; private set; }

        public string AxisName { get; private set; }

        public float Value { get; private set; }

        public GamepadButton(AxisType axis)
        {
            Axis = axis;

            AxisName = axis.ToString();

            Value = 0;
        }

        public void ControlChanged(AxisType axisType, float before, float after)
        {
            GamepadControlChange?.Invoke(axisType, before, after);
        }

        public void Poll()
        {
            float oldValue = Value;

            Value = Input.GetAxis(AxisName);

            if (Value != oldValue)
            {
                ControlChanged(Axis, oldValue, Value);
            }
        }

        public void MapControl(GamepadMapping.MappingEntry mapping)
        {
            AxisName = mapping.InputAxis;
        }
    }
}
