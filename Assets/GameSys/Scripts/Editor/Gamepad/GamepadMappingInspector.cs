using GameSys.Gamepad;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace GameSys.Editors.Gamepad
{
    [CustomEditor(typeof(GamepadMapping))]
    public class GamepadMappingInspector : Editor
    {
        private string[] inputChoices;
        private Object inputManager;
        private ReorderableList entryList;

        private void OnEnable()
        {
            inputManager = AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/InputManager.asset")[0];

            SerializedObject input = new SerializedObject(inputManager);
            SerializedProperty inputAxes = input.FindProperty("m_Axes");
            SerializedProperty entries = serializedObject.FindProperty("entries");

            entryList = new ReorderableList(serializedObject, entries, true, true, true, true);

            inputChoices = new string[inputAxes.arraySize];
            for (int i = 0; i < inputChoices.Length; ++i)
            {
                var axes = inputAxes.GetArrayElementAtIndex(i);
                inputChoices[i] = axes.FindPropertyRelative("m_Name").stringValue;
            }
        }

        public override void OnInspectorGUI()
        {
            entryList.drawHeaderCallback = headerRect => EditorGUI.LabelField(headerRect, "Control Mapping");
            entryList.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) => {
                SerializedProperty entry = entryList.serializedProperty.GetArrayElementAtIndex(index);
                SerializedProperty gamepadAxis = entry.FindPropertyRelative("gamepadAxis");
                SerializedProperty inputAxis = entry.FindPropertyRelative("inputAxis");

                float x = rect.x;
                float width = 90;

                EditorGUI.LabelField(
                    new Rect(x, rect.y + 2, width, EditorGUIUtility.singleLineHeight),
                    "Gamepad Axis"
                );

                x += width;
                width = (rect.width * 0.25f);

                AxisType gamepadAxisType = (AxisType)EditorGUI.EnumPopup(
                    new Rect(x, rect.y + 2, width, EditorGUIUtility.singleLineHeight), 
                    (AxisType)gamepadAxis.enumValueIndex
                );
                gamepadAxis.enumValueIndex = (int)gamepadAxisType;

                x += width + 2;
                width = 65;

                EditorGUI.LabelField(
                    new Rect(x, rect.y + 2, width, EditorGUIUtility.singleLineHeight),
                    "Input Axis"
                );

                x += width;
                width = rect.width - x + 30;

                int selected = ArrayUtility.IndexOf(inputChoices, inputAxis.stringValue);
                selected = EditorGUI.Popup(
                    new Rect(x, rect.y + 2, width, EditorGUIUtility.singleLineHeight), 
                    selected, inputChoices
                );
                inputAxis.stringValue = inputChoices[selected];
            };
            entryList.DoLayoutList();

            serializedObject.ApplyModifiedProperties();
        }
    }
}
