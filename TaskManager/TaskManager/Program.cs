using TaskManager;

class Program
{
    static void Main()
    {
        var filesHelper = new FilesHelper();
        TasksManager tasksManager = new TasksManager(new ApiHelper(new RequestSender(), filesHelper), filesHelper);
        tasksManager.PrepareTasksFromFile();
    }
}