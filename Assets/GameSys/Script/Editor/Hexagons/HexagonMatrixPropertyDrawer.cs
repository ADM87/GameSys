using GameSystems.Hexagons;
using UnityEditor;
using UnityEngine;

namespace GameSystems.Editors.Hexagons
{
    [CustomPropertyDrawer(typeof(HexagonMatrix))]
    public class HexagonMatrixPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            Rect orientationRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
            Rect cellRadiusRect = new Rect(position.x, orientationRect.y + EditorGUIUtility.singleLineHeight, position.width, EditorGUIUtility.singleLineHeight);
            Rect originRect = new Rect(position.x, cellRadiusRect.y + EditorGUIUtility.singleLineHeight, position.width, EditorGUIUtility.singleLineHeight);

            EditorGUI.PropertyField(orientationRect, property.FindPropertyRelative("orientation"), GUIContent.none);
            EditorGUI.PropertyField(cellRadiusRect, property.FindPropertyRelative("cellRadius"), GUIContent.none);
            EditorGUI.PropertyField(originRect, property.FindPropertyRelative("origin"), GUIContent.none);

            EditorGUI.indentLevel = indent;

            EditorGUI.EndProperty();
        }
    }
}
