using Redbus.Events;
using UnityEngine;

namespace Signals
{
    public class ActualVelocitySignal  : PayloadEvent<Vector3>
    {
        public ActualVelocitySignal(Vector3 payload) : base(payload)
        {
            
        }
    }
}