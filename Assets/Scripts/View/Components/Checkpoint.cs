using Redbus.Interfaces;
using Signals;
using UnityEngine;
using Zenject;

namespace View.Components
{
    public class Checkpoint : MonoBehaviour
    {
        [SerializeField] private string checkpointName;
    
        private IEventBus eventBus;
    
        [Inject]
        private void Init(IEventBus eventBus)
        {
            this.eventBus = eventBus;
        }

        private void OnTriggerEnter(Collider collider)
        {
            if (collider.tag == "Player")
            {
                eventBus.Publish(new CheckpointSignal(checkpointName));
            }
        }
    }
}

