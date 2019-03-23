using GameSystems.Hexagons.GridBuilders;
using UnityEngine;

namespace GameSystems.Hexagons
{
    [ExecuteInEditMode]
    public class HexagonGrid : MonoBehaviour
    {
        [SerializeField]
        private bool visualize;

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

        private void OnDrawGizmos()
        {
            if (visualize && hexagons.Length > 0)
            {
                Gizmos.color = Color.green;

                for (int i = 0; i < hexagons.Length; i++)
                {
                    Vector3 start = matrix.Origin + matrix.From(hexagons[i]) + matrix.CornerOffset(0);
                    for (int j = 1; j <= Hexagon.Edges; j++)
                    {
                        Vector3 end = matrix.Origin + matrix.From(hexagons[i]) + matrix.CornerOffset(j);                        

                        Gizmos.DrawLine(transform.position + start, transform.position + end);

                        start = end;
                    }
                }
            }
        }
    }
}
