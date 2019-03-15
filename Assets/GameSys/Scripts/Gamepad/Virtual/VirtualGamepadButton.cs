using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GameSys.Gamepad
{
    public class VirtualGamepadButton : MonoBehaviour,
        IGamepadControl, IGamepadButton
    {
        public event GamepadControlChangeEvent GamepadControlChange;

        [SerializeField]
        private AxisType axis;
        public AxisType Axis { get { return axis; } }

        [SerializeField]
        private Image graphic;

        public string AxisName { get { return axis.ToString(); } }

        public float Value { get; private set; }

        private EventTrigger eventTrigger;
        private float targetScale = 1f;

        private void Awake()
        {
            eventTrigger = gameObject.AddComponent<EventTrigger>();
            AddEvent(EventTriggerType.PointerDown, OnControlDown);
            AddEvent(EventTriggerType.PointerUp, OnControlUp);
        }

        private void Update()
        {
            if (graphic.transform.localScale.magnitude != targetScale)
            {
                Vector3 scale = graphic.transform.localScale;

                scale.x = Mathf.Lerp(scale.x, targetScale, Time.deltaTime * 15f);
                scale.y = Mathf.Lerp(scale.y, targetScale, Time.deltaTime * 15f);

                graphic.transform.localScale = scale;
            }
        }

        private void OnControlDown(BaseEventData eventData)
        {
            Value = 1f;
            targetScale = 0.9f;

            ControlChanged(axis, 0f, Value);
        }

        private void OnControlUp(BaseEventData eventData)
        {
            Value = 0f;
            targetScale = 1f;

            ControlChanged(axis, 1f, Value);
        }

        private void AddEvent(EventTriggerType eventType, UnityAction<BaseEventData> handler)
        {
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = eventType;
            entry.callback.AddListener(handler);
            eventTrigger.triggers.Add(entry);
        }

        public void SetValue(float value)
        {
#if UNITY_EDITOR
            if (value <= 0f)
            {
                OnControlUp(null);
            }
            else
            {
                OnControlDown(null);
            }
#endif
        }

        public void Poll() { }

        public void ControlChanged(AxisType axisType, float before, float after)
        {
            GamepadControlChange?.Invoke(axisType, before, after);
        }
    }
}
