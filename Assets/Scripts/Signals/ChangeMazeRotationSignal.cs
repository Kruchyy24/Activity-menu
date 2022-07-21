using Redbus.Events;
using UnityEngine;

namespace Signals
{
    public class ChangeMazeRotationSignal : PayloadEvent<Vector3>
    {
        public ChangeMazeRotationSignal(Vector3 payload) : base(payload)
        {
            
        }
    }
}