using UnityEngine;
using UnityEngine.UI;

namespace GameSys.Gamepad
{
    [RequireComponent(typeof(CanvasGroup))]
    public class VirtualGamepadJoystick : MonoBehaviour,
        IGamepadControl, IGamepadJoystick
    {
        public event GamepadControlChangeEvent GamepadControlChange;

        [SerializeField, Range(1f, 600f)]
        private float controlThreshold = 1f;
        public float ControlThreshold { get { return controlThreshold; } }

        [SerializeField]
        private Image joystickBase;
        [SerializeField]
        private Image joystickGrip;

        [SerializeField]
        private AxisType xAxis;
        public AxisType XAxis { get { return xAxis; } }

        [SerializeField]
        private AxisType yAxis;
        public AxisType YAxis { get { return yAxis; } }

        [SerializeField]
        private TouchPhase joystickTouchPhase;
        public TouchPhase JoyStickTouchPhase { get { return joystickTouchPhase; } }

        public float X { get; private set; }
        public float Y { get; private set; }
        
        public float Magnitude { get { return Delta.magnitude; } }
        
        public Vector2 Delta { get { return new Vector2(X, Y); } }

        public string XAxisName { get { return xAxis.ToString(); } }
        public string YAxisName { get { return yAxis.ToString(); } }

        private CanvasGroup canvasGroup;
        private float targetVisibility = 0f;
        private Vector3 trackingPosition;

        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            canvasGroup.alpha = targetVisibility;
        }

        private void Update()
        {
            if (canvasGroup.alpha != targetVisibility)
            {
                float alpha = Mathf.Lerp(canvasGroup.alpha, targetVisibility, 15f * Time.deltaTime);
                if (Mathf.Abs(alpha - targetVisibility) <= 0.01f)
                {
                    alpha = targetVisibility;
                }
                canvasGroup.alpha = alpha;
            }
        }

        public void OnTouchBegin(TouchPhase touchPhase, Vector3 touchPosition)
        {
            joystickTouchPhase = touchPhase;
            transform.position = touchPosition;

            joystickGrip.transform.localPosition = Vector3.zero;
            trackingPosition = Vector3.zero;

            X = 0f;
            Y = 0f;

            targetVisibility = 1f;
        }
        
        public void OnTouchMoved(TouchPhase touchPhase, Vector3 delta)
        {
            joystickTouchPhase = touchPhase;
            ProcessMove(delta);
        }

        public void OnTouchEnd(TouchPhase touchPhase, Vector3 touchPosition)
        {
            joystickTouchPhase = touchPhase;
            targetVisibility = 0f;

            trackingPosition = Vector3.zero;
            ProcessMove(Vector3.zero);
        }

        public void SetTrackingPosition(Vector3 position)
        {
#if UNITY_EDITOR
            trackingPosition = position;
#endif
        }

        private void ProcessMove(Vector3 delta)
        {
            trackingPosition.x += delta.x;
            trackingPosition.y += delta.y;

            Vector3 stickPosition = trackingPosition;

            float magnitude = stickPosition.magnitude;
            if (magnitude > controlThreshold)
            {
                float correction = controlThreshold / magnitude;
                stickPosition.x *= correction;
                stickPosition.y *= correction;
            }

            float oldX = X;
            float oldY = Y;

            X = (float)System.Math.Round(stickPosition.x / controlThreshold, 6);
            Y = (float)System.Math.Round(stickPosition.y / controlThreshold, 6);

            joystickGrip.transform.localPosition = new Vector3(X * controlThreshold, Y * controlThreshold, 0);

            if (X != oldX)
            {
                ControlChanged(xAxis, oldX, X);
            }

            if (Y != oldY)
            {
                ControlChanged(yAxis, oldY, Y);
            }
        }

        public void Poll() { }

        public void ControlChanged(AxisType axisType, float before, float after)
        {
            GamepadControlChange?.Invoke(axisType, before, after);
        }
    }
}
