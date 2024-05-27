namespace TaskScheduler.Console
{
    public class Task (string name, short id, short priority, int BurstTime, int deadline = 0)
    {
        public string Name { get; set; } = name;
        public short Id {  get; set; } = id;
        public short Priority { get; set; } = priority;
        public int BurstTime { get; set; } = BurstTime;
        public int RemainingTime { get; set;} = BurstTime;
        public int Deadline { get; set; } = deadline;
    }
}
