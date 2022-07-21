using System.Collections;
using Model;
using Redbus.Interfaces;
using Service;
using Signals;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Controller
{
    public class ActivityController
    {
        private IBoredService boredService;
        private ActivityModel activityModel;
        private CoroutineService coroutineService;
        private IEventBus eventBus;
        private bool coroutineRunning;

        [Inject]
        private void Init(IBoredService boredService, ActivityModel activityModel, IEventBus eventBus, CoroutineService coroutineService)
        {
            this.boredService = boredService;
            this.activityModel = activityModel;
            this.eventBus = eventBus;
            this.coroutineService = coroutineService;
            
            Setup();
        }

        private void Setup()
        {
            GetNewActivity();
            SubscribeSignals();
        }

        private void SubscribeSignals()
        {
            eventBus.Subscribe<RefreshSignal>((a) => Refresh());
            eventBus.Subscribe<EndLoadingActivitySignal>((a) => SendActivitySignal(activityModel));
            eventBus.Subscribe<LoadMazeGameSignal>((a) => LoadMazeGame());
        }

        private void Refresh()
        {
            GetNewActivity();
        }

        private void GetNewActivity()
        {
            eventBus.Publish(new StartLoadingSignal());
            boredService.GetBoredActivity(AfterActivityReceiveCallback);
        }

        private void AfterActivityReceiveCallback(ActivityDto dto)
        {
            activityModel = new ActivityModel
            {
                Activity = dto.TextActivity,
                Type = dto.TextType,
                Participants = dto.TextParticipants,
                Price = dto.TextPrice,
                Link = dto.TextLink,
                Key = dto.TextKey,
                Accessibility = dto.TextAccessibility
            };
            eventBus.Publish(new EndLoadingSignal());
        }
        
        private void SendActivitySignal(ActivityModel activityModel)
        {
            eventBus.Publish(new SetActivitySignal(activityModel));
        }
        
        private IEnumerator Wait(int time)
        {
            yield return new WaitForSeconds(time);
        }

        private void LoadMazeGame()
        {
            SceneManager.LoadScene(1);
        }
    }

}
