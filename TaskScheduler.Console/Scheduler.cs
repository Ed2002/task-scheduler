using WindowConsole = System.Console;

namespace TaskScheduler.Console
{
    public class Scheduler()
    {
        private List<Task> TasksList = [];

        public virtual void AddTask(Task task) => TasksList.Add(task);

        public virtual void Schedule()
        {
            try
            {
                TasksList = TasksList.OrderBy(t => t.Priority).ToList();
                while (TasksList.Count > 0)
                {
                    for (int i = 0; i < TasksList.Count; i++)
                    {
                        Task CurrentTask = TasksList[i];
                        WindowConsole.WriteLine("Task: {TaskId}, TaskPriority {TaskPriority}", CurrentTask.Id, CurrentTask.Priority);
                        TasksList.Remove(TasksList[i]);
                    }
                }
            }
            catch(Exception ex)
            {
                WindowConsole.WriteLine("ERRO ON Schedule\nErro: {ErroMsg}\nStackTrace: {ErroStackTrace}", ex.Message, ex.StackTrace);
                throw;
            }
        }
    }
}
