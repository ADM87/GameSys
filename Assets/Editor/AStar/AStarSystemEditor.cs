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

            if (GUILayout.Button("Update Nodes"))
            {
                AStarSystem aStarSystem = (AStarSystem)target;
                aStarSystem.BuildNodeMap();

                EditorUtility.SetDirty(target);
            }
        }
    }
}
