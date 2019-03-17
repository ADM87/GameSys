using GameSystems.Loader;
using GameSystems.Routines;
using UnityEngine;

namespace GameSystems
{
    public static class GameSys
    {
        /// <summary>
        /// 
        /// </summary>
        public static RoutineRunner Routines { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public static AssetLoader Assets { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public static SceneLoader Scenes { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public static void Initialize()
        {
            Routines = RoutineRunner.Create();
            Assets = AssetLoader.Create();
            Scenes = SceneLoader.Create();

        }
    }
}
