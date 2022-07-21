using Redbus.Events;
using UnityEngine;

namespace Signals
{
    public class GetActualBallSpeed: PayloadEvent<float>
    {
        public GetActualBallSpeed(float payload) : base(payload)
        {
            
        }
    }
}