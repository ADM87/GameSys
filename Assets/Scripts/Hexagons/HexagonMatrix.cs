using UnityEngine;

/// <summary>
/// Referenced from:
/// http://www.redblobgames.com/grids/hexagons/
/// http://www.redblobgames.com/grids/hexagons/implementation.html
/// </summary>
namespace GameSystems.Hexagons
{
    [System.Serializable]
    public class HexagonMatrix :
        ISerializationCallbackReceiver
    {
        /// <summary> Possible 2D axis types. </summary>
        public enum Axis { XY, XZ, YX, YZ, ZX, ZY };

        /// <summary> Square root constant of 3. [1.732051f] </summary>
        public const float Sqrt_3 = 1.732051f;

        /// <summary> Orientation of a horizontal layout. </summary>
        public static readonly HexagonMatrix Horizontal = new HexagonMatrix(
            Sqrt_3, Sqrt_3 / 2f, 0f, 3f / 2f, // Forward matrix
            Sqrt_3 / 3f, -1f / 3f, 0f, 2f / 3f, // Inverse matrix
            0.5f // Starting angle
        );
        /// <summary> Orientation of a vertical layout. </summary>
        public static readonly HexagonMatrix Vertical = new HexagonMatrix(
            3f / 2f, 0f, Sqrt_3 / 2f, Sqrt_3, // Forward matrix
            2f / 3f, 0f, -1f / 3f, Sqrt_3 / 3f, // Inverse matrix
            0f // Starting angle
        );

        [SerializeField]
        private Hexagon.Orientation orientation;
        /// <summary> Orientation of the hexagons used within this matrix. </summary>
        public Hexagon.Orientation Orientation
        {
            get { return orientation; }
            set
            {
                switch (value)
                {
                    case Hexagon.Orientation.Horizontal:
                        Set(Horizontal.F0, Horizontal.F1, Horizontal.F2, Horizontal.F3, Horizontal.B0, Horizontal.B1, Horizontal.B2, Horizontal.B3, Horizontal.Angle);
                        break;

                    case Hexagon.Orientation.Vertical:
                        Set(Vertical.F0, Vertical.F1, Vertical.F2, Vertical.F3, Vertical.B0, Vertical.B1, Vertical.B2, Vertical.B3, Vertical.Angle);
                        break;
                }

                orientation = value;
            }
        }

        [SerializeField]
        private float cellRadius;
        /// <summary> Size of a hexagon cell. </summary>
        public float CellRadius { get { return cellRadius; } }

        [SerializeField]
        private Vector2 origin;
        /// <summary> Starting offset for hexagon positions. </summary>
        public Vector2 Origin { get { return origin; } }

        /// <summary> Forward matrix f0. </summary>
        public float F0 { get; private set; }
        /// <summary> Forward matrix f1. </summary>
        public float F1 { get; private set; }
        /// <summary> Forward matrix f2. </summary>
        public float F2 { get; private set; }
        /// <summary> Forward matrix f3. </summary>
        public float F3 { get; private set; }
        /// <summary> Inverse matrix b0. </summary>
        public float B0 { get; private set; }
        /// <summary> Inverse matrix b1. </summary>
        public float B1 { get; private set; }
        /// <summary> Inverse matrix b2. </summary>
        public float B2 { get; private set; }
        /// <summary> Inverse matrix b3. </summary>
        public float B3 { get; private set; }
        /// <summary> Start angle in multiples of 60. </summary>
        public float Angle { get; private set; }

        public HexagonMatrix(Hexagon.Orientation orientation)
        {
            switch (orientation)
            {
                case Hexagon.Orientation.Horizontal:
                    Set(Horizontal.F0, Horizontal.F1, Horizontal.F2, Horizontal.F3, Horizontal.B0, Horizontal.B1, Horizontal.B2, Horizontal.B3, Horizontal.Angle);
                    break;

                case Hexagon.Orientation.Vertical:
                    Set(Vertical.F0, Vertical.F1, Vertical.F2, Vertical.F3, Vertical.B0, Vertical.B1, Vertical.B2, Vertical.B3, Vertical.Angle);
                    break;
            }
        }

        public HexagonMatrix(float f0, float f1, float f2, float f3, float b0, float b1, float b2, float b3, float angle)
        {
            Set(f0, f1, f2, f3, b0, b1, b2, b3, angle);
        }

        public void Set(float f0, float f1, float f2, float f3, float b0, float b1, float b2, float b3, float angle)
        {
            F0 = f0;
            F1 = f1;
            F2 = f2;
            F3 = f3;
            B0 = b0;
            B1 = b1;
            B2 = b2;
            B3 = b3;

            Angle = angle;
        }

        public void SetRadius(float radius)
        {
            cellRadius = radius;
        }

        public void SetOrigin(Vector2 origin)
        {
            this.origin = origin;
        }

        public Vector2 From(Hexagon hexagon)
        {
            return new Vector2(
                origin.x + (F0 * hexagon.X + F1 * hexagon.Y) * cellRadius,
                origin.y + (F2 * hexagon.X + F3 * hexagon.Y) * cellRadius
            );
        }

        public Hexagon To(Vector2 point)
        {
            Vector2 delta = (point - origin) / cellRadius;

            float fx = B0 * delta.x + B1 * delta.y;
            float fy = B2 * delta.x + B2 * delta.y;
            float fz = -fx - fy;

            int x = Mathf.RoundToInt(fx);
            int y = Mathf.RoundToInt(fy);
            int z = Mathf.RoundToInt(fz);

            float dx = Mathf.Abs(x - fx);
            float dy = Mathf.Abs(y - fy);
            float dz = Mathf.Abs(z - fz);

            if (dx > dy && dx > dz) x = -y - z;
            else if (dy > dz)       y = -x - z;
            else                    z = -x - y;

            return new Hexagon(x, y, z);
        }

        public Vector2 CornerOffset(int direction)
        {
            float angle = 2f * Mathf.PI * (Angle + direction) / Hexagon.Edges;
            return new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * cellRadius;
        }

        public void OnBeforeSerialize() { }

        public void OnAfterDeserialize()
        {
            Orientation = orientation;
        }

        public override string ToString()
        {
            return "(" + F0 + ", " + F1 + ", " + F2 + ", " + F3 + ", " + B0 + ", " + B1 + ", " + B2 + ", " + B3 + ", " + Angle + ", " + cellRadius + ")";
        }
    }
}
