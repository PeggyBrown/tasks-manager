using TaskManager;

class Program
{
    static void Main()
    {
        TasksReader tasksReader = new TasksReader(new ApiHelper());
        tasksReader.PrepareTasksFromFile();
    }
}