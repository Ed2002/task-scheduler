namespace TaskScheduler.Console
{
    public class Timer(int TimeSlice, RRP Scheduler_RRP, EDF Scheduler_EDF)
    {
        private readonly int TimeSlice = TimeSlice;
        private readonly RRP Scheduler_RRP = Scheduler_RRP;
        private readonly EDF Scheduler_EDF = Scheduler_EDF;

        public void StartRRP() => new Thread(() =>
        {
            while (true)
            {
                Scheduler_RRP.Schedule();
                Thread.Sleep(TimeSlice * 100);
            }
        }).Start();

        public void StartEDF() => new Thread(() =>
        {
            while(true)
            {
                Scheduler_EDF.Schedule();
                Thread.Sleep(100);
            }
        }).Start();
    }
}
