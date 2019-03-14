using UnityEngine;

namespace GameSys.Utility
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(RectTransform))]
    public class SafeAreaHelper : MonoBehaviour
    {
        static public Vector4 iPhoneXPadding = new Vector4(44f, 32f, 44f, 21f) * 3f;

        public enum AnchorPoint
        {
            TopLeft,
            TopCenter,
            TopRight,

            CenterLeft,
            Center,
            CenterRight,

            BottomLeft,
            BottomCenter,
            BottomRight
        }

        [SerializeField]
        private bool iPhoneXDebug = false;

        [SerializeField]
        private AnchorPoint anchorPoint;

        private float scaleFactor;
        private RectTransform rectTransform;

        private void Start()
        {
            rectTransform = rectTransform ?? GetComponent<RectTransform>();

            Canvas canvas = FindObjectOfType<Canvas>();
            scaleFactor = canvas.scaleFactor;

            AdjustPosition();
        }

        private void LateUpdate()
        {
#if UNITY_EDITOR
            if (!Application.isPlaying)
            {
                rectTransform = rectTransform ?? GetComponent<RectTransform>();

                Canvas canvas = FindObjectOfType<Canvas>();
                scaleFactor = canvas.scaleFactor;

                AdjustPosition();
            }
#endif
        }

        private void AdjustPosition()
        {
            rectTransform.anchorMin = Vector2.zero;
            rectTransform.anchorMax = Vector2.zero;

            float left = Screen.safeArea.xMin / scaleFactor;
            float top = Screen.safeArea.yMax / scaleFactor;
            float right = Screen.safeArea.xMax / scaleFactor;
            float bottom = Screen.safeArea.yMin / scaleFactor;

            Vector2 center = Screen.safeArea.center / scaleFactor;
            Vector2 position = Vector2.zero;

#if UNITY_EDITOR
            if (iPhoneXDebug)
            {
                left += iPhoneXPadding.x;
                top -= iPhoneXPadding.y;
                right -= iPhoneXPadding.z;
                bottom += iPhoneXPadding.w;
            }
#endif

            switch (anchorPoint)
            {
                case AnchorPoint.TopLeft:
                    position.Set(left, top);
                    break;

                case AnchorPoint.CenterLeft:
                    position.Set(left, center.y);
                    break;

                case AnchorPoint.BottomLeft:
                    position.Set(left, bottom);
                    break;

                case AnchorPoint.TopCenter:
                    position.Set(center.x, top);
                    break;

                case AnchorPoint.Center:
                    position.Set(center.x, center.y);
                    break;

                case AnchorPoint.BottomCenter:
                    position.Set(center.x, bottom);
                    break;

                case AnchorPoint.TopRight:
                    position.Set(right, top);
                    break;

                case AnchorPoint.CenterRight:
                    position.Set(right, center.y);
                    break;

                case AnchorPoint.BottomRight:
                    position.Set(right, bottom);
                    break;
            }

            rectTransform.anchoredPosition = position;
        }
    }
}
