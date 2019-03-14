using System.Collections.Generic;

namespace GameSys.Gamepad
{
    public class Gamepad : 
        IGamepad
    {
        public IGamepadJoystick LeftJoystick { get; }
        public IGamepadJoystick RightJoystick { get; }

        public IGamepadButton Button01 { get; }
        public IGamepadButton Button02 { get; }
        public IGamepadButton Button03 { get; }
        public IGamepadButton Button04 { get; }

        public Dictionary<AxisType, IGamepadControl> ControlMap { get; private set; }

        public Gamepad()
        {
            LeftJoystick = new GamepadJoystick(AxisType.LeftJoystickX, AxisType.LeftJoystickY);
            RightJoystick = new GamepadJoystick(AxisType.RightJoystickX, AxisType.RightJoystickY);

            Button01 = new GamepadButton(AxisType.Button01);
            Button02 = new GamepadButton(AxisType.Button02);
            Button03 = new GamepadButton(AxisType.Button03);
            Button04 = new GamepadButton(AxisType.Button04);

            ControlMap = new Dictionary<AxisType, IGamepadControl> {
                { AxisType.LeftJoystickX, LeftJoystick },
                { AxisType.LeftJoystickY, LeftJoystick },
                { AxisType.RightJoystickX, RightJoystick },
                { AxisType.RightJoystickY, RightJoystick },
                { AxisType.Button01, Button01 },
                { AxisType.Button02, Button02 },
                { AxisType.Button03, Button03 },
                { AxisType.Button04, Button04 }
            };
        }

        public void PollInput()
        {
            LeftJoystick.Poll();
            RightJoystick.Poll();
            Button01.Poll();
            Button02.Poll();
            Button03.Poll();
            Button04.Poll();
        }

        public void AddControlListener(AxisType axisType, GamepadControlChangeEvent listener)
        {
            IGamepadControl control;
            if (ControlMap.TryGetValue(axisType, out control))
            {
                control.GamepadControlChange += listener;
            }
        }

        public void RemoveControlListener(AxisType axisType, GamepadControlChangeEvent listener)
        {
            IGamepadControl control;
            if (ControlMap.TryGetValue(axisType, out control))
            {
                control.GamepadControlChange -= listener;
            }
        }
    }
}
