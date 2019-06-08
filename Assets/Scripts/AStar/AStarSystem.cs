using UnityEngine;

namespace GameSystems.AStar
{
    public class AStarSystem : MonoBehaviour
    {
        public enum GridLayout
        {
            Cartesian, Hexagonal 
        }

        [SerializeField]
        private GridLayout layout;
        private GridLayout lastSelectedLayout;

        [SerializeField]
        private bool showGrid;
        [SerializeField]
        private bool showBorder;

        [SerializeField]
        private uint gridWidth;
        [SerializeField]
        private uint gridHeight;
        [SerializeField]
        private float gridSpacing;

        [SerializeField]
        private bool showNodes;
        [SerializeField]
        private LayerMask collisionLayers;
        [SerializeField, Range(0f, 1f)]
        private float collisionBuffer;

        [SerializeField, HideInInspector]
        private AStarNode[] nodeMap;

        public void BuildNodeMap()
        {
            if (layout == GridLayout.Cartesian)
            {
                BuildCartesianNodeMap();
            }
            else if (layout == GridLayout.Hexagonal)
            {
                BuildHexagonalNodeMap();
            }
        }

        private void OnValidate()
        {
            gridSpacing = Mathf.Max(gridSpacing, 0);

            if (lastSelectedLayout != layout)
            {
                lastSelectedLayout = layout;
                nodeMap = null;
            }
        }

        private void OnDrawGizmos()
        {
            if (layout == GridLayout.Cartesian)
            {
                DrawCartesianGizmo();
            }
            else if (layout == GridLayout.Hexagonal)
            {
                DrawHexagonalGizmo();
            }

            if (showNodes && nodeMap != null && nodeMap.Length > 0)
            {
                for (int i = 0; i < nodeMap.Length; i++)
                {
                    Gizmos.color = nodeMap[i].Walkable ? Color.white : Color.red;
                    Gizmos.DrawSphere(nodeMap[i].Position, gridSpacing * 0.45f);
                }
            }
        }

        private void BuildCartesianNodeMap()
        {
            nodeMap = new AStarNode[gridWidth * gridHeight];
            if (gridWidth > 0 && gridHeight > 0)
            {
                float offset = gridSpacing / 2f;
                Vector3 nodeSize = new Vector3(gridSpacing, gridSpacing, gridSpacing) * collisionBuffer;

                uint length = gridWidth * gridHeight;
                for (int i = 0; i < length; i++)
                {
                    float x = i % gridWidth;
                    float y = -1 * ((x % gridWidth) - i) / gridWidth;

                    Vector3 position = transform.position;
                    position.x += (x * gridSpacing) + offset;
                    position.z -= (y * gridSpacing) + offset;

                    bool collision = Physics.CheckBox(position, nodeSize / 2f, Quaternion.identity, collisionLayers);
                    nodeMap[i] = new AStarNode(position, !collision);
                }
            }
        }

        private void BuildHexagonalNodeMap()
        {

        }

        private void DrawCartesianGizmo()
        {
            if ((showGrid || showBorder) && gridWidth > 0 && gridHeight > 0 && gridSpacing > 0)
            {
                Gizmos.color = Color.white;

                float left = transform.position.x;
                float right = left + gridWidth * gridSpacing;
                float top = transform.position.z;
                float bottom = top - gridHeight * gridSpacing;

                if (showGrid || showBorder)
                {
                    Vector3 topRight = new Vector3(right, transform.position.y, top);
                    Vector3 bottomRight = new Vector3(right, transform.position.y, bottom);
                    Vector3 bottomLeft = new Vector3(left, transform.position.y, bottom);

                    Gizmos.DrawLine(transform.position, topRight);
                    Gizmos.DrawLine(topRight, bottomRight);
                    Gizmos.DrawLine(bottomRight, bottomLeft);
                    Gizmos.DrawLine(bottomLeft, transform.position);
                }

                if (showGrid)
                {
                    Gizmos.color = new Color(1, 1, 1, 0.5f);

                    for (float x = left + gridSpacing; x < right; x += gridSpacing)
                    {
                        Gizmos.DrawLine(new Vector3(x, transform.position.y, top), new Vector3(x, transform.position.y, bottom));
                    }

                    for (float y = top - gridSpacing; y > bottom; y -= gridSpacing)
                    {
                        Gizmos.DrawLine(new Vector3(left, transform.position.y, y), new Vector3(right, transform.position.y, y));
                    }
                }
            }
        }

        private void DrawHexagonalGizmo()
        {

        }
    }
}
