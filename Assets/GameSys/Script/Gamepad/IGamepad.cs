using System.Collections.Generic;

namespace GameSystems.Gamepad
{
    public interface IGamepad
    {
        IGamepadJoystick LeftJoystick { get; }

        IGamepadButton Button01 { get; }
        IGamepadButton Button02 { get; }
        IGamepadButton Button03 { get; }
        IGamepadButton Button04 { get; }

        Dictionary<AxisType, IGamepadControl> ControlMap { get; }

        void PollInput();

        void AddControlListener(AxisType axisType, GamepadControlChangeEvent listener);
        void RemoveControlListener(AxisType axisType, GamepadControlChangeEvent listener);
    }
}
