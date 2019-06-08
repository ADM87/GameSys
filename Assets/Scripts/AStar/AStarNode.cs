using UnityEngine;
 
namespace GameSystems.AStar
{
    public struct AStarNode
    {
        public float F { get; set; }
        public float G { get; set; }
        public float H { get { return F + G; } }

        public bool Walkable { get; private set; }

        public Vector3 Position { get; private set; }

        public AStarNode(Vector3 position, bool walkable)
        {
            F = G = 0;
            Walkable = walkable;
            Position = position;
        }
    }
}
