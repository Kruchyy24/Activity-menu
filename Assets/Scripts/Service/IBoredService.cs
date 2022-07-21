using System;

namespace Service
{
    public interface IBoredService
    {
        ActivityDto GetBoredActivity(Action<ActivityDto> activityReceiveCallback);
    }
}