using System.Collections;
using UnityEngine;

namespace GameSystems.Routines
{
    public class RoutineRunner : MonoBehaviour
    {
        private static RoutineRunner instance;
        public static RoutineRunner Create()
        {
            if (instance == null)
            {
                GameObject gameObject = new GameObject("[RoutineRunner]");
                instance = gameObject.AddComponent<RoutineRunner>();

                DontDestroyOnLoad(gameObject);
            }
            return instance;
        }

        private void Awake()
        {
            if (instance != null)
            {
                Debug.LogError("[RoutineRunner] Detected multiple RoutineRunners. Use GameSys.RoutineRunner or RoutineRunner.Create() to get the instance of the RoutineRunner.");
                DestroyImmediate(gameObject);
            }
        }

        public Coroutine Run(IEnumerator routine)
        {
            return StartCoroutine(routine);
        }
    }
}
