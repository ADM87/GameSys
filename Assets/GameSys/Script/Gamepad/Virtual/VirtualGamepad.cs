using System.Collections.Generic;
using UnityEngine;

namespace GameSystems.Gamepad
{
    public class VirtualGamepad : MonoBehaviour,
        IGamepad
    {
        [SerializeField]
        private VirtualGamepadTouchZone touchZone;
        public VirtualGamepadTouchZone TouchZone { get { return touchZone; } }

        [SerializeField]
        private VirtualGamepadJoystick leftJoystick = null;
        public IGamepadJoystick LeftJoystick { get { return leftJoystick; } }

        [SerializeField]
        private VirtualGamepadJoystick rightJoystick = null;
        public IGamepadJoystick RightJoystick { get { return rightJoystick; } }

        [SerializeField]
        private VirtualGamepadButton button01 = null;
        public IGamepadButton Button01 { get { return button01; } }

        [SerializeField]
        private VirtualGamepadButton button02 = null;
        public IGamepadButton Button02 { get { return button02; } }

        [SerializeField]
        private VirtualGamepadButton button03 = null;
        public IGamepadButton Button03 { get { return button03; } }

        [SerializeField]
        private VirtualGamepadButton button04 = null;
        public IGamepadButton Button04 { get { return button04; } }

        public Dictionary<AxisType, IGamepadControl> ControlMap { get; private set; }

        private void Awake()
        {
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

        private void OnEnable()
        {
            if (leftJoystick != null)
            {
                touchZone.TouchStart += leftJoystick.OnTouchBegin;
                touchZone.TouchMove += leftJoystick.OnTouchMoved;
                touchZone.TouchEnd += leftJoystick.OnTouchEnd;
            }
        }

        private void OnDisable()
        {
            if (leftJoystick != null)
            {
                touchZone.TouchStart += leftJoystick.OnTouchBegin;
                touchZone.TouchMove += leftJoystick.OnTouchMoved;
                touchZone.TouchEnd += leftJoystick.OnTouchEnd;
            }
        }

        public void AddControlListener(AxisType axisType, GamepadControlChangeEvent listener)
        {
            if (ControlMap.ContainsKey(axisType))
            {
                ControlMap[axisType].GamepadControlChange += listener;
            }
        }

        public void RemoveControlListener(AxisType axisType, GamepadControlChangeEvent listener)
        {
            if (ControlMap.ContainsKey(axisType))
            {
                ControlMap[axisType].GamepadControlChange -= listener;
            }
        }

        public void PollInput() { }
    }
}
