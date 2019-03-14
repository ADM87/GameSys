using GameSys.Loader;
using GameSys.Routines;
using UnityEngine;
 
namespace GameSys
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
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Boot()
        {
            Routines = RoutineRunner.Create();
            Assets = AssetLoader.Create();
            Scenes = SceneLoader.Create();
        }
    }
}
