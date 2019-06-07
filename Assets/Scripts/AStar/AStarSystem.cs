using UnityEngine;

namespace GameSystems.AStar
{
    public class AStarSystem : MonoBehaviour
    {
        [Header("Grid")]
        [SerializeField]
        private bool showGrid;
        [SerializeField]
        private uint gridWidth;
        [SerializeField]
        private uint gridHeight;
        [SerializeField]
        private float gridSpacing;

        private void OnValidate()
        {
            gridSpacing = Mathf.Max(gridSpacing, 0);
        }

        private void OnDrawGizmos()
        {
            if (showGrid && gridWidth > 0 && gridHeight > 0 && gridSpacing > 0)
            {
                Gizmos.color = Color.white;

                float left = transform.position.x;
                float right = left + gridWidth * gridSpacing;
                float top = transform.position.z;
                float bottom = top - gridHeight * gridSpacing;

                Vector3 topRight = new Vector3(right, transform.position.y, top);
                Vector3 bottomRight = new Vector3(right, transform.position.y, bottom);
                Vector3 bottomLeft = new Vector3(left, transform.position.y, bottom);

                Gizmos.DrawLine(transform.position, topRight);
                Gizmos.DrawLine(topRight, bottomRight);
                Gizmos.DrawLine(bottomRight, bottomLeft);
                Gizmos.DrawLine(bottomLeft, transform.position);
                
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
}
