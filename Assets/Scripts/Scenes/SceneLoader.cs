using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameSystems.Scenes
{
    public class SceneLoader
    {
        private static SceneLoader instance;
        public static SceneLoader GetInstance()
        {
            return instance ?? (instance = new SceneLoader());
        }

        private SceneLoader() { }

        public void LoadSceneAsync(string name, Action<string> loadComplete)
        {
            GameSys.Routines.Run(AsyncLoadRoutine(name, loadComplete));
        }
        public void LoadSceneAsync(string[] names, Action<string[]> loadComplete)
        {
            GameSys.Routines.Run(AsyncLoadRoutine(names, loadComplete));
        }

        private IEnumerator AsyncLoadRoutine(string name, Action<string> loadComplete)
        {
            AsyncOperation asyncOp = SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);

            WaitUntil asyncComplete = new WaitUntil(() => asyncOp.progress >= 1);
            yield return asyncComplete;

            loadComplete?.Invoke(name);
        }

        private IEnumerator AsyncLoadRoutine(string[] names, Action<string[]> loadComplete)
        {
            AsyncOperation[] asyncOps = new AsyncOperation[names.Length];
            for (int i = 0; i < names.Length; i++)
            {
                asyncOps[i] = SceneManager.LoadSceneAsync(names[i], LoadSceneMode.Additive);
            }

            // Wait until all scenes are loaded.
            WaitUntil asyncComplete = new WaitUntil(() => {
                float totalProgress = 0;
                for (int i = 0; i < asyncOps.Length; i++)
                {
                    totalProgress += asyncOps[i].progress;
                }
                return totalProgress / asyncOps.Length >= 1f;
            });
            yield return asyncComplete;

            loadComplete?.Invoke(names);
        }
    }
}
