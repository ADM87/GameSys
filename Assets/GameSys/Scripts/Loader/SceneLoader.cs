using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameSystems.Loader
{
    public class SceneLoader
    {
        private static SceneLoader instance;
        public static SceneLoader GetInstance()
        {
            return instance ?? (instance = new SceneLoader());
        }

        private SceneLoader() { }

        public void AsyncSceneLoad(string name, Action<string> loadComplete)
        {
            GameSys.Routines.Run(AsyncLoadRoutine(name, loadComplete));
        }
        public void AsyncSceneLoad(string[] names, Action<string[]> loadComplete)
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

            WaitUntil asyncComplete = new WaitUntil(() => {
                for (int i = 0; i < asyncOps.Length; i++)
                {
                    if (asyncOps[i].progress < 1)
                    {
                        return false;
                    }
                }
                return true;
            });
            yield return asyncComplete;

            loadComplete?.Invoke(names);
        }
    }
}
