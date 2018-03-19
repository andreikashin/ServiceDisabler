namespace ServiceDisabler.Services
{
    internal interface IScheduleService
    {
        StopSchedule GetSchedule();
        void UpdateSchedule(StopSchedule currentSchedule, StopTimeRecord[] newStopTimeRecords);
        void SaveSchedule(StopSchedule currentSchedule);
    }
}
