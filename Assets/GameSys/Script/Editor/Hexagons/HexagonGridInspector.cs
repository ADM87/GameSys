using GameSystems.Hexagons;
using UnityEditor;
using UnityEngine;

namespace GameSystems.Editors.Hexagons
{
    [CustomEditor(typeof(HexagonGrid))]
    public class HexagonGridInspector : Editor
    {
        private SerializedProperty visualize;
        private SerializedProperty gridBuilder;
        private SerializedProperty matrix;

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            CheckCache();

            EditorGUILayout.PropertyField(visualize);
            EditorGUILayout.PropertyField(gridBuilder);

            if (gridBuilder.objectReferenceValue != null)
            {
                EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                CreateEditor(gridBuilder.objectReferenceValue).OnInspectorGUI();
                EditorGUILayout.EndVertical();
            }

            EditorGUILayout.PropertyField(matrix);

            if (gridBuilder.objectReferenceValue == null)
            {
                EditorGUILayout.HelpBox("Please load a grid builder.", MessageType.Warning);
            }
            else
            {
                if (GUILayout.Button("Build Grid"))
                {
                    HexagonGrid hexagonGrid = (HexagonGrid)target;
                    hexagonGrid.BuildGrid();

                    SceneView.RepaintAll();
                }
            }

            serializedObject.ApplyModifiedProperties();
        }

        private void CheckCache()
        {
            if (visualize == null || gridBuilder == null || matrix == null)
            {
                visualize = serializedObject.FindProperty("visualize");
                gridBuilder = serializedObject.FindProperty("gridBuilder");
                matrix = serializedObject.FindProperty("matrix");
            }
        }
    }
}
