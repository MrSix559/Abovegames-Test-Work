using System.Collections;

namespace Tools
{
    public class CoroutineRunner : Singleton<CoroutineRunner>
    {
        public static UnityEngine.Coroutine Run(IEnumerator coroutine) => Instance.StartCoroutine(coroutine);
        public static void Stop(IEnumerator coroutine) => Instance.StopCoroutine(coroutine);
        public static void StopAll(IEnumerator coroutine) => Instance.StopAllCoroutines();
    }
}