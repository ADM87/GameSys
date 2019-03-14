using UnityEngine;

namespace GameSys.Gamepad
{
    public delegate void GamepadControlChangeEvent(AxisType axisType, float before, float after);

    public enum AxisType
    {
        LeftJoystickX,
        LeftJoystickY,
        RightJoystickX,
        RightJoystickY,

        LeftJoystick, // Not Implemented
        RightJoystick, // Not Implemented

        LeftShoulderTop,
        LeftShoulderBottom,
        RightShoulderTop,
        RightShoulderBottom,

        Button01,
        Button02,
        Button03,
        Button04,

        StartButton, // Not Implemented
        BackButton, // Not Implemented
        HomeButton, // Not Implemented

        DirectionalUp, // Not Implemented
        DirectionalDown, // Not Implemented
        DirectionalLeft, // Not Implemented
        DirectionalRight // Not Implemented
    }

    public interface IGamepadControl
    {
        event GamepadControlChangeEvent GamepadControlChange;

        void Poll();
        void ControlChanged(AxisType axisType, float before, float after);
    }

    public interface IGamepadButton : IGamepadControl
    {
        AxisType Axis { get; }

        string AxisName { get; }

        float Value { get; }
    }

    public interface IGamepadJoystick : IGamepadControl
    {
        AxisType XAxis { get; }
        AxisType YAxis { get; }

        string XAxisName { get; }
        string YAxisName { get; }

        float X { get; }
        float Y { get; }

        float Magnitude { get; }

        Vector2 Delta { get; }
    }
}
