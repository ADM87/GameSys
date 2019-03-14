using System;
using System.Collections;
using UnityEngine;

namespace GameSys.Loader
{
    public class AssetLoader
    {
        private static AssetLoader instance;
        public static AssetLoader Create()
        {
            return instance ?? (instance = new AssetLoader());
        }

        private AssetLoader() { }

        public void AsyncAssetLoad<T>(string path, Action<T> loadComplete)
        {
            GameSys.Routines.Run(AsyncLoadRoutine(path, loadComplete));
        }

        private IEnumerator AsyncLoadRoutine<T>(string path, Action<T> loadComplete)
        {
            Type assetType = typeof(T);
            ResourceRequest request = Resources.LoadAsync(path, assetType);

            WaitUntil requestComplete = new WaitUntil(() => request.progress >= 1);
            yield return requestComplete;

            try
            {
                loadComplete?.Invoke((T)Convert.ChangeType(request.asset, assetType));
            }
            catch (InvalidCastException)
            {
                loadComplete?.Invoke(default(T));
            }
        }
    }
}
