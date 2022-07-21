using Redbus.Interfaces;
using Signals;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Zenject;

namespace View
{
    public class ActivityView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI activityText;
        [SerializeField] private TextMeshProUGUI typeText;
        [SerializeField] private TextMeshProUGUI participantsText;
        [SerializeField] private TextMeshProUGUI priceText;
        [SerializeField] private TextMeshProUGUI linkText;
        [SerializeField] private Button refreshButton;
        [SerializeField] private Button mazeGameButton;
        
        private IEventBus eventBus;
        private LoadingView loadingView;

        [Inject]
        private void Init(IEventBus eventBus, LoadingView loadingView)
        {
            this.eventBus = eventBus;
            
            Setup();
        }

        private void Setup()
        {
            eventBus.Subscribe<SetActivitySignal>(SetActivity);
        }
        private void SetActivity(SetActivitySignal activitySignal)
        {
            var activity = activitySignal.Payload;
            activityText.text = activity.Activity;
            typeText.text = activity.Type;
            participantsText.text = activity.Participants.ToString();
            priceText.text = activity.Price.ToString("n2");
            linkText.text = activity.Link;
        }

        private void Awake()
        {
            RefreshButtonAddListener();
            MazeGameButtonAddListener();
        }

        private void RefreshButtonAddListener()
        {
            refreshButton.onClick.AddListener(RefreshButtonAction);
        }
        
        private void MazeGameButtonAddListener()
        {
            mazeGameButton.onClick.AddListener(MazeGameButtonAction);
        }

        private void RefreshButtonAction()
        {
            eventBus.Publish(new RefreshSignal());
        }
        
        private void MazeGameButtonAction()
        {
            eventBus.Publish(new LoadMazeGameSignal());
        }
    }
}

