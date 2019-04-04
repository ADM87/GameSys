using GameSystems.Routines;
using System;
using System.Collections;
using UnityEngine;

namespace GameSystems.Assets
{
    public class AssetLoader
    {
        private static AssetLoader instance;
        public static AssetLoader GetInstance()
        {
            return instance ?? (instance = new AssetLoader());
        }

        private RoutineRunner routineRunner;

        private AssetLoader()
        {
            routineRunner = RoutineRunner.GetInstance();
        }

        public AssetLoader LoadAssetAsync<T>(string path, Action<T> loadComplete)
        {
            routineRunner.Run(AsyncLoadRoutine(path, false, loadComplete));
            return this;
        }

        public AssetLoader LoadAssetAsync<T>(string path, bool addToCache, Action<T> loadComplete)
        {
            routineRunner.Run(AsyncLoadRoutine(path, addToCache, loadComplete));
            return this;
        }

        private IEnumerator AsyncLoadRoutine<T>(string path, bool addToCache, Action<T> loadComplete)
        {
            Type assetType = typeof(T);
            ResourceRequest request = Resources.LoadAsync(path, assetType);

            WaitUntil requestComplete = new WaitUntil(() => request.progress >= 1f);
            yield return requestComplete;

            try
            {
                loadComplete?.Invoke((T)Convert.ChangeType(request.asset, assetType));
            }
            catch (InvalidCastException)
            {
                loadComplete?.Invoke(default);
            }
        }
    }
}
