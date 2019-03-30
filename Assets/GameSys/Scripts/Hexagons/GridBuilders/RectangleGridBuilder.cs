using System.Collections.Generic;
using UnityEngine;

namespace GameSystems.Hexagons.GridBuilders
{
    [CreateAssetMenu(fileName = "RectangleGridBuilder", menuName = "GameSys/Hexagons/GridBuilder/RectangleGridBuilder")]
    public class RectangleGridBuilder : GridBuilder
    {
        [SerializeField]
        private Vector2Int size;

        [SerializeField]
        protected HexagonMatrix.Axis axis;

        public override Hexagon[] Build()
        {
            List<Hexagon> grid = new List<Hexagon>();

            switch (axis)
            {
                case HexagonMatrix.Axis.XY:
                    for (int y = 0; y < size.y; y++)
                    {
                        int yOff = y >> 1;
                        for (int x = -yOff; x < size.x - yOff; x++)
                        {
                            grid.Add(new Hexagon(x, y));
                        }
                    }
                    break;

                case HexagonMatrix.Axis.XZ:
                    for (int y = 0; y < size.y; y++)
                    {
                        int yOff = y >> 1;
                        for (int x = -yOff; x < size.x - yOff; x++)
                        {
                            grid.Add(new Hexagon(-x - y, y, x));
                        }
                    }
                    break;

                case HexagonMatrix.Axis.YX:
                    for (int y = 0; y < size.y; y++)
                    {
                        int yOff = y >> 1;
                        for (int x = -yOff; x < size.x - yOff; x++)
                        {
                            grid.Add(new Hexagon(y, x, -x - y));
                        }
                    }
                    break;

                case HexagonMatrix.Axis.YZ:
                    for (int y = 0; y < size.y; y++)
                    {
                        int yOff = y >> 1;
                        for (int x = -yOff; x < size.x - yOff; x++)
                        {
                            grid.Add(new Hexagon(x, -x - y, y));
                        }
                    }
                    break;

                case HexagonMatrix.Axis.ZX:
                    for (int x = 0; x < size.x; x++)
                    {
                        int xOff = x >> 1;
                        for (int y = -xOff; y < size.y - xOff; y++)
                        {
                            grid.Add(new Hexagon(-x - y, y, x));
                        }
                    }
                    break;

                case HexagonMatrix.Axis.ZY:
                    for (int x = 0; x < size.x; x++)
                    {
                        int xOff = x >> 1;
                        for (int y = -xOff; y < size.y - xOff; y++)
                        {
                            grid.Add(new Hexagon(x, -x - y, y));
                        }
                    }
                    break;
            }

            return grid.ToArray();
        }

        public override void OnAfterDeserialize()
        {
            size.x = Mathf.Max(size.x, 1);
            size.y = Mathf.Max(size.y, 1);
        }
    }
}
