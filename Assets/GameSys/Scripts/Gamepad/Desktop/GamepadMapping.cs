using UnityEngine;

namespace GameSys.Gamepad
{
    [CreateAssetMenu(fileName = "GamepadMapping", menuName = "GameSys/Gamepad/GamepadMapping", order = 1)]
    public class GamepadMapping : ScriptableObject
    {
        [System.Serializable]
        public struct MappingEntry
        {
            [SerializeField]
            private AxisType gamepadAxis;
            public AxisType GamepadAxis { get { return gamepadAxis; } }

            [SerializeField]
            private string inputAxis;
            public string InputAxis { get { return inputAxis; } }
        }

        [SerializeField]
        private MappingEntry[] entries;
        public MappingEntry[] Entries { get { return entries; } }
    }
}
