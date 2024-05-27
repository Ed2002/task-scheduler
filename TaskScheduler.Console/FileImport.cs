using System.Runtime.InteropServices;
using WindowConsole = System.Console;
namespace TaskScheduler.Console
{
    public class FileImport
    {
        private string DirectoryPath = "C:\\Schedules";
        private Dictionary<string, string> Files = new() { {"RRP","rrp.txt"}, {"EDF","edf.txt"} };
        
        public FileImport(RRP Scheduler_RRP, EDF Scheduler_EDF)
        {
            bool run = true;
            while(run)
            {
                DirectoryQuestion();
                run = !VerifyFilesAndAddTasks(Scheduler_RRP, Scheduler_EDF);
            }
        }

        private void DirectoryQuestion()
        {
            WindowConsole.WriteLine($"You want to use the Directory {DirectoryPath}\n1- Yes (Default)");
            string? useDirectory = WindowConsole.ReadLine();
            if (!string.IsNullOrEmpty(useDirectory))
            {
                if (useDirectory.Equals("1"))
                    VerifyDirectoryPath();
                else
                    VerifyDirectoryPath();
            }
        }

        private Task AddTaskToScheduleRRP(short id, string data)
        {
            List<string> dataFromText = [.. data.Split(',')];
            return new(dataFromText[0], id, Convert.ToInt16(dataFromText[1]), Convert.ToInt16(dataFromText[2]));
        }

        private Task AddTaskToScheduleEDF(short id, string data)
        {
            List<string> dataFromText = [.. data.Split(',')];
            return new(dataFromText[0], id, Convert.ToInt16(dataFromText[1]), Convert.ToInt16(dataFromText[2]), Convert.ToInt16(dataFromText[3]));
        }

        private bool VerifyFilesAndAddTasks(RRP Scheduler_RRP, EDF Scheduler_EDF)
        {
            bool rrp = false, edf = false;
            
            string pathRRP = Path.Combine(DirectoryPath, Files["RRP"]);
            string pathEDF = Path.Combine(DirectoryPath, Files["EDF"]);
            if (File.Exists(pathRRP))
            {
                WindowConsole.WriteLine("File RRP Found.");
                using (StreamReader reader = new(pathRRP))
                {
                    string? line;
                    short id = 1;
                    while ((line = reader.ReadLine()) != null)
                    {
                        Scheduler_RRP.AddTask(AddTaskToScheduleRRP(id, line));
                        id++;
                    }
                }
                rrp = true;
            }
            else
                WindowConsole.WriteLine("File RRP Not Found.");
            if (File.Exists(pathEDF))
            {
                WindowConsole.WriteLine("File EDF Found.");
                using (StreamReader reader = new(pathEDF))
                {
                    string? line;
                    short id = 1;
                    while ((line = reader.ReadLine()) != null)
                    {
                        Scheduler_EDF.AddTask(AddTaskToScheduleEDF(id, line));
                        id++;
                    }
                }
                edf = true;
            }
            else
                WindowConsole.WriteLine("File EDF Not Found.");
            return rrp && edf ? true : false;
        }

        private void VerifyDirectoryPath()
        {
            if (Directory.Exists(DirectoryPath))
                WindowConsole.WriteLine("Directory Found.");
            else
            {
                WindowConsole.WriteLine("Not Found.");
                WindowConsole.WriteLine("Creating for you......");
                Directory.CreateDirectory(DirectoryPath);
                WindowConsole.WriteLine("Created");
                VerifyDirectoryPath();
            }
        }
    }
}
