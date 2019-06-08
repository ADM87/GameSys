using UnityEngine;

namespace GameSystems.AStar
{
    public class AStarSystem : MonoBehaviour
    {
        [Header("Grid")]
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

        [Header("Collision")]
        [SerializeField]
        private bool showNodes;
        [SerializeField]
        private LayerMask collisionLayers;
        [SerializeField, Range(0f, 1f)]
        private float collisionBuffer;

        [SerializeField, HideInInspector]
        private AStarNode[,] nodeMap;

        public void BuildNodeMap()
        {
            nodeMap = new AStarNode[gridWidth, gridHeight];
            if (gridWidth > 0 && gridHeight > 0)
            {
                float offset = gridSpacing / 2f;
                Vector3 nodeSize = new Vector3(gridSpacing, gridSpacing, gridSpacing) * collisionBuffer;

                for (int x = 0; x < gridWidth; x++)
                {
                    for (int y = 0; y < gridHeight; y++)
                    {
                        Vector3 position = transform.position;
                        position.x += (x * gridSpacing) + offset;
                        position.z -= (y * gridSpacing) + offset;

                        bool collision = Physics.CheckBox(position, nodeSize / 2f, Quaternion.identity, collisionLayers);
                        nodeMap[x, y] = new AStarNode(position, !collision);
                    }
                }
            }
        }

        private void OnValidate()
        {
            gridSpacing = Mathf.Max(gridSpacing, 0);
        }

        private void OnDrawGizmos()
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

            if (showNodes && nodeMap != null && nodeMap.GetLength(0) > 0 && nodeMap.GetLength(1) > 0)
            {
                int xLength = nodeMap.GetLength(0);
                int yLength = nodeMap.GetLength(1);

                Vector3 nodeSize = new Vector3(gridSpacing, 0, gridSpacing) * 0.75f;

                for (int x = 0; x < xLength; x++)
                {
                    for (int y = 0; y < yLength; y++)
                    {
                        Gizmos.color = nodeMap[x, y].Walkable ? Color.white : Color.red;
                        Gizmos.DrawCube(nodeMap[x, y].Position, nodeSize);
                    }
                }
            }
        }
    }
}
