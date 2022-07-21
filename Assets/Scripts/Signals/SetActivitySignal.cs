using Model;
using Redbus.Events;

namespace Signals
{
    public class SetActivitySignal : PayloadEvent<ActivityModel>
    {
        public SetActivitySignal(ActivityModel payload) : base(payload)
        {
        }
    }
}