using GameSystems.Hexagons;
using UnityEditor;
using UnityEngine;

namespace GameSystems.Editors.Hexagons
{
    [CustomEditor(typeof(HexagonGrid))]
    public class HexagonGridInspector : Editor
    {
        private SerializedProperty visualize;
        private SerializedProperty showCoordinates;
        private SerializedProperty gridBuilder;
        private SerializedProperty matrix;

        public override void OnInspectorGUI()
        {
            CheckCache();

            EditorGUILayout.PropertyField(visualize);
            EditorGUILayout.PropertyField(showCoordinates);

            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.PropertyField(gridBuilder);
            if (gridBuilder.objectReferenceValue != null)
            {
                CreateEditor(gridBuilder.objectReferenceValue).OnInspectorGUI();

                if (GUILayout.Button("Build Grid"))
                {
                    HexagonGrid hexagonGrid = (HexagonGrid)target;
                    hexagonGrid.BuildGrid();

                    SceneView.RepaintAll();
                }
            }
            else
            {
                EditorGUILayout.HelpBox("Please load a grid builder.", MessageType.Warning);

                HexagonGrid hexagonGrid = (HexagonGrid)target;
                hexagonGrid.ClearGrid();

                SceneView.RepaintAll();
            }
            EditorGUILayout.EndVertical();

            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.PropertyField(matrix);
            EditorGUILayout.EndVertical();

            serializedObject.ApplyModifiedProperties();
        }

        private void CheckCache()
        {
            if (visualize == null || showCoordinates  == null || gridBuilder == null || matrix == null)
            {
                showCoordinates = serializedObject.FindProperty("showCoordinates");
                visualize = serializedObject.FindProperty("visualize");
                gridBuilder = serializedObject.FindProperty("gridBuilder");
                matrix = serializedObject.FindProperty("matrix");
            }
        }
    }
}
