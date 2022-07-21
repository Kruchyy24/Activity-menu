using System;
using System.Collections;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;
using Zenject;

namespace Service
{
    public class BoredService : IBoredService
    {
        private const string BoredApiUrl = "www.boredapi.com/api/activity";
        private CoroutineService coroutineService;

        [Inject]
        private void Init(CoroutineService coroutineService)
        {
            this.coroutineService = coroutineService;
        }
        public ActivityDto GetBoredActivity(Action<ActivityDto> activityReceiveCallback)
        {
            var request = UnityWebRequest.Get(BoredApiUrl);
            coroutineService.StartC(GetRequestCoroutine(request, activityReceiveCallback));
            return new ActivityDto() { TextActivity = request.downloadHandler.text };
        }

        private IEnumerator GetRequestCoroutine(UnityWebRequest www, Action<ActivityDto> activityReceiveCallback)
        {
            Debug.Log("Before SendWebRequest");
            yield return www.SendWebRequest();
            yield return Wait(2);
 
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("SendWebRequest dziala");
                Debug.Log(www.downloadHandler.text);
                var dto = JsonConvert.DeserializeObject<ActivityDto>(www.downloadHandler.text);
                activityReceiveCallback?.Invoke(dto);
            }
        }

        private IEnumerator Wait(int time)
        {
            yield return new WaitForSeconds(time);
        }
    }
}