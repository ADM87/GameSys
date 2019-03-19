using UnityEngine;

namespace GameSystems.Gamepad
{
    public class GamepadJoystick : 
        IGamepadControl, IGamepadJoystick
    {
        public event GamepadControlChangeEvent GamepadControlChange;

        public AxisType XAxis { get; private set; }
        public AxisType YAxis { get; private set; }

        public string XAxisName { get; private set; }
        public string YAxisName { get; private set; }

        public float X { get; private set; }
        public float Y { get; private set; }

        public float Magnitude { get { return Delta.magnitude; } }

        public Vector2 Delta { get { return new Vector2(X, Y); } }

        public GamepadJoystick(AxisType xAxis, AxisType yAxis)
        {
            XAxis = xAxis;
            YAxis = yAxis;

            XAxisName = xAxis.ToString();
            YAxisName = yAxis.ToString();

            X = 0;
            Y = 0;
        }

        public void ControlChanged(AxisType axisType, float before, float after)
        {
            GamepadControlChange?.Invoke(axisType, before, after);
        }

        public void Poll()
        {
            float oldX = X;
            float oldY = Y;

            X = (float)System.Math.Round(Input.GetAxis(XAxisName), 6);
            Y = (float)System.Math.Round(Input.GetAxis(YAxisName), 6);

            if (X != oldX)
            {
                ControlChanged(XAxis, oldX, X);
            }

            if (Y != oldY)
            {
                ControlChanged(YAxis, oldY, Y);
            }
        }

        public void MapControl(GamepadMapping.MappingEntry mapping)
        {
            if (mapping.GamepadAxis == XAxis)
            {
                XAxisName = mapping.InputAxis;
            }
            else if (mapping.GamepadAxis == YAxis)
            {
                YAxisName = mapping.InputAxis;
            }
        }
    }
}
