using System.Diagnostics;
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

            while (TasksList.Count > 0)
            {
                Task CurrentTask = TasksList[0];
                TasksList.RemoveAt(0);

                WindowConsole.WriteLine($"Time {CurrentTime}: Task {CurrentTask.Id} with DeadLine {CurrentTask.Deadline} is Executing...");
                CurrentTime += CurrentTask.BurstTime;
            }


            //TasksList.ForEach(CurrentTask =>
            //{
            //    WindowConsole.WriteLine($"Time {CurrentTime}: Task {CurrentTask.Id} with DeadLine {CurrentTask.Deadline} is Executing...");
            //    CurrentTime += CurrentTask.BurstTime;
            //});
        }
    }
}
