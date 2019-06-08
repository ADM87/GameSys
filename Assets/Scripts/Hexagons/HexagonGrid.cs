using GameSystems.Hexagons.GridBuilders;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace GameSystems.Hexagons
{
    [ExecuteInEditMode]
    public class HexagonGrid : MonoBehaviour
    {
        [SerializeField]
        private bool visualize;
        [SerializeField]
        private bool showCoordinates;

        [SerializeField]
        private GridBuilder gridBuilder;
        public GridBuilder GridBuilder { get { return gridBuilder; } }

        [SerializeField]
        private HexagonMatrix matrix;
        public HexagonMatrix Matrix { get { return matrix; } }

        [HideInInspector, SerializeField]
        private Hexagon[] hexagons;
        public Hexagon[] Hexagons { get { return hexagons; } }

        public void BuildGrid()
        {
            if (gridBuilder != null)
            {
                hexagons = gridBuilder.Build();
            }
        }

        public void ClearGrid()
        {
            hexagons = new Hexagon[0];
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (!visualize && !showCoordinates)
            {
                return;
            }

            if (hexagons.Length > 0)
            {
                Gizmos.color = Color.green;

                float zoom = SceneView.currentDrawingSceneView.camera.orthographicSize;
                int fontSize = 50;

                GUIStyle style = new GUIStyle();
                style.alignment = TextAnchor.MiddleCenter; // This isn't working - TODO - Fix it.
                style.fontSize = Mathf.FloorToInt(fontSize / zoom);

                for (int i = 0; i < hexagons.Length; i++)
                {
                    Vector2 center = matrix.From(hexagons[i]);
                    if (showCoordinates)
                    {
                        GUIContent label = new GUIContent(hexagons[i].ToString());
                        
                        float x = transform.position.x + center.x;
                        float y = transform.position.y + center.y;

                        Handles.Label(new Vector3(x, y, transform.position.z), label, style);
                    }

                    if (visualize)
                    {
                        Vector3 start = center + matrix.CornerOffset(0);
                        for (int j = 1; j <= Hexagon.Edges; j++)
                        {
                            Vector3 end = matrix.From(hexagons[i]) + matrix.CornerOffset(j);

                            Gizmos.DrawLine(transform.position + start, transform.position + end);

                            start = end;
                        }
                    }
                }
            }
        }
#endif
    }
}
