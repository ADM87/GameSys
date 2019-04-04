using GameSystems.Hexagons;
using UnityEditor;
using UnityEngine;

namespace GameSystems.Editors.Hexagons
{
    [CustomPropertyDrawer(typeof(HexagonMatrix))]
    public class HexagonMatrixPropertyDrawer : PropertyDrawer
    {
        private static readonly GUIContent OrientationLabel = new GUIContent("Orientation");
        private static readonly GUIContent CellRadiusLabel = new GUIContent("CellRadius");
        private static readonly GUIContent OriginLabel = new GUIContent("Origin");

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label, EditorStyles.boldLabel);

            float y = position.y + EditorGUIUtility.singleLineHeight;

            Rect orientationRect = new Rect(position.x, y, position.width, EditorGUIUtility.singleLineHeight);
            orientationRect = EditorGUI.PrefixLabel(orientationRect, GUIUtility.GetControlID(FocusType.Passive), OrientationLabel);

            y += EditorGUIUtility.singleLineHeight;
            Rect cellRadiusRect = new Rect(position.x, y, position.width, EditorGUIUtility.singleLineHeight);
            cellRadiusRect = EditorGUI.PrefixLabel(cellRadiusRect, GUIUtility.GetControlID(FocusType.Passive), CellRadiusLabel);

            y += EditorGUIUtility.singleLineHeight;
            Rect originRect = new Rect(position.x, y, position.width, EditorGUIUtility.singleLineHeight);
            originRect = EditorGUI.PrefixLabel(originRect, GUIUtility.GetControlID(FocusType.Passive), OriginLabel);

            EditorGUI.PropertyField(orientationRect, property.FindPropertyRelative("orientation"), GUIContent.none);
            EditorGUI.PropertyField(cellRadiusRect, property.FindPropertyRelative("cellRadius"), GUIContent.none);
            EditorGUI.PropertyField(originRect, property.FindPropertyRelative("origin"), GUIContent.none);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight * 4f;
        }
    }
}
