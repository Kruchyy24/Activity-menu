using Redbus.Events;
using UnityEngine;

namespace Signals
{
    public class SetMazeRotationSignal : PayloadEvent<Vector3>
    {
        public SetMazeRotationSignal(Vector3 payload) : base(payload)
        {
            
        }
    }
}