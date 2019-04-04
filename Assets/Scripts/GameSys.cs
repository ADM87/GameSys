using GameSystems.Assets;
using GameSystems.Routines;
using GameSystems.Scenes;

namespace GameSystems
{
    public static class GameSys
    {
        /// <summary>
        /// Singleton MonoBehaviour used to run coroutines. Useful for classes that are not Unity components.
        /// </summary>
        public static RoutineRunner Routines { get; private set; }
        /// <summary>
        /// Asset helper for loading and unloading assets.
        /// </summary>
        public static AssetLoader Assets { get; private set; }
        /// <summary>
        /// Scene management helper for loading and unloading scenes.
        /// </summary>
        public static SceneLoader Scenes { get; private set; }

        /// <summary>
        /// Initialize the core game systems.
        /// </summary>
        public static void Initialize()
        {
            Routines = RoutineRunner.GetInstance();
            Assets = AssetLoader.GetInstance();
            Scenes = SceneLoader.GetInstance();
        }
    }
}
