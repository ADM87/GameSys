using System.Collections.Generic;
using UnityEngine;

namespace GameSystems.Hexagons.GridBuilders
{
    [CreateAssetMenu(fileName = "TriangleGridBuilder", menuName = "GameSys/Hexagons/GridBuilder/TriangleGridBuilder")]
    public class TriangleGridBuilder : GridBuilder
    {
        [SerializeField]
        private int size;

        public override Hexagon[] Build()
        {
            List<Hexagon> grid = new List<Hexagon>();

            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size - x; y++)
                {
                    grid.Add(new Hexagon(x, y));
                }
            }

            return grid.ToArray();
        }

        public override void OnAfterDeserialize()
        {
            size = Mathf.Max(size, 1);
        }
    }
}
