using System.Collections;
using UnityEngine;

namespace Service
{
    public class CoroutineService : MonoBehaviour
    {
        public Coroutine StartC(IEnumerator coroutine)
        {
            return StartCoroutine(coroutine);
        }

        public void End(Coroutine coroutine)
        {
            StopCoroutine(coroutine);
        }
    }
}

