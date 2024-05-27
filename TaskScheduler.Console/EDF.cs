using WindowConsole = System.Console;

namespace TaskScheduler.Console
{
    public class EDF : Scheduler
    {
        private List<Task> TasksList = [];

        public override void AddTask(Task task) => TasksList.Add(task);

        public override void Schedule()
        {
            TasksList.Sort((taskOne, taskTwo) => taskOne.Deadline.CompareTo(taskTwo.Deadline));
            int CurrentTime = 0;
            TasksList.ForEach(CurrentTask =>
            {
                WindowConsole.WriteLine($"Time {CurrentTime}: Task {CurrentTask.Id} with DeadLine {CurrentTask.Deadline} is Executing...");
                CurrentTime += CurrentTask.BurstTime;
            });
        }
    }
}
