using System.Collections;
using UnityEngine;

namespace GameSys.Routines
{
    public class RoutineRunner : MonoBehaviour
    {
        public static RoutineRunner Create()
        {
            GameObject gameObject = new GameObject("[RoutineRunner]");
            return gameObject.AddComponent<RoutineRunner>();
        }

        public Coroutine Run(IEnumerator routine)
        {
            return StartCoroutine(routine);
        }
    }
}
