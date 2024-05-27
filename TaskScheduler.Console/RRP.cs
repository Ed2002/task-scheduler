using WindowConsole = System.Console;

namespace TaskScheduler.Console
{
    public class RRP : Scheduler
    {
        private List<Queue<Task>> ReadyQueues = [];
        private readonly int TimeSlice;

        public RRP(int timeSlice, int numberOfPriorities = 2)
        {
            ReadyQueues = new(numberOfPriorities);
            for(int i = 0; i < numberOfPriorities; i++)
            {
                ReadyQueues.Add(new Queue<Task>());
            }
            TimeSlice = timeSlice;
        }

        public override void AddTask(Task task)
        {
            if ((task.Priority - 1) > 0)
                ReadyQueues[task.Priority - 1].Enqueue(task);
            else
                ReadyQueues[1].Enqueue(task);
        }

        public override void Schedule()
        {
            int CurentTime = 0;
            while (true)
            {
                bool AllQueuesEmpty = true;
                for(int i = 0; i < ReadyQueues.Count; i++)
                {
                    if (ReadyQueues[i].Count > 0)
                    {
                        AllQueuesEmpty = false;
                        Task CurrentTask = ReadyQueues[i].Dequeue();
                        int ExecuteTime = Math.Min(TimeSlice, CurrentTask.RemainingTime);
                        CurrentTask.RemainingTime -= ExecuteTime;
                        CurentTime += ExecuteTime;
                        WindowConsole.WriteLine($"Time {CurentTime}: Task {CurrentTask.Id} of Priority {CurrentTask.Priority} is Executing...");

                        if (CurrentTask.RemainingTime > 0)
                            ReadyQueues[i].Enqueue(CurrentTask);
                        else
                            WindowConsole.WriteLine("Dead");
                    }
                }

                if(AllQueuesEmpty)
                    break;
            }
        }

    }
}
