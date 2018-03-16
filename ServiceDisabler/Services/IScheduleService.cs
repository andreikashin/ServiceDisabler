namespace ServiceDisabler.Services
{
    internal interface IScheduleService
    {
        StopSchedule GetSchedule();
        void UpdateSchedule(StopTimeRecord[] stopTimeRecords);
    }
}
