using Redbus.Events;
using UnityEngine;

namespace Signals
{
    public class CheckpointSignal  : PayloadEvent<string>
    {
        public CheckpointSignal(string payload) : base(payload)
        {
            
        }
    }
}