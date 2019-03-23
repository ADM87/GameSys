using UnityEngine;

namespace GameSystems.Hexagons.GridBuilders
{
    [CreateAssetMenu(fileName = "RhombusGridBuilder", menuName = "GameSys/Hexagons/GridBuilder/RhombusGridBuilder")]
    public class RhombusGridBuilder : GridBuilder
    {
        [SerializeField]
        private Vector2Int size;

        [SerializeField]
        protected HexagonMatrix.Axis axis;

        public override Hexagon[] Build()
        {
            Hexagon[] grid = new Hexagon[size.x * size.y];

            switch (axis)
            {
                case HexagonMatrix.Axis.XY:
                    for (int x = 0; x < size.x; x++)
                    {
                        for (int y = 0; y < size.y; y++)
                        {
                            grid[x + (y * size.x)] = new Hexagon(x, y);
                        }
                    }
                    break;

                case HexagonMatrix.Axis.YZ:
                    for (int x = 0; x < size.x; x++)
                    {
                        for (int y = 0; y < size.y; y++)
                        {
                            grid[x + (y * size.x)] = new Hexagon(x, -x - y, y);
                        }
                    }
                    break;

                case HexagonMatrix.Axis.ZX:
                    for (int x = 0; x < size.x; x++)
                    {
                        for (int y = 0; y < size.y; y++)
                        {
                            grid[x + (y * size.x)] = new Hexagon(-x - y, y, x);
                        }
                    }
                    break;
            }

            return grid;
        }

        public override void OnAfterDeserialize()
        {
            size.x = Mathf.Max(size.x, 1);
            size.y = Mathf.Max(size.y, 1);
        }
    }
}
