using System.Collections.Generic;
using UnityEngine;

namespace GameSystems.Hexagons.GridBuilders
{
    [CreateAssetMenu(fileName = "HexagonGridBuilder", menuName = "GameSys/Hexagons/GridBuilder/HexagonGridBuilder")]
    public class HexagonGridBuilder : GridBuilder
    {
        [SerializeField]
        private int size;

        public override Hexagon[] Build()
        {
            List<Hexagon> grid = new List<Hexagon>();

            for (int x = -size + 1; x < size; x++)
            {
                int y1 = Mathf.Max(-size, -x - size) + 1;
                int y2 = Mathf.Min(size, -x + size) - 1;
                for (int y = y1; y <= y2; y++)
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
