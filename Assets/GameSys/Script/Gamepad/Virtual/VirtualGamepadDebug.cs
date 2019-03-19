using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameSystems.Gamepad
{
    public class VirtualGamepadDebug : MonoBehaviour
    {
        [SerializeField]
        private bool usePhysicalGamepad;

        [SerializeField]
        private GamepadMapping mapping;

        [SerializeField]
        private AxisType[] debugAxes;

        [SerializeField]
        private VirtualGamepad virtualGamepad;

        [SerializeField]
        private Text debugArea;

        private Dictionary<AxisType, string> debugText;
        private Gamepad debugGamepad;

        private void Start()
        {
            debugGamepad = new Gamepad();
            debugGamepad.ApplyMapping(mapping);

            debugText = new Dictionary<AxisType, string>();

            if (virtualGamepad != null)
            {
                for (int i = 0; i < debugAxes.Length; ++i)
                {
                    debugText.Add(debugAxes[i], debugAxes[i].ToString() + " : 0");
                    virtualGamepad.AddControlListener(debugAxes[i], OnVirtualGamepadControlChange);
                }

                UpdateDebugText();
            }
        }

        private void Update()
        {
            if (usePhysicalGamepad)
            {
                debugGamepad.PollInput();
                if (virtualGamepad != null)
                {
                    for (int i = 0; i < debugAxes.Length; ++i)
                    {
                        switch (debugAxes[i])
                        {
                            case AxisType.LeftJoystickX:
                            case AxisType.LeftJoystickY:
                            case AxisType.RightJoystickX:
                            case AxisType.RightJoystickY:
                                VirtualGamepadJoystick virtualJoystick = (VirtualGamepadJoystick)virtualGamepad.ControlMap[debugAxes[i]];
                                GamepadJoystick debugJoystick = (GamepadJoystick)debugGamepad.ControlMap[debugAxes[i]];

                                if (debugJoystick.Magnitude >= 0.1f)
                                {
                                    if (virtualJoystick.JoyStickTouchPhase != TouchPhase.Moved)
                                    {
                                        virtualJoystick.OnTouchBegin(TouchPhase.Began, virtualJoystick.transform.position);
                                    }

                                    virtualJoystick.SetTrackingPosition(debugJoystick.Delta * virtualJoystick.ControlThreshold);
                                    virtualJoystick.OnTouchMoved(TouchPhase.Moved, Vector3.zero);
                                }
                                else
                                {
                                    virtualJoystick.SetTrackingPosition(Vector3.zero);
                                    virtualJoystick.OnTouchMoved(TouchPhase.Moved, Vector3.zero);
                                    virtualJoystick.OnTouchEnd(TouchPhase.Ended, Vector3.zero);
                                }

                                break;

                            case AxisType.Button01:
                            case AxisType.Button02:
                            case AxisType.Button03:
                            case AxisType.Button04:
                                VirtualGamepadButton virtualButton = (VirtualGamepadButton)virtualGamepad.ControlMap[debugAxes[i]];
                                GamepadButton debugButton = (GamepadButton)debugGamepad.ControlMap[debugAxes[i]];

                                virtualButton.SetValue(debugButton.Value);
                                break;
                        }
                    }
                }
            }
        }

        private void OnVirtualGamepadControlChange(AxisType axisType, float before, float after)
        {
            debugText[axisType] = axisType.ToString() + " : " + after.ToString();
            UpdateDebugText();
        }

        private void UpdateDebugText()
        {
            if (debugArea != null)
            {
                debugArea.text = "";
                for (int i = 0; i < debugAxes.Length; ++i)
                {
                    debugArea.text += debugText[debugAxes[i]] + "\n";
                }
            }
        }
    }
}
