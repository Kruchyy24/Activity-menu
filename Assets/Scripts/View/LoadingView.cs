using System;
using System.Collections;
using System.Linq;
using Redbus.Interfaces;
using Signals;
using UnityEngine;
using UnityEngine.Serialization;
using View.Types;
using Zenject;

namespace View
{
    public class LoadingView : MonoBehaviour
    {
        private IEventBus eventBus;
        [SerializeField] private LoadingIndicatorType chosenLoadingIndicatorType;
        [FormerlySerializedAs("_loadingIndicators")] [SerializeField] private TypedLoadingIndicator[] loadingIndicators;
        [SerializeField] private int minimalWaitingTime = 5;
        
        private bool coroutineRunning;
        private Action onMinimalWaitingTimeCountdownEnd;    //Akcja jednorazowa
        

        [Inject]
        private void Init(IEventBus eventBus)
        {
            this.eventBus = eventBus;
            
            Setup();
        }

        private void Awake()
        {
            var chosenIndicator = loadingIndicators.First(loadingIndicator => loadingIndicator.IndicatorType == chosenLoadingIndicatorType);
            Instantiate(chosenIndicator.IndicatorView, transform);
        }

        private void Setup()
        {
            eventBus.Subscribe<StartLoadingSignal>(StartLoading);
            eventBus.Subscribe<EndLoadingSignal>(EndLoading);
        }
        
        private void StartLoading(StartLoadingSignal startLoading)
        {
            gameObject.SetActive(true);
            StartCoroutine(StartMinimalWaitingTimeCountdown());
            coroutineRunning = true;
        }
        
        private void EndLoading(EndLoadingSignal endLoading)
        {
            if (coroutineRunning == false)
            {
                gameObject.SetActive(false);
                eventBus.Publish(new EndLoadingActivitySignal());
            }
            else
            {
                onMinimalWaitingTimeCountdownEnd = () =>
                {
                    gameObject.SetActive(false);
                    eventBus.Publish(new EndLoadingActivitySignal());
                };   //akcja wywołuje ustawienie obiektu i wysłanie sygnału
            }
        }

        private IEnumerator StartMinimalWaitingTimeCountdown()
        {
            coroutineRunning = true;
            yield return Wait(minimalWaitingTime);
            coroutineRunning = false;
            
            onMinimalWaitingTimeCountdownEnd?.Invoke(); //wykonaj akcje
            onMinimalWaitingTimeCountdownEnd = null;
        }

        private IEnumerator Wait(int time)
        {
            yield return new WaitForSeconds(time);
        }
    }
}