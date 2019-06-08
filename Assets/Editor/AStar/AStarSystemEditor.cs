using GameSystems.AStar;
using UnityEditor;
using UnityEngine;

namespace GameSystems.Editors
{
    [CustomEditor(typeof(AStarSystem))]
    public class AStarSystemEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            AStarSystem aStarSystem = (AStarSystem)target;

            if (aStarSystem.Layout == AStarSystem.GridLayout.Hexagonal)
            {
                if (GUILayout.Button("Build Hexagon Grid"))
                {
                    aStarSystem.BuildHexagonGrid();
                    EditorUtility.SetDirty(target);
                }
            }

            if (GUILayout.Button("Update Nodes"))
            {
                aStarSystem.BuildNodeMap();
                EditorUtility.SetDirty(target);
            }

            if (GUILayout.Button("Clear Nodes"))
            {
                aStarSystem.ClearNodeMap();
                EditorUtility.SetDirty(target);
            }
        }
    }
}
