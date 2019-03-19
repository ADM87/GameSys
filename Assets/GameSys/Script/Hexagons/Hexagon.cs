using GameSystems.Utility;
using UnityEngine;

/// <summary>
/// Referenced from:
/// http://www.redblobgames.com/grids/hexagons/
/// http://www.redblobgames.com/grids/hexagons/implementation.html
/// </summary>
namespace GameSystems.Hexagons
{
    [System.Serializable]
    public struct Hexagon
    {
        /// <summary> Hexagon orientation types. Horizontal = pointy topped; Vertical = flat topped. </summary>
        public enum Orientation { None, Horizontal, Vertical };

        /// <summary> Number of edges in a hexagon. </summary>
        public const int Edges = 6;
        /// <summary> The inner angle between two edges of the hexagon. [120] </summary>
        public const int InnerAngle = 120;
        /// <summary> Half of the inner angle. [60] </summary>
        public const int InnerHalf = 60;
        /// <summary> Quarter of the inner angle. [30] </summary>
        public const int InnerQuarter = 30;

        /**************************************************************
         * Direction reference (lines point to the edge with a neighbor: 
         *      Horizontal:           Vertical:
         *     NW        NE               N
         *       \      /          NW     |     NE
         *        [5][0]             \   [5]   /
         *  W -- [4]  [1] -- E        [4]   [0]
         *        [3][2]              [3]   [1]
         *       /      \            /   [2]   \
         *     SW        SE        SW     |     SE
         *                                S
         /**************************************************************/
        /// <summary> Hexagon direction coordinates. </summary>
        public static readonly Hexagon[] Directions = new Hexagon[Edges] {
            new Hexagon(0, 1, -1), new Hexagon(1, 0, -1), new Hexagon(1, -1, 0),
            new Hexagon(0, -1, 1), new Hexagon(-1, 0, 1), new Hexagon(-1, 1, 0)
        };

        [SerializeField]
        private int x;
        /// <summary> Hexagon x coordinate. </summary>
        public int X { get { return x; } }

        [SerializeField]
        private int y;
        /// <summary> Hexagon y coordinate. </summary>
        public int Y { get { return y; } }

        [SerializeField]
        private int z;
        /// <summary> Hexagon z coordinate. </summary>
        public int Z { get { return z; } }

        /// <summary> Length of the hexagon's coordinates. </summary>
        public int Length { get { return (int)((Mathf.Abs(x) + Mathf.Abs(y) + Mathf.Abs(z)) * 0.5f); } }

        public Hexagon(int x, int y)
        {
            this.x = x;
            this.y = y;

            z = -x - y;

            if (!Validate(this))
            {
                throw new System.Exception("[Hexagon] Sum of hexagon coordinates must equal zero.");
            }
        }

        public Hexagon(int x, int y, int z)
        {
            this.x = x;
            this.y = y;
            this.z = z;

            if (!Validate(this))
            {
                throw new System.Exception("[Hexagon] Sum of hexagon coordinates must equal zero.");
            }
        }

        public void Set(int x, int y, int z)
        {
            this.x = x;
            this.y = y;
            this.z = z;

            if (!Validate(this))
            {
                throw new System.Exception("[Hexagon] Sum of hexagon coordinates must equal zero.");
            }
        }

        public int Distance(Hexagon hexagon)
        {
            return (hexagon - this).Length;
        }

        public Hexagon Neighbor(int direction)
        {
            return this + Directions[MathHelper.SafeMod(direction, Edges)];
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return "(" + x + ", " + y + ", " + z + ")";
        }

        public static Hexagon operator + (Hexagon hex1, Hexagon hex2)
        {
            return new Hexagon(hex1.X + hex2.X, hex1.Y + hex2.Y, hex1.Z + hex2.Z);
        }

        public static Hexagon operator - (Hexagon hex1, Hexagon hex2)
        {
            return new Hexagon(hex1.X - hex2.X, hex1.Y - hex2.Y, hex1.Z - hex2.Z);
        }

        public static Hexagon operator * (Hexagon hexagon, int scale)
        {
            return new Hexagon(hexagon.X * scale, hexagon.Y * scale, hexagon.Z * scale);
        }

        public static bool operator == (Hexagon hex1, Hexagon hex2)
        {
            return hex1.X == hex2.X && hex1.Y == hex2.Y && hex1.Z == hex2.Z;
        }

        public static bool operator !=(Hexagon hex1, Hexagon hex2)
        {
            return hex1.X != hex2.X && hex1.Y != hex2.Y && hex1.Z != hex2.Z;
        }

        /// <summary>
        /// Validate the provided coordinates. Returns true of the sum of the coordinates equal zero.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public static bool Validate(int x, int y, int z)
        {
            return (x + y + z) == 0;
        }

        /// <summary>
        /// Validate the provided hexagon. Returns true of the sum of the hexagon coordinates equal zero.
        /// </summary>
        /// <param name="hexagon"></param>
        public static bool Validate(Hexagon hexagon)
        {
            return (hexagon.X + hexagon.Y + hexagon.Z) == 0;
        }
    }
}
