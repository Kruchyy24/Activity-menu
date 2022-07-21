using Redbus.Events;
using UnityEngine;

namespace Signals
{
    public class GetActualMazeRotationSignal: PayloadEvent<Vector3>
    {
        public GetActualMazeRotationSignal(Vector3 payload) : base(payload)
        {
            
        }
    }
}