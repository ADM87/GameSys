using UnityEngine;

namespace GameSystems.AStar
{
    public struct AStarNode
    {
        public float F { get; set; }
        public float G { get; set; }
        public float H { get; set; }
    }
}
