using UnityEngine;

namespace GameSystems.Gamepad
{
    public delegate void VirtualGamepadTouchEvent(TouchPhase touchPhase, Vector3 touchPosition);

    public class VirtualGamepadTouchZone : MonoBehaviour
    {
        public event VirtualGamepadTouchEvent TouchStart;
        public event VirtualGamepadTouchEvent TouchMove;
        public event VirtualGamepadTouchEvent TouchStationary;
        public event VirtualGamepadTouchEvent TouchEnd;

        private TouchPhase currentTouchPhase;

        private RectTransform rectTransform;
        private Rect boundRect;

        private Vector3? touchPosition;
        private Vector3? lastTouchPosition;

        private float scaleFactor = 1f;

        public Vector3 TouchPosition { get { return touchPosition.HasValue ? touchPosition.Value : Vector3.zero; } }
        public Vector3 LastTouchPosition { get { return lastTouchPosition.HasValue ? lastTouchPosition.Value : Vector3.zero; } }
        public Vector3 TouchDelta { get; private set; }

        private void Start()
        {
            currentTouchPhase = TouchPhase.Ended;

            rectTransform = rectTransform ?? GetComponent<RectTransform>();
            boundRect = rectTransform.rect;

            Canvas canvas = FindObjectOfType<Canvas>();
            scaleFactor = canvas.scaleFactor;

            boundRect.width *= scaleFactor;
            boundRect.height *= scaleFactor;
        }

        private void Update()
        {
            bool touchInProgress = currentTouchPhase == TouchPhase.Moved || currentTouchPhase == TouchPhase.Stationary;

            touchPosition = Input.mousePosition;
            if (touchInProgress || boundRect.Contains(touchPosition.Value))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    currentTouchPhase = TouchPhase.Began;
                    TouchStart?.Invoke(currentTouchPhase, touchPosition.Value);
                }

                if (Input.GetMouseButton(0))
                {
                    if (!lastTouchPosition.HasValue)
                    {
                        lastTouchPosition = touchPosition.Value;
                    }

                    TouchDelta = (touchPosition.Value - lastTouchPosition.Value) / scaleFactor;
                    lastTouchPosition = touchPosition.Value;

                    if (TouchDelta.magnitude > 0)
                    {
                        currentTouchPhase = TouchPhase.Moved;
                        TouchMove?.Invoke(currentTouchPhase, TouchDelta);
                    }
                    else
                    {
                        currentTouchPhase = TouchPhase.Stationary;
                        TouchStationary?.Invoke(currentTouchPhase, touchPosition.Value);
                    }
                }

                if (Input.GetMouseButtonUp(0))
                {
                    lastTouchPosition = null;
                    currentTouchPhase = TouchPhase.Ended;
                    TouchEnd?.Invoke(currentTouchPhase, touchPosition.Value);
                }
            }
        }
    }
}
