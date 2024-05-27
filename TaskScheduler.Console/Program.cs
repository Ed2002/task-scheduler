using TaskScheduler.Console;
using SimulationTimer = TaskScheduler.Console.Timer;


class Program
{
    static void Main()
    {
        RRP Scheduler_RRP = new(3, 10);
        EDF Scheduler_EDF = new();


        Console.WriteLine("Run with: \n\t1-Data In Code (Default Option)\n\t2-Data On File");
        string? value = Console.ReadLine();
        if (!string.IsNullOrEmpty(value))
        {
            var run = Convert.ToInt16(value);
            if (run.Equals(1))
                RunWithCode(Scheduler_RRP, Scheduler_EDF);
            else if (run.Equals(2))
                RunWithFile(Scheduler_RRP, Scheduler_EDF);
            else
                RunWithCode(Scheduler_RRP, Scheduler_EDF);
        }
        else
            RunWithCode(Scheduler_RRP, Scheduler_EDF);

        SimulationTimer TimerSimulation = new(10, Scheduler_RRP, Scheduler_EDF);
        TimerSimulation.StartRRP();
        TimerSimulation.StartEDF();
    }

    private static void RunWithFile(RRP Scheduler_RRP, EDF Scheduler_EDF)
    {
        new FileImport(Scheduler_RRP, Scheduler_EDF);
    }

    private static void RunWithCode(RRP Scheduler_RRP, EDF Scheduler_EDF)
    {
        Scheduler_RRP.AddTask(new("T1", 1, 1, 20));
        Scheduler_RRP.AddTask(new("T2", 2, 2, 30));
        Scheduler_RRP.AddTask(new("T3", 3, 1, 10));
        Scheduler_RRP.AddTask(new("T4", 4, 3, 25));

        Scheduler_EDF.AddTask(new("T1", 1, 1, 20, 50));
        Scheduler_EDF.AddTask(new("T2", 2, 2, 30, 60));
        Scheduler_EDF.AddTask(new("T3", 3, 1, 10, 40));
        Scheduler_EDF.AddTask(new("T4", 4, 3, 25, 70));
    }
}